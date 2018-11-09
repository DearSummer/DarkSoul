using UnityEngine;

namespace DS.Role
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class WeaponData : MonoBehaviour
    {
        private CapsuleCollider weaponCollider;
        public float damage;

        // Use this for initialization
        void Start ()
        {
            weaponCollider = GetComponent<CapsuleCollider>();
        }

    }
}
