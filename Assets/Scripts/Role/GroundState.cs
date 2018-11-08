using DS.Role.FSM;
using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role
{
    public class GroundState : IPlayerState
    {
        private Vector3 _movingVec;

        private GameObject _player;
        private ActorController _controller;

        public void OnEnter(GameObject player, ActorController controller)
        {
            this._player = player;
            _controller = controller;
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {

            if (inputSignal.SignalValueMagic > 0.1f)
            {
                _player.transform.forward = Vector3.Slerp(_player.transform.forward, inputSignal.SignalVec, 0.1f);
            }

            _movingVec = inputSignal.SignalValueMagic * _player.transform.forward *
                         (inputSignal.Run ? _controller.runVelocity : _controller.moveVelocity);
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {       
            rigidbody.velocity = new Vector3(_movingVec.x, rigidbody.velocity.y, _movingVec.z);
        }

        public string GetStateName()
        {
            return "GroundState";
        }

        public void OnExit()
        {

        }
    }
}

