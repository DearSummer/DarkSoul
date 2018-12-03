using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------


namespace DS
{
    public class KeyBoardInput : PlayerInput
    {


        [Header("-----key setting----")] public string keyUp;
        public string keyDown;
        public string keyLeft;
        public string keyRight;

        [Header("--------camera key setting----")]
        public string cKeyUp;

        public string cKeyDown;
        public string cKeyLeft;
        public string cKeyRight;

        [Header("-----run key------")] public string keyA;
        [Header("------jump key-----")] public string keyB;

        [Header("-----attack key setting----")]
        public string keyC;
        public string keyD;
        public string keyE;

        [Header("-------mouse enable------")]
        public bool mouseEnable = true;

        private readonly ButtonSignal _btnA = new ButtonSignal();
        private readonly ButtonSignal _btnB = new ButtonSignal();
        private readonly ButtonSignal _btnC = new ButtonSignal();
        private readonly ButtonSignal _btnD = new ButtonSignal();

        private readonly ButtonSignal _btnE = new ButtonSignal();

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            InputHandler();


            HandleAction();

        }

        private void HandleAction()
        {
            Run = (!_btnA.IsDelaying && _btnA.IsPressing);
            //Jump = _btnA.IsExtending && _btnA.OnPressed;
            Roll = _btnA.IsDelaying && (_btnA.OnRelease || _btnA.IsExtending);
            Attack = _btnC.OnPressed || _btnC.IsDelaying;
            Defense = _btnD.IsPressing;
            LockOn = _btnB.OnPressed;
            Combo = _btnE.OnPressed;
        }


        /// <summary>
        /// 控制输入的信息
        /// </summary>
        private void InputHandler()
        {
            targetUpValue = (Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f);
            targetRightValue = (Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f);

            if (mouseEnable)
            {
                targetCUpValue = Input.GetAxis("Mouse Y");
                targetCRightValue = Input.GetAxis("Mouse X");
            }
            else
            {
                targetCUpValue = (Input.GetKey(cKeyUp) ? 1.0f : 0f) - (Input.GetKey(cKeyDown) ? 1.0f : 0f);
                targetCRightValue = (Input.GetKey(cKeyRight) ? 1.0f : 0f) - (Input.GetKey(cKeyLeft) ? 1.0f : 0f);
            }


            _btnA.Tick(Input.GetKey(keyA));
            _btnB.Tick(Input.GetKey(keyB));
            _btnC.Tick(Input.GetKey(keyC));
            _btnD.Tick(Input.GetKey(keyD));
            _btnE.Tick(Input.GetKey(keyE));
        }

    }
}

