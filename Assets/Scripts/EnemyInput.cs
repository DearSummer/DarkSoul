using UnityEngine;

namespace DS
{
    public class EnemyInput : PlayerInput {

        // Use this for initialization
        void Start ()
        {
            Attack = true;
            targetUpValue = 0.0f;
            targetCRightValue = 0.0f;
        }
	
        // Update is called once per frame
        void Update () {
		    CalculationAxisSignal();
        }


    }
}
