
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    public class AirState : IPlayerState
    {
        private Vector3 _movingVec;
        private Vector3 _jumpVec;

        private bool _isOnGround = false;

        private ActorController _actor;
        private float _checkTime = 0.5f;
        private float _timer = 0f;

        public void OnEnter(GameObject player, ActorController controller)
        {
            var input = player.GetComponentInParent<KeyBoardInput>();
            _movingVec = input.SignalValueMagic * player.transform.forward *
                        (input.Run ? controller.runVelocity : controller.moveVelocity);

            _jumpVec = new Vector3(0, controller.jumpVelocity, 0);

            _actor = controller;
        }

        public void Update(PlayerInput inputSignal, Animator animator)
        {
            if (_timer < _checkTime)
            {
                _timer += Time.deltaTime;
                return;
            }

            _isOnGround = animator.GetBool(ProjectConstant.AnimatorParameter.ON_GROUND);
        }

        public void FixedUpdate(Rigidbody rigidbody)
        {            
            rigidbody.velocity = new Vector3(_movingVec.x, rigidbody.velocity.y, _movingVec.z) + _jumpVec;
            if (!(_jumpVec == Vector3.zero))
            {
                _jumpVec = Vector3.zero;
            }


            if (_isOnGround)
            {
                _movingVec = Vector3.zero;
            }
        }


        /// <summary>
        /// ÍË³ö×´Ì¬Ê±ÖØÖÃ×´Ì¬
        /// </summary>
        public void OnExit()
        {
            _isOnGround = false;
            _timer = 0f;
        }


    }


