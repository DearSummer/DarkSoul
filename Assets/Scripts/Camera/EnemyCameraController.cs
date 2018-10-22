using DS.Role;
using UnityEngine;

namespace DS.Camera
{
    public class EnemyCameraController : CameraController {

        // Use this for initialization
        void Start ()
        {
            controller = GetComponentInParent<ActorController>();
            player = controller.player;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        private void FixedUpdate()
        {
            if (!IsLock)
            {
                TranslatePlayerRotation();
            }
        }

        private void TranslatePlayerRotation()
        {
            preFramePlayerEulerAngel = player.transform.eulerAngles;

            yAxis.transform.Rotate(Vector3.up, controller.InputSignal.CRightValue * volicity * Time.fixedDeltaTime);
            eulerAngelX -= controller.InputSignal.CUpValue * volicity * Time.fixedDeltaTime;
            eulerAngelX = Mathf.Clamp(eulerAngelX, minAngel, maxAngel);
            xAxis.transform.localEulerAngles = new Vector3(eulerAngelX, 0, 0);

            player.transform.eulerAngles = preFramePlayerEulerAngel;
        }
    }
}
