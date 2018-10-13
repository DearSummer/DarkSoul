using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role.FSM
{
    public interface IPlayerState
    {
        void OnEnter(GameObject player, ActorController controller);
        void Update(PlayerInput inputSignal, Animator animator);
        void FixedUpdate(Rigidbody rigidbody);
        void OnExit();
    }
}

