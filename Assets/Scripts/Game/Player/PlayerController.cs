using DS.Game.Audio;
using DS.Game.Camera;
using DS.Game.Core;
using DS.Game.DamageSystem;
using DS.Game.Enemy;
using DS.Game.Message;
using DS.Game.Weapon;
using DS.Runtime;
using UnityEngine;

namespace DS.Game.Player
{ 
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoSingleton<PlayerController>,IMessageReceiver<DamageData>
    {
        [Header("Base Character Attribute")]
        public float maxForwardSpeed = 8f;
        public float gravity = 20f;
        public float jumpSpeed = 10f;
        public float minTurnSpeed = 400f;
        public float maxTurnSpeed = 1200f;
        public float idleTimeout = 5f;

        [Header("Attack Flag")]
        public bool canAttack;

        [Header("Reference")]
        public CameraSetting cameraSetting;
        public Damageable damageable;
        public MeleeWeapon weapon;
        public RandomAudioPlayer stepAudio;

        private const float groundAcceleration = 20f;
        private const float groundDeceleration = 25f;
        private const float stickingGravityProportion = 0.3f;
        private const float jumpAbortSpeed = 10f;
        private const float groundRayDistance = 1f;
        private const float minEnemyDotCutOff = 0.8f;
        private const float hitShakeRadius = 0.05f;
        private const float hitShakeTime = 0.4f;

        private float verticalSpeed = 0f;
        private float forwardSpeed = 0f;
        private float desireForwardSpeed = 0f;
        private float angleDiff = 0f;

        private bool isGround;
        private bool readyToJump = false;


        private bool previousIsGround;

        private bool IsInAttack
        {
            get
            {
                return !currentFrameInfo.isAnimatorTranslationing &&
                       currentFrameInfo.currentStateInfo.tagHash == hashAttackTag ||
                       currentFrameInfo.nextStateInfo.tagHash == hashAttackTag;
            }
        }

        private bool IsInDefense
        {
            get
            {
                return !currentFrameInfo.isAnimatorTranslationing &&
                       currentFrameInfo.currentStateInfo.tagHash == hashDefenseTag ||
                       currentFrameInfo.nextStateInfo.tagHash == hashDefenseTag;
            }
        }

        private PlayerInput playerInput;
        private Animator animator;
       // private CharacterController characterController;

        private Rigidbody rigibody;

        private Material currentWalkingSurface = null;

        private AnimatorInfo previousFrameInfo;
        private AnimatorInfo currentFrameInfo;

        private Quaternion targetRotation;

        private static readonly Collider[] overlapResultCache = new Collider[8];

        //Animator Parameter
        private static readonly int hashStateTime = Animator.StringToHash("stateTime");
        private static readonly int hashAttack = Animator.StringToHash("attack");
        private static readonly int hashForwardSpeed = Animator.StringToHash("forward");
        private static readonly int hashAngleDetlaRad = Animator.StringToHash("angleDetlaRad");
        private static readonly int hashDefense = Animator.StringToHash("defense");
        private static readonly int hashBlocked = Animator.StringToHash("blocked");
        private static readonly int hashDie = Animator.StringToHash("die");
        private static readonly int hashCounterBack = Animator.StringToHash("counterBack");
        private static readonly int hashFootFall = Animator.StringToHash("footFall");

        //State
        private static readonly int hashStateLocotiom = Animator.StringToHash("ground");

        //TAG
        private static readonly int hashBlockInputTag = Animator.StringToHash("BlockInput");
        private static readonly int hashAttackTag = Animator.StringToHash("Attack");
        private static readonly int hashDefenseTag = Animator.StringToHash("Defense");

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();
       //     characterController = GetComponent<CharacterController>();

            rigibody = GetComponent<Rigidbody>();

            damageable.Register(this);
            weapon.SetMaster(this.gameObject);

            MonoLinkedStateMachineBehviour<PlayerController>.Initialise(animator, this);
        }

        private void Update()
        {
            UpdatePlayerState();
        }

        private void UpdatePlayerState()
        {
            damageable.isInvnlerable = IsInDefense;

        }

        private void FixedUpdate()
        {
            RecodeAnimatorStateInfo();
            UpdateInputEnable();

            animator.SetFloat(hashStateTime, Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1));
            animator.ResetTrigger(hashAttack);

            if(playerInput.Attack && canAttack)
                animator.SetTrigger(hashAttack);

            if (playerInput.Defense && IsDefenseable())
                animator.SetTrigger(hashDefense);

            CalculateForwardMovement();
            CalculateVerticalMovement();

            SetTargetRotation();

            if (IsOrientationNeedUpdate() && playerInput.IsMoveInput)
                UpdateOrientation();

            PlayAudio();

