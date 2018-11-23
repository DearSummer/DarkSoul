using UnityEngine;

namespace DS.Role
{
    public class AttackRange : MonoBehaviour
    {

        private EnemyInput input;

        private void Start()
        {
            input = GetComponentInParent<EnemyInput>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(ProjectConstant.Tag.PLAYER))
                input.InAttackRange();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ProjectConstant.Tag.PLAYER))
                input.OutOfAttackRange();
        }
    }
}
