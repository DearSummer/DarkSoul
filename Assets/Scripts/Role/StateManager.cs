using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class StateManager : IActorStateManager
    {

        private Animator _anim;

        public int hpMax;
        public float hp;

        [Header("1st state flag")]
        public bool isOnGround;
        public bool isHit;
        public bool isAttack;
        public bool isDie;
        public bool isRoll;
        public bool isDefense;
        public bool isCounterBack;
        public bool isBlocked;

        [Header("2rd state flag")]
        public bool isCounterBackEnable;
        public bool isImmortal;
        public bool isAttackEnable;

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
            isRoll = CheckState("Roll");
            isHit = actorManager.GetCurrentStateName() == ProjectConstant.PlayerState.HIT;
            isOnGround = _anim.GetBool(ProjectConstant.AnimatorParameter.ON_GROUND);
            isCounterBack = CheckState("CounterBack");
            isBlocked = CheckState("blocked");

            isCounterBackEnable = !isCounterBack && isDefense;
            isImmortal = isRoll || isCounterBack;
            isAttackEnable = isOnGround && !isImmortal && !isDefense;
        }

        public bool AddHP(float value)
        {
            hp += value;
            hp = Mathf.Clamp(hp, 0, hpMax);

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
