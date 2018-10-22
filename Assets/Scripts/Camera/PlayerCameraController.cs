using DS.Role;
using UnityEngine;
using UnityEngine.UI;

namespace DS.Camera
{
    public class PlayerCameraController : CameraController {

        // Use this for initialization
        void Awake()
        {
            controller = GetComponentInParent<ActorController>();
            player = controller.player;

            Camera = UnityEngine.Camera.main.gameObject;
            Cursor.lockState = CursorLockMode.Locked;

            EnableLockDoc(false);
        }

        private void Update()
        {
            if (controller.InputSignal.LockOn)
            {
                Lock();
            }

            if (IsLock)
            {
                lockDoc.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(lockTarget.transform.position);
                AutoUnlock();
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!IsLock)
            {
                TranslateCameraPos();
            }
            else
            {
                LockEnemy();
            }

            Camera.transform.position = Vector3.Slerp(Camera.transform.position, xAxis.transform.position, 0.5f);
            Camera.transform.eulerAngles = xAxis.transform.eulerAngles;
        }

        private void LockEnemy()
        {
            Vector3 pos = lockTarget.transform.position - player.transform.position;
            pos.y = 0;
            yAxis.transform.forward = Vector3.Lerp(yAxis.transform.forward, pos, 0.1f);
        }

        private void TranslateCameraPos()
        {
            preFramePlayerEulerAngel = player.transform.eulerAngles;

            yAxis.transform.Rotate(Vector3.up, controller.InputSignal.CRightValue * volicity * Time.fixedDeltaTime);
            eulerAngelX -= controller.InputSignal.CUpValue * volicity * Time.fixedDeltaTime;
            eulerAngelX = Mathf.Clamp(eulerAngelX, minAngel, maxAngel);
            xAxis.transform.localEulerAngles = new Vector3(eulerAngelX, 0, 0);

            player.transform.eulerAngles = preFramePlayerEulerAngel;

        }

        private void AutoUnlock()
        {
            if (Vector3.Distance(lockTarget.transform.position, player.transform.position) > 8f)
            {
                EnableLockDoc(false);
            }
        }

        private void Lock()
        {
            Vector3 playerOriginPos1 = player.transform.position;
            Vector3 playerOriginPos2 = playerOriginPos1 + new Vector3(0, 1, 0);
            Vector3 center = playerOriginPos2 + player.transform.forward * 5f;

            Collider[] cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5f), player.transform.rotation,
                LayerMask.GetMask(ProjectConstant.Layer.ENEMY));

            if (cols.Length == 0)
            {
                return;
            }

            foreach (var col in cols)
            {
                if (col.gameObject == lockTarget)
                {
                    lockTarget = null;
                    EnableLockDoc(false);
                    break;
                }

                lockTarget = col.gameObject;
                EnableLockDoc(true);
                break;
            }
        }

        private void EnableLockDoc(bool enable)
        {
            IsLock = enable;
            lockDoc.enabled = enable;
            if (!enable)
            {
                lockTarget = null;
            }
        }
    }
}
