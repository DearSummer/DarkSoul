using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class ActorManager : IActorManager
    {

        private void Update()
        {
            if (stateManager.isCounterBackEnable && actorController.InputSignal.Attack)
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.COUNTER_BACK);
            }

        }

        public override void TryGetHurt(float damage)
        {
            if (stateManager.isImmortal || stateManager.isDie)
                return;
            if (actorController.GetCurrentState() == ProjectConstant.PlayerState.DEFENSE)
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.BLOACKED);
            else if (stateManager.AddHP(-damage))
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
            else
            {
                actorController.IssueBool(ProjectConstant.AnimatorParameter.DIE);
                battleManager.Enable(false);
            }

        }

    }
}
