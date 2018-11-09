using DS.Role.FSM;
using UnityEngine;

namespace DS.Role
{
    public class DeathState : IPlayerState {
        public void OnEnter(GameObject player, ActorController controller)
        {
            controller.GetComponent<Rigidbody>().velocity = Vector3.zero;
            controller.InputSignal.InputEnable = false;
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {
        }

        public string GetStateName()
        {
            return ProjectConstant.PlayerState.DEATH;
        }

        public void OnExit()
        {
        }
    }
}
