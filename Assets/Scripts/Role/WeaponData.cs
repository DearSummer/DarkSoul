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
        
        public int effectIndexMin;
        public int effectIndexMax;

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

        public int GetEffectIndex()
        {
            return Random.Range(effectIndexMin, effectIndexMax);
        }

    }
}
