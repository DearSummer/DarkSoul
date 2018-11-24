using DS.Role.FSM;
using UnityEngine;

namespace DS.Role
{
    public class DrawWeaponState : IPlayerState
    {


        public void OnEnter(GameObject player, ActorController controller)
        {
            player.GetComponent<Animator>().SetBool(ProjectConstant.AnimatorParameter.DRAW_WEAPON, true);
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
            return (byte) ProjectConstant.PlayerState.DRAW_WEAPON;
        }

        public void OnExit()
        {
            
        }
    }
}
