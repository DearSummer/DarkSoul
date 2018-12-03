using UnityEngine;
using UnityEngine.AI;

namespace DS.Game.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        public float speed = 5f;

        public bool interpolateTurning = false;
        public bool applyAnimationRotation = false;

        public Animator Animator
        {
            get { return animator; }
        }

        public NavMeshAgent Agent
        {
            get { return navMeshAgent; }
        }

        private Animator animator;      
        private NavMeshAgent navMeshAgent;

        private Vector3 externalForce;

        private bool followNvaMeshAgent;
        private bool underExternalForce = false;

        private bool externalForceAddGravity = true;
        private bool isOnGround;

        private new Rigidbody rigidbody;

        private const float groundRayDistance = 0.8f;


        private void OnEnable()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            rigidbody = GetComponentInChildren<Rigidbody>();
            if (rigidbody == null)
                rigidbody = gameObject.AddComponent<Rigidbody>();

            animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
            navMeshAgent.updatePosition = false;

            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            followNvaMeshAgent = true;

        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.up * groundRayDistance * 0.5f, -Vector3.up);
            isOnGround = Physics.Raycast(ray, out hit, groundRayDistance, Physics.AllLayers,
                QueryTriggerInteraction.Ignore);

            if(underExternalForce)
                FroceMovement();
        }

        private void FroceMovement()
        {
            if (externalForceAddGravity)
                externalForce += Physics.gravity * Time.fixedDeltaTime;

            Vector3 movement = externalForce * Time.deltaTime;
            RigibodyMove(movement,movement.sqrMagnitude);
            navMeshAgent.Warp(rigidbody.position);
        }

        private void OnAnimatorMove()
        {
            if (underExternalForce)
                return;

            if (followNvaMeshAgent)
            {
                navMeshAgent.speed = speed;
                transform.position = navMeshAgent.nextPosition + animator.deltaPosition;
            }
            else
            {
                Vector3 movement = animator.deltaPosition.Equals(Vector3.zero)
                    ? Vector3.forward * speed 
                    : animator.deltaPosition;
                RigibodyMove(movement, movement.sqrMagnitude);
            }

            if (applyAnimationRotation)
            {
                transform.forward = animator.deltaRotation * transform.forward;
            }
        }



        private void RigibodyMove(Vector3 movement,float maxDistance)
        {
            RaycastHit hitInfo;
            if (!rigidbody.SweepTest(movement.normalized, out hitInfo, maxDistance, QueryTriggerInteraction.Ignore))
            {
                rigidbody.MovePosition(rigidbody.position + movement);
            }
        }

        public void SetFollowNavMesh(bool follow)
        {
            if (!follow && navMeshAgent.enabled)
            {
                navMeshAgent.ResetPath();
            }
            else if (follow && !navMeshAgent.enabled)
            {
                navMeshAgent.Warp(transform.position);
            }

            navMeshAgent.enabled = follow;
            followNvaMeshAgent = follow;
        }

        public void SetTarget(Vector3 target)
        {
            navMeshAgent.destination = target;
        }

        public void SetForward(Vector3 forward)
        {
            Quaternion targetQuat = Quaternion.LookRotation(forward);

            if (interpolateTurning)
            {
                targetQuat = Quaternion.RotateTowards(this.transform.rotation, targetQuat,
                    navMeshAgent.angularSpeed * Time.fixedDeltaTime);
            }

            transform.rotation = targetQuat;
        }
    }
}
