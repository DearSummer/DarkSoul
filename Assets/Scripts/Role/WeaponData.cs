using UnityEngine;

namespace DS.Role
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class WeaponData : MonoBehaviour
    {
        private CapsuleCollider weaponCollider;
        public float damage;

        public WeaponManager Manager { get; set; }
        // Use this for initialization
        void Start ()
        {
            weaponCollider = GetComponent<CapsuleCollider>();
        }

        public void CloseWeaponCollider()
        {
            weaponCollider.enabled = false;
        }

    }
}
