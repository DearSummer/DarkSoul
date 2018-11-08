using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class BattleManager : IActorManager
    {

        private CapsuleCollider _collider;


        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
            _collider.center = Vector3.up * 0.79f;
            _collider.radius = 0.2f;
            _collider.height = 1.58f;
            _collider.isTrigger = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ProjectConstant.Tag.WEAPON))
            {
                RikoManager.TryGetHurt();
            }
        }

        public void Enable(bool enable)
        {
            _collider.enabled = enable;
        }
    }
}
