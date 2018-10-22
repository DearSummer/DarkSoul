using UnityEngine;

namespace DS
{
    public class EnemyInput : PlayerInput {
        private float _targetUpValue;
        private float _upValueVelocity;
        private float _targetRightValue;
        private float _targetCUpValue;
        private float _targetCRightValue;
        private float _rightValueVelocity;
        private float _cUpValueVelocity;
        private float _cRightValueVelocity;

        // Use this for initialization
        void Start ()
        {
            Attack = true;
        }
	
        // Update is called once per frame
        void Update () {
		    CalculationAxisSignal();
        }


        private void CalculationAxisSignal()
        {
            UpValue = Mathf.SmoothDamp(UpValue, _targetUpValue, ref _upValueVelocity, 0.1f);
            RightValue = Mathf.SmoothDamp(RightValue, _targetRightValue, ref _rightValueVelocity, 0.1f);

            CUpValue = Mathf.SmoothDamp(CUpValue, _targetCUpValue, ref _cUpValueVelocity, 0.3f);
            CRightValue = Mathf.SmoothDamp(CRightValue, _targetCRightValue, ref _cRightValueVelocity, 0.3f);

            Vector2 temp = SquareToCircle(new Vector2(UpValue, RightValue));

            SignalValueMagic = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);
            SignalVec = temp.x * this.transform.forward + temp.y * this.transform.right;

        }
    }
}
