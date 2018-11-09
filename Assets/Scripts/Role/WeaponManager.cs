using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class WeaponManager : IActorManager
    {

        private WeaponData _weaponData;
        
        // Use this for initialization
        void Start ()
        {
            _weaponData = GetComponentInChildren<WeaponData>();
        }

        public float GetDamage()
        {
            return _weaponData.damage;
        }
    }
}
