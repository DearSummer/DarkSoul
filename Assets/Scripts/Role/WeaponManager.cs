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

        public float GetDamage()
        {
            return _weaponData.damage;
        }

        public CapsuleCollider GetWeaponCollider()
        {
            return _weaponData.GetComponent<CapsuleCollider>();
        }

       
    }
}
