using DS.Role.FSM;
using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role
{
    public class AttackState : IPlayerState
    {
        private bool isStubBack;

        public void OnEnter(GameObject player, ActorController controller)
        {
            controller.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {
            isStubBack = animator.GetCurrentAnimatorStateInfo(0).IsName("BACK_STAB");
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {
        }

        public string GetStateName()
        {
            return isStubBack ? ProjectConstant.PlayerState.BACK_STAB : ProjectConstant.PlayerState.ATTACK;
        }

        public void OnExit()
        {
        }
    }
}

