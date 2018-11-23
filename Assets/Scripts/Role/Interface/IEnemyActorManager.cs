using DS.UI;
using UnityEngine;

namespace DS.Role.Interface
{
    public class IEnemyActorManager : IActorManager
    {

        public GameObject hpUI;
        public new string name;

        public float hpOffset;
        private GameObject hp;

        private void Start()
        {
            hp = ObjectPool.Instance.Get(hpUI);
            Canvas parent = GameObject.FindObjectOfType<Canvas>();
            hp.transform.SetParent(parent.transform);

            UnitFrameEnemy unitEnemyUI = hp.GetComponent<UnitFrameEnemy>();
            stateManager.unitUI = unitEnemyUI;

            unitEnemyUI.SetName(name);
        }

        private void Update()
        {
            hp.transform.position =
                UnityEngine.Camera.main.WorldToScreenPoint(this.transform.position +
                                                           this.transform.up * hpOffset);
        }


        public override void TryGetHurt(WeaponManager wm, bool counterbackEnable, Vector3 pos)
        {

            if (wm.GetActorManager().GetCurrentStateName() == ProjectConstant.PlayerState.BACK_STAB)
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
                actorController.IssueBool(ProjectConstant.AnimatorParameter.STAFF_STUNED);
                stateManager.StaffStuned(5f);
            }

            if (!stateManager.AddHP(-GetHurtDamage(wm.GetDamage())))
            {
                actorController.IssueTrigger(ProjectConstant.AnimatorParameter.DIE);
                battleManager.Enable(false);
            }

            DamageUIManager.Instance.SetDamage((int)GetHurtDamage(wm.GetDamage()), pos,
                wm.IsCrit ? ColorSet.critColor : ColorSet.normalColor);

        }

        

        protected virtual float GetHurtDamage(float damage)
        {
            return damage;
        }


        public bool BackStabEnable()
        {           
            return stateManager.backStabEnable;
        }

    }
}
