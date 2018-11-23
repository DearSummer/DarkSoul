using DS.Role.Interface;
using UnityEngine;

namespace DS.Role.Riko
{
    public class RikoManager : IActorManager
    {
        private void Start()
        {
            weaponManager.HideWeapon();
        }

        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable, Vector3 pos)
        {

            if (stateManager.isCounterBackEnable)
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.COUNTER_BACK);
                wm.GetActorManager().Stuned();
                return;
            }
    

            if (stateManager.isImmortal || stateManager.isDie)
                return;
            if (actorController.GetCurrentState() == ProjectConstant.PlayerState.DEFENSE)
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.BLOACKED);
            else if (stateManager.AddHP(-wm.GetDamage()))
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
            else
            {
                actorController.IssueBool(ProjectConstant.AnimatorParameter.DIE);
                battleManager.Enable(false);
            }

        }



    }
}
