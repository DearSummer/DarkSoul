using DS.Role.FSM;
using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    namespace DS.Role
    {
        public class PlayerFiniteStateMachine : IMachine
        {

            private IPlayerState _curPlayerState;

            private readonly GameObject _player;
            private readonly ActorController _controller;
            private readonly Animator _animator;
            private readonly Rigidbody _rigidbody;
            private readonly PlayerInput _playerInput;

            public PlayerFiniteStateMachine(GameObject player,ActorController actorController,IPlayerState fristPlayerState, PlayerInput playerInput, Animator animator,
                Rigidbody rigidbody)
            {
                _curPlayerState = fristPlayerState;
                this._animator = animator;
                this._rigidbody = rigidbody;
                this._playerInput = playerInput;
                this._player = player;
                this._controller = actorController;

                fristPlayerState.OnEnter(_player,_controller);
            }


            public void Update()
            {
                _curPlayerState.Update(_playerInput, _animator);
            }

            public void FixUpdate()
            {
                _curPlayerState.FixedUpdate(_rigidbody);


                _rigidbody.position += _controller.AnimMovePos;
                _controller.AnimMovePos = Vector3.zero;
            
            }

            public void TranslateTo(IPlayerState newPlayerState)
            {
                if(newPlayerState == _curPlayerState)
                    return;

                _curPlayerState.OnExit();
                _curPlayerState = newPlayerState;
                _curPlayerState.OnEnter(_player,_controller);
            }

            public IPlayerState GetCurrentState()
            {
                return _curPlayerState;
            }

            public string GetCurrentStateName()
            {
                return _curPlayerState.GetStateName();
            }
        }
    }

