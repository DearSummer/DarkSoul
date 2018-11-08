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

        public void OnEnter(GameObject player, ActorController controller)
        {
            controller.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {
        }

        public string GetStateName()
        {
            return "AttackState";
        }

        public void OnExit()
        {
        }
    }
}

