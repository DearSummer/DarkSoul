using DS.Role.Interface;
using UnityEngine;

namespace DS.Role.OspreyCaptian
{
    public class OspreyCaptianManager : IEnemyActorManager {

        protected override float GetHurtDamage(float damage)
        {
            return damage * 1.2f;
        }

        
    }
}
