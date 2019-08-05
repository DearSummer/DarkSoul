using UnityEngine;

namespace DS
{
    public class PlayerInput : MonoBehaviour {

        public bool LockOn
        {
            protected set;
            get;
        }

        public bool Roll
        {
            protected set;
            get;
        }

        public bool Run
        {
            protected set;
            get;
        }

        public bool Jump
        {
            protected set;
            get;
        }

        public bool Attack
        {
            protected set;
            get;
        }

        public bool Defense
        {
            protected set;
            get;
        }

        public bool Combo
        {
            protected set;
            get;
        }

        public bool WrapStrike
        {
            protected set;
            get;
        }

        //摄像机的上方向信号量
        public float CUpValue
        {
            protected set;
            get;
        }

        //摄像机的右方向信号量
        public float CRightValue
        {
            protected set;
            get;
        }

        public float UpValue
        {
            protected set;
            get;
        }

        public float RightValue
        {
            protected set;
            get;
        }

        public float SignalValueMagic
        {
            protected set;
            get;
        }

        public Vector3 SignalForwardVec
        {
            protected set;
            get;
        }

        public Vector2 SignalVec
        {
            protected set;
            get;
        }

        [SerializeField]
        [Header("-------enable-------")]
        protected bool inputEnable = true;

        public bool InputEnable
        {
            get
            {
                return inputEnable;
            }
            set
            {
                inputEnable = value;
            }
        }

        public bool IsMoveInput
        {
            get { return !(Mathf.Approximately(targetUpValue, 0f) && Mathf.Approximately(targetRightValue, 0f)); }
        }


        protected  virtual void Update()
        {
            if (!inputEnable)
            {
                targetRightValue = 0f;
                targetUpValue = 0f;
            }

            CalculationAxisSignal();
        }

        /// <summary>
        /// 将直角坐标系下的坐标映射为相应的圆的坐标
        /// </summary>
        /// <param name="input">直角坐标系下的坐标</param>
        /// <returns>相应的圆的坐标系的坐标</returns>
        protected Vector2 SquareToCircle(Vector2 input)
        {
            Vector2 result = Vector2.zero;
            result.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
            result.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

            return result;
        }

        protected float targetUpValue;
        protected float targetRightValue;
        protected float targetCUpValue;
        protected float targetCRightValue;

        private float _upValueVelocity;       
        private float _rightValueVelocity;
        private float _cUpValueVelocity;
        private float _cRightValueVelocity;

        protected void CalculationAxisSignal()
        {
            UpValue = Mathf.SmoothDamp(UpValue, targetUpValue, ref _upValueVelocity, 0.1f);
            RightValue = Mathf.SmoothDamp(RightValue, targetRightValue, ref _rightValueVelocity, 0.1f);

            CUpValue = Mathf.SmoothDamp(CUpValue, targetCUpValue, ref _cUpValueVelocity, 0.3f);
            CRightValue = Mathf.SmoothDamp(CRightValue, targetCRightValue, ref _cRightValueVelocity, 0.3f);

            Vector2 temp = SquareToCircle(new Vector2(UpValue, RightValue));

            SignalValueMagic = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);
            SignalForwardVec = temp.x * this.transform.forward + temp.y * this.transform.right;
            SignalVec = new Vector2(temp.x,temp.y);
        }
    }
}
