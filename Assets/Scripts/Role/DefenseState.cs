using DS.Role.FSM;
using UnityEngine;

namespace DS.Role
{
    public class DefenseState : IPlayerState {


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
            return "DefenseState";
        }

        public void OnExit()
        {
        }
    }
}
