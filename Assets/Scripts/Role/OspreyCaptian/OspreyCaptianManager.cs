using DS.Role.Interface;
using UnityEngine;

namespace DS.Role.OspreyCaptian
{
    public class OspreyCaptianManager : IEnemyActorManager {


        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable)
        {
            base.TryGetHurt(wm,counterbackEnable);
            stateManager.AddHP(-wm.GetDamage());
        }
    }
}
