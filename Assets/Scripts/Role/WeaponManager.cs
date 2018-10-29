using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class WeaponManager : IActorManager
    {

        private WeaponHandler weaponHandler;
        
        // Use this for initialization
        void Start ()
        {
            weaponHandler = GetComponent<WeaponHandler>();
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
