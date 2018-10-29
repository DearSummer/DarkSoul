using UnityEngine;

namespace DS.Role
{
    public class WeaponHandler : MonoBehaviour
    {
        private CapsuleCollider weaponCollider;

        // Use this for initialization
        void Start ()
        {
            weaponCollider = GetComponentInChildren<CapsuleCollider>();
        }

    }
}
