using DS.Game.Camera;
using DS.Game.Core;
using DS.Game.DamageSystem;
using DS.Game.Message;
using DS.Game.Player;
using DS.Game.UI;
using DS.Game.Utils;
using DS.Game.Weapon;
using DS.Runtime;
using UnityEngine;

namespace DS.Game.Enemy.OspreyCaptian
{

    public class OspreyCaptianBehaviour : MonoBehaviour, IMessageReceiver<DamageData>
    {

        public static readonly int hashPursuit = Animator.StringToHash("InPursuit");
        public static readonly int hashAttack = Animator.StringToHash("attack");
        public static readonly int hashSpeed = Animator.StringToHash("speed");
        public static readonly int hashDie = Animator.StringToHash("die");

        public EnemyController Controller { private set; get; }
        public PlayerController Target { private set; get; }

        public new string name;
        public float hpOffset;
        public GameObject hpUI;

        public TargetScanner playerScanner;
        public float meleeRange = 4f;

        [Header("Refence")] public MeleeWeapon meleeWeapon;
        public Damageable damageable;


        private void Awake()
        {
            Controller = GetComponent<EnemyController>();
            meleeWeapon.SetMaster(this.gameObject);
            damageable.Register(this);

            MonoLinkedStateMachineBehviour<OspreyCaptianBehaviour>.Initialise(Controller.Animator, this);
        }


        public void ScanTarget()
        {
            Target = playerScanner.Detect(this.transform, Target == null);
        }

        private void StartAttack()
        {
            meleeWeapon.StartAttack();
        }

        private void EndAttack()
        {
            meleeWeapon.EndAttack();
        }

        public bool OrientTowardTarget()
        {
            Vector3 v = Target.transform.position - this.transform.position;
            v.y = 0;

            float angle = Vector3.SignedAngle(transform.forward, v, Vector3.up);

            if (Mathf.Abs(angle) < 20)
            {
                transform.forward = v.normalized;
                return true;
            }

            return false;
        }

        public void StartPursuit()
        {
            Controller.Animator.SetBool(hashPursuit, true);
        }


        public void StopPursuit()
        {
            Controller.Animator.SetBool(hashPursuit, false);
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            playerScanner.DrawGizmo(this.transform);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, meleeRange);
        }
#endif
        public void OnMessageReceiver(MessageType type, object sender, DamageData data)
        {
            switch (type)
            {
                case MessageType.DAMAGE:
                    ApplyDamage(data);
                    break;
                case MessageType.DIE:
                    Die(data);
                    break;
            }
        }

        private void Die(DamageData d)
        {
            Controller.Animator.SetTrigger(hashDie);
        }

        private void ApplyDamage(DamageData d)
        {
            DamageUIManager.Instance.SetDamage(d);
        }
    }
}
