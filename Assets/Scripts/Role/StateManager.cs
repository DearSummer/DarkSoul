using DS.Role.Interface;
using DS.UI;
using UnityEngine;

namespace DS.Role
{
    public class StateManager : IActorStateManager
    {

        private Animator _anim;

        public int hpMax;
        public float hp;

        public float toughness;

        private bool isOnGround;
        private bool isHit;
        private bool isAttack;     
        private bool isRoll;
        private bool isDefense;
        private bool isCounterBack;
        private bool isBlocked;
        public bool isDie;

        [Header("2rd state flag")]
        public bool isCounterBackEnable;
        public bool isImmortal;
        public bool isAttackEnable;
        public bool isStuned;
        public bool backStabEnable;

        public UnitFrame unitUI;

        private float stunedTime;
        private float stunedTimer;

        private void Start()
        {
            _anim = GetComponentInChildren<Animator>();
            AddHP(hpMax);
        }

        private void Update()
        {
            isDie = actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.DEATH;
            isAttack = actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.ATTACK;
            isDefense = actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.DEFENSE;
            isRoll = CheckState("roll");
            isHit = actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.HIT;
            isOnGround = _anim.GetBool(ProjectConstant.AnimatorParameter.ON_GROUND);
            isCounterBack = CheckState("CounterBack");
            isBlocked = CheckState("blocked");
   
            isCounterBackEnable = !isCounterBack && isDefense && _anim.GetBool("attack");
            isImmortal = isRoll || isCounterBack ;
            isAttackEnable = isOnGround && !isImmortal && !isDefense;
            backStabEnable = isAttackEnable && !isAttack;

            if (isStuned && actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.STUNED)
            {
                stunedTimer += Time.deltaTime;
                if (stunedTimer > stunedTime)
                {
                    actorManager.StaffStunedEnd();
                    isStuned = false;
                }

            }
            
        }

        public void StaffStuned(float time)
        {
            isStuned = true;
            stunedTime = time - time * (toughness / (toughness + 20));
            stunedTimer = 0f;
        }

        public bool AddHP(float value)
        {
            hp += value;
            hp = Mathf.Clamp(hp, 0, hpMax);

            if (unitUI != null)
                unitUI.SetPrimary(hp / hpMax);
            return hp > 0;
        }

        private bool CheckState(string anim, string layer = "Base")
        {
            var currentInfo = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex(layer));
            return currentInfo.IsName(anim);
        }

        private bool CheckStateTag(string anim, string layer = "Base")
        {
            var currentInfo = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex(layer));
            return currentInfo.IsTag(anim);
        }
    }
}
