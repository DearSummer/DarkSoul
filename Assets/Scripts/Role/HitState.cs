using DS.Role.FSM;
using UnityEngine;

namespace DS.Role
{
    public class HitState : IPlayerState {


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

        public byte GetStateName()
        {
            return (byte) ProjectConstant.PlayerState.HIT;
        }

        public void OnExit()
        {
            
        }
    }
}
