using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class BattleManager : IActorStateManager
    {

        private CapsuleCollider _collider;


        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
//            _collider.center = Vector3.up * 0.79f;
//            _collider.radius = 0.2f;
//            _collider.height = 1.58f;
//            _collider.isTrigger = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag(ProjectConstant.Tag.WEAPON))
            {
                WeaponData wd = other.GetComponent<WeaponData>();

                GameObject attacker = wd.Manager.GetActorManager().gameObject;
                GameObject receiver = this.gameObject;

                Vector3 attackerForward = wd.Manager.GetActorManager().GetForwardVec();
                Vector3 receiverForward = GetActorManager().GetForwardVec();

                Vector3 attackDir = receiver.transform.position - attacker.transform.position;
                float attackAngle = Vector3.Angle(attackDir, attackerForward);

                Vector3 receiverDir = attacker.transform.position - receiver.transform.position;
                float receiverAngle = Vector3.Angle(receiverForward, receiverDir);
                float counterbackAngle = Vector3.Angle(receiverForward, attackerForward);

                if (attackAngle < 45f)
                {
                    Debug.Log(receiverAngle);
                    actorManager.TryGetHurt(other.GetComponent<WeaponData>().Manager,
                        (receiverAngle < 45f && Mathf.Abs(counterbackAngle - 180) < 20F));
                    wd.CloseWeaponCollider();
                    
                }

            }
        }

        public void Enable(bool enable)
        {
            _collider.enabled = enable;
        }
    }
}
