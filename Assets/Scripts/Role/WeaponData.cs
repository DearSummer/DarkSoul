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
            weaponCollider.isTrigger = true;

            this.gameObject.tag = ProjectConstant.Tag.WEAPON;
            this.gameObject.layer = LayerMask.NameToLayer(ProjectConstant.Layer.WEAPON);
        }

        public void CloseWeaponCollider()
        {
            weaponCollider.enabled = false;
        }

    }
}
