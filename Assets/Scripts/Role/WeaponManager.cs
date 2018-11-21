using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class WeaponManager : IActorStateManager
    {

        private WeaponData _weaponData;
        
        // Use this for initialization
        void Awake ()
        {
            _weaponData = GetComponentInChildren<WeaponData>();
            _weaponData.Manager = this;
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
            return _weaponData.Damage;
        }

        public CapsuleCollider GetWeaponCollider()
        {
            return _weaponData.GetComponent<CapsuleCollider>();
        }

       
    }
}
