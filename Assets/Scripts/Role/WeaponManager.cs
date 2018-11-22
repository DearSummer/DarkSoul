using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class WeaponManager : IActorStateManager
    {

        private WeaponData _weaponData;

        public bool IsCrit
        {
            get;
            set;
        }
        
        // Use this for initialization
        void Awake ()
        {
            _weaponData = GetComponentInChildren<WeaponData>();
            _weaponData.Manager = this;

            IsCrit = false;
        }

        public void HideWeapon()
        {
            _weaponData.gameObject.SetActive(false);
        }

        public void DisplayWeapon()
        {
            _weaponData.gameObject.SetActive(true);
        }

        public float GetDamage()
        {
            if (!IsCrit)
            {
                if (_weaponData.critRate > Random.Range(0f, 1f))
                {
                    IsCrit = true;
                }
            }
            return _weaponData.Damage * (IsCrit ? 2 : 1);
        }

        public CapsuleCollider GetWeaponCollider()
        {
            return _weaponData.GetComponent<CapsuleCollider>();
        }

       
    }
}
