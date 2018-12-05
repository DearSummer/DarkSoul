using DS.Game.Effect;
using DS.Game.UI;
using DS.Role.Interface;
using UnityEngine;

namespace DS.Role.Riko
{
    public class RikoManager : IActorManager
    {

       // public SkillEffect skillEffect;

        private void Start()
        {
            weaponManager.HideWeapon();
        }

        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable, Vector3 pos)
        {

            if (stateManager.isCounterBackEnable)
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.COUNTER_BACK);
                //GameObject effect = FightingParticleManager.Instance.ShowEffect(skillEffect.GetEffectObject(), this.transform.position);
               // effect.GetComponent<SkillEffect>().SetColor(ColorSet.counterBackColor);
                wm.GetActorManager().Stuned();
                return;
            }
    

            if (stateManager.isImmortal || stateManager.isDie)
                return;
            if (actorController.GetCurrentState() == (byte) ProjectConstant.PlayerState.DEFENSE)
            {                
               // GameObject effect = FightingParticleManager.Instance.ShowEffect(skillEffect.GetEffectObject(), this.transform.position);
               // effect.GetComponent<SkillEffect>().SetColor(Color.red);
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.BLOACKED);               
            }               
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
