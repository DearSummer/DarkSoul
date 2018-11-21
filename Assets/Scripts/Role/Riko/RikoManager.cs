using DS.Role.Interface;

namespace DS.Role.Riko
{
    public class RikoManager : IActorManager
    {
        private void Start()
        {
            weaponManager.HideWeapon();
        }

        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable)
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