            previousIsGround = isGround;
        }

        private void OnAnimatorMove()
        {
            Vector3 movement;

            if (isGround)
            {
                RaycastHit hitInfo;
                Ray ray = new Ray(transform.position + Vector3.up * groundRayDistance * 0.5f, -Vector3.up);
                if (Physics.Raycast(ray, out hitInfo, groundRayDistance, Physics.AllLayers,
                    QueryTriggerInteraction.Ignore))
                {
                    movement = Vector3.ProjectOnPlane(
                        animator.deltaPosition + (IsInAttack || IsInDefense ? Vector3.zero : this.transform.forward) * forwardSpeed,
                        hitInfo.normal);
                    movement *= Time.fixedDeltaTime;

                    Renderer goundRenderer = hitInfo.collider.GetComponentInChildren<Renderer>();
                    currentWalkingSurface = goundRenderer ? goundRenderer.sharedMaterial : null;
                }
                else
                {
                    movement = animator.deltaPosition + (IsInAttack || IsInDefense ? Vector3.zero : this.transform.forward) *
                               forwardSpeed * Time.fixedDeltaTime;

                    currentWalkingSurface = null;
                }
            }
            else
            {
                movement = forwardSpeed * transform.forward * Time.fixedDeltaTime;
            }

            rigibody.transform.rotation *= animator.deltaRotation;
        //    characterController.transform.rotation *= animator.deltaRotation;
        //    movement += verticalSpeed * Vector3.up * Time.fixedDeltaTime;

            rigibody.transform.position += movement;
            //   characterController.Move(movement);
            //   isGround = characterController.isGrounded;

            RaycastHit hit;
            Ray r = new Ray(transform.position + Vector3.up * groundRayDistance * 0.5f, -Vector3.up);
            isGround = Physics.Raycast(r, out hit, groundRayDistance, Physics.AllLayers,
                QueryTriggerInteraction.Ignore);

          //  Debug.DrawLine(transform.position + Vector3.up * groundRayDistance * 0.5f, transform.position - Vector3.up * groundRayDistance * 0.5f);

            animator.SetBool(ProjectConstant.AnimatorParameter.ON_GROUND, isGround);
        }

        private void PlayAudio()
        {
            float footCurve = animator.GetFloat(hashFootFall);
            if (footCurve > 0.01f && !stepAudio.isPlaying && stepAudio.canPlay)
            {
                stepAudio.isPlaying = true;
                stepAudio.canPlay = false;
                stepAudio.RandomPlay(currentWalkingSurface);
            }
            else if (stepAudio.isPlaying)
            {
                stepAudio.isPlaying = false;
            }
            else if (footCurve < 0.01f && !stepAudio.canPlay)
            {
                stepAudio.canPlay = true;
            }
        }

        private void RecodeAnimatorStateInfo()
        {
            previousFrameInfo.currentStateInfo = currentFrameInfo.currentStateInfo;
            previousFrameInfo.nextStateInfo = currentFrameInfo.nextStateInfo;
            previousFrameInfo.isAnimatorTranslationing = currentFrameInfo.isAnimatorTranslationing;

            currentFrameInfo.currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentFrameInfo.nextStateInfo = animator.GetNextAnimatorStateInfo(0);
            currentFrameInfo.isAnimatorTranslationing = animator.IsInTransition(0);

        }

        private void UpdateInputEnable()
        {
            bool inputBlock = currentFrameInfo.currentStateInfo.tagHash == hashBlockInputTag &&
                               !currentFrameInfo.isAnimatorTranslationing;
            inputBlock |= currentFrameInfo.nextStateInfo.tagHash == hashBlockInputTag;
            playerInput.InputEnable = !inputBlock;
        }

        private void CalculateForwardMovement()
        {
            desireForwardSpeed = playerInput.SignalValueMagic * maxForwardSpeed;
            float acceleration = playerInput.IsMoveInput ? groundAcceleration : groundDeceleration;

            forwardSpeed = Mathf.MoveTowards(forwardSpeed, desireForwardSpeed, acceleration * Time.fixedDeltaTime);
            animator.SetFloat(hashForwardSpeed, forwardSpeed);
        }

        private void CalculateVerticalMovement()
        {
            if (!playerInput.Jump && isGround)
                readyToJump = true;

            if (isGround)
            {
                verticalSpeed = -gravity * stickingGravityProportion;

                if (playerInput.Jump && readyToJump)
                {
                    verticalSpeed = jumpSpeed;
                    isGround = false;
                    readyToJump = false;
                }
            }
            else
            {
                if (!playerInput.Jump && verticalSpeed > 0f)
                    verticalSpeed -= jumpAbortSpeed * Time.fixedDeltaTime;

                if (Mathf.Approximately(verticalSpeed, 0f))
                    verticalSpeed = 0f;

                verticalSpeed -= gravity * Time.fixedDeltaTime;
            }
        }

        private void SetTargetRotation()
        {
            Vector2 moveInput = new Vector2(playerInput.UpValue, playerInput.RightValue);
            Vector3 locolMovementDirection = new Vector3(moveInput.y, 0, moveInput.x).normalized;

            Vector3 forward = Quaternion.Euler(0f, cameraSetting.CurrentCamera.m_XAxis.Value, 0f) * Vector3.forward;
            forward.y = 0f;
            forward.Normalize();

            Quaternion targetQuaternion;

            if (Mathf.Approximately(Vector3.Dot(locolMovementDirection, Vector3.forward), -1f))
            {
                targetQuaternion = Quaternion.LookRotation(-forward);
            }
            else
            {
                Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, locolMovementDirection);
                targetQuaternion = Quaternion.LookRotation(cameraToInputOffset * forward);
            }

            
            Vector3 resultingForword = targetQuaternion * Vector3.forward;

            if (IsInAttack)
            {
                Vector3 center = transform.position + transform.forward * 2.0f + transform.up;
                Vector3 halfExtends = new Vector3(3.0f,1.0f,2.0f);

                int count = Physics.OverlapBoxNonAlloc(center, halfExtends, overlapResultCache, targetQuaternion,
                    weapon.targetLayers);

                float closestDot = 0f;
                Vector3 closestForward = Vector3.zero;
                int closesetIndex = -1;

                for (int i = 0; i < count; i++)
                {
                    Vector3 playerToEnemy = overlapResultCache[i].transform.position - this.transform.position;
                    playerToEnemy.y = 0;

                    float d = Vector3.Dot(resultingForword, playerToEnemy.normalized);

                    if (d > minEnemyDotCutOff && d > closestDot)
                    {
                        closestForward = playerToEnemy;
                        closestDot = d;
                        closesetIndex = i;
                    }
                }

                if (closesetIndex != -1)
                {
                    resultingForword = closestForward;
                    transform.rotation = Quaternion.LookRotation(resultingForword);
                }

            }

            float angleCurrent = Mathf.Atan2(this.transform.forward.x, this.transform.forward.z) * Mathf.Deg2Rad;
            float targetAngle = Mathf.Atan2(resultingForword.x, resultingForword.z) * Mathf.Deg2Rad;

            angleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
            targetRotation = targetQuaternion;
        }

        private bool IsOrientationNeedUpdate()
        {
            bool updateOrientationForLocomtion = !currentFrameInfo.isAnimatorTranslationing &&
                                                 currentFrameInfo.currentStateInfo.shortNameHash == hashStateLocotiom ||
                                                 currentFrameInfo.nextStateInfo.shortNameHash == hashStateLocotiom;

            return updateOrientationForLocomtion && !IsInAttack;
        }

        private void UpdateOrientation()
        {
            animator.SetFloat(hashAngleDetlaRad, angleDiff * Mathf.Deg2Rad);

            Vector3 localInput = new Vector3(playerInput.SignalVec.x, 0f, playerInput.SignalVec.y);
            float groundTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, forwardSpeed / desireForwardSpeed);

            
            targetRotation = Quaternion.RotateTowards(this.transform.rotation,targetRotation,groundTurnSpeed * Time.fixedDeltaTime);
            this.transform.rotation = targetRotation;
        }

        private bool IsDefenseable()
        {
            return !currentFrameInfo.isAnimatorTranslationing &&
                   currentFrameInfo.currentStateInfo.normalizedTime > 0.5f &&
                   currentFrameInfo.currentStateInfo.tagHash == hashAttackTag ||
                   currentFrameInfo.nextStateInfo.tagHash == hashAttackTag;
        }

        #region call by animation event

        private void StartAttack()
        {
            weapon.StartAttack();
        }


        private void EndAttack()
        {

            weapon.EndAttack();
        }


        #endregion


        private struct AnimatorInfo
        {
            public AnimatorStateInfo currentStateInfo;
            public AnimatorStateInfo nextStateInfo;
            public bool isAnimatorTranslationing;
        }

        public void OnMessageReceiver(MessageType type, object sender, DamageData data)
        {
            switch (type)
            {
                case MessageType.DAMAGE:
                    ApplyDamage(data);
                    break;
                case MessageType.DIE:
                    Die();
                    break;
                case MessageType.INVNLERABLE:
                    Invnlerable(data);
                    break;
            }
        }

        private void Invnlerable(DamageData data)
        {
            if (IsInDefense)
            {
                Vector3 attackerPos = data.attacker.transform.position - this.transform.position;
                float angle = Vector3.Angle(this.transform.forward, attackerPos);
                if (angle < 30f)
                {
                    if (playerInput.Attack)
                    {
                        data.attacker.GetComponent<EnemyBehaviour>().Stuned();
                        StartAttack();
                        animator.SetTrigger(hashCounterBack);
                    }
                    else
                        animator.SetTrigger(hashBlocked);
                }
                else
                {
                    //TODO : GET FROCE AND STUNED
                }
                
                EndAttack();

            }
        }

        private void ApplyDamage(DamageData date)
        {
            CameraShake.Shake(hitShakeRadius, hitShakeTime);
        }

        private void Die()
        {
            animator.SetBool(hashDie, true);           
        }
    }
}
