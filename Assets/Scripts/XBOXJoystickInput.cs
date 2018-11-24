using System;
using UnityEngine;

namespace DS
{
    public class XBOXJoystickInput : PlayerInput
    {

        [Header("-------key----------")]
        public string keyX = "joystick button 2";
        public string keyY = "joystick button 3";
        public string keyB = "joystick button 1";
        public string keyA = "joystick button 0";

        [Header("----------ex key---------")]
        public string keyR1 = "joystick button 5";
        public string keyR2 = "RT Axis";
        public string keyR3 = "joystick button 9";
        public string keyL1 = "joystick button 4";
        public string keyL2 = "LT Axis";
        public string keyL3 = "joystick button 8";

        private readonly ButtonSignal _btnX = new ButtonSignal();
        private readonly ButtonSignal _btnY = new ButtonSignal();
        private readonly ButtonSignal _btnA = new ButtonSignal();
        private readonly ButtonSignal _btnB = new ButtonSignal();
        private readonly ButtonSignal _btnR1 = new ButtonSignal();
        private readonly ButtonSignal _btnR2 = new ButtonSignal();
        private readonly ButtonSignal _btnR3 = new ButtonSignal();
        private readonly ButtonSignal _btnL1 = new ButtonSignal();
        private readonly ButtonSignal _btnL2 = new ButtonSignal();
        private readonly ButtonSignal _btnL3 = new ButtonSignal();


        protected override void Update()
        {
            base.Update();

            InputHandler();


            HandleActionSignal();

        
        }

        private void HandleActionSignal()
        {
            Roll = _btnA.IsDelaying && _btnA.OnRelease;
            Jump = _btnA.OnPressed && _btnA.IsExtending;
            Attack = _btnX.OnPressed;
            Run = (_btnA.IsPressing && !_btnA.IsDelaying) || _btnA.IsExtending;

            LockOn = _btnL2.OnPressed;
        }

        private void InputHandler()
        {
            targetUpValue = Input.GetAxis("Left Y Axis");
            targetRightValue = Input.GetAxis("Left X Axis");

            targetCRightValue = -Input.GetAxis("Right X Axis");
            targetCUpValue = Input.GetAxis("Right Y Axis");        

            _btnA.Tick(Input.GetKey(keyA));
            _btnB.Tick(Input.GetKey(keyB));
            _btnX.Tick(Input.GetKey(keyX));
            _btnY.Tick(Input.GetKey(keyY));

            _btnL1.Tick(Input.GetKey(keyL1));
            _btnL2.Tick(Math.Abs(Input.GetAxis(keyL2)) > 0);
            _btnL3.Tick(Input.GetKey(keyL3));
            _btnR1.Tick(Input.GetKey(keyR1));
            _btnR2.Tick(Math.Abs(Input.GetAxis(keyR2)) > 0);
            _btnR3.Tick(Input.GetKey(keyR3));

        
        }



    }
}
