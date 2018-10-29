using UnityEngine;

namespace DS
{
    public class EnemyInput : PlayerInput {

        // Use this for initialization
        void Start ()
        {
            Attack = true;
            targetUpValue = 1.0f;
            targetCRightValue = 1.0f;
        }
	
        // Update is called once per frame
        void Update () {
		    CalculationAxisSignal();
        }


    }
}
