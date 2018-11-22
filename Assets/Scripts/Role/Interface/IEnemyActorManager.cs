using UnityEngine;

namespace DS.Role.Interface
{
    public class IEnemyActorManager : IActorManager {


        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable)
        {
            if (wm.GetActorManager().GetCurrentStateName() == ProjectConstant.PlayerState.BACK_STAB)
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
                actorController.IssueBool(ProjectConstant.AnimatorParameter.STAFF_STUNED);
                stateManager.StaffStuned(5f);
            }
                
        }




    }
}
