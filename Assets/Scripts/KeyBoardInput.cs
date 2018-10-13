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

        [Header("-------mouse enable------")]
        public bool mouseEnable = true;

        private readonly ButtonSignal _btnA = new ButtonSignal();
        private readonly ButtonSignal _btnB = new ButtonSignal();
        private readonly ButtonSignal _btnC = new ButtonSignal();
        private readonly ButtonSignal _btnD = new ButtonSignal();

        private float _targetUpValue;
        private float _targetRightValue;

        private float _targetCUpvalue;
        private float _targetCRightValue;

        private float _velocityUpValue;
        private float _velocityRightValue;

        private float _velocityCUpValue;
        private float _velocityCRightValue;



        // Update is called once per frame
        void Update()
        {
            InputHandler();

            if (!inputEnable)
            {
                _targetRightValue = 0f;
                _targetUpValue = 0f;
            }

            HandleAction();
            CalculatorAxisValue();
        }

        private void HandleAction()
        {
            Run = (!_btnA.IsDelaying && _btnA.IsPressing) || _btnA.IsExtending;
            Jump = _btnA.IsExtending && _btnA.OnPressed;
            Roll = _btnA.IsDelaying && _btnA.OnRelease;
            Attack = _btnC.OnPressed;
            LockOn = _btnB.OnPressed;
        }

        private void CalculatorAxisValue()
        {
            UpValue = Mathf.SmoothDamp(UpValue, _targetUpValue, ref _velocityUpValue, 0.1f);
            RightValue = Mathf.SmoothDamp(RightValue, _targetRightValue, ref _velocityRightValue, 0.1f);

            CUpValue = Mathf.SmoothDamp(CUpValue, _targetCUpvalue, ref _velocityCUpValue, 0.3f);
            CRightValue = Mathf.SmoothDamp(CRightValue, _targetCRightValue, ref _velocityCRightValue, 0.3f);


            Vector2 temp = SquareToCircle(new Vector2(UpValue, RightValue));

            SignalValueMagic = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);
            SignalVec = temp.x * this.transform.forward + temp.y * this.transform.right;
        }

        /// <summary>
        /// 控制输入的信息
        /// </summary>
        private void InputHandler()
        {
            _targetUpValue = (Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f);
            _targetRightValue = (Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f);

            if (mouseEnable)
            {
                _targetCUpvalue = Input.GetAxis("Mouse Y");
                _targetCRightValue = Input.GetAxis("Mouse X");
            }
            else
            {
                _targetCUpvalue = (Input.GetKey(cKeyUp) ? 1.0f : 0f) - (Input.GetKey(cKeyDown) ? 1.0f : 0f);
                _targetCRightValue = (Input.GetKey(cKeyRight) ? 1.0f : 0f) - (Input.GetKey(cKeyLeft) ? 1.0f : 0f);
            }


            _btnA.Tick(Input.GetKey(keyA));
            _btnB.Tick(Input.GetKey(keyB));
            _btnC.Tick(Input.GetKey(keyC));
            _btnD.Tick(Input.GetKey(keyD));
        }

    }
}

