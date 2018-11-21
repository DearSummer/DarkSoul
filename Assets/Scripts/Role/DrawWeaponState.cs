using DS.Role.FSM;
using UnityEngine;

namespace DS.Role
{
    public class DrawWeaponState : IPlayerState {

        public void OnEnter(GameObject player, ActorController controller)
        {
            
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {
            
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {
            
        }

        public string GetStateName()
        {
            return ProjectConstant.PlayerState.DRAW_WEAPON;
        }

        public void OnExit()
        {
            
        }
    }
}
