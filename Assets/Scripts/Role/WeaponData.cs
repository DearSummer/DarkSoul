using UnityEngine;

namespace DS.Role
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class WeaponData : MonoBehaviour
    {
        private CapsuleCollider weaponCollider;

        [SerializeField]
        private float damage;
        public float Damage
        {
            set { damage = value; }
            get { return Random.Range(-3, 3) + damage; }
        }

        [Range(0, 1)]
        public float critRate;
        public GameObject effect;

        public WeaponManager Manager { get; set; }
        // Use this for initialization
        void Start ()
        {
            weaponCollider = GetComponent<CapsuleCollider>();
            weaponCollider.isTrigger = true;

            this.gameObject.tag = ProjectConstant.Tag.WEAPON;
            this.gameObject.layer = LayerMask.NameToLayer(ProjectConstant.Layer.WEAPON);

            CloseWeaponCollider();
        }

        public void CloseWeaponCollider()
        {
            weaponCollider.enabled = false;
        }

        public GameObject GetEffect()
        {
            return effect;
        }

    }
}
