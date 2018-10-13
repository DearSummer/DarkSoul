using DS.Role;
using UnityEngine;
using UnityEngine.UI;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("--------axis------")]
        public GameObject yAxis;
        public GameObject xAxis;

        [Header("---------rotate speed------")]
        public float volicity;

        [Header("--------camera angel-----")]
        public float maxAngel = 30f;
        public float minAngel = -40f;

        [Header("-------lock doc----------")]
        public Image lockDoc;

        public bool IsLock
        {
            private set;
            get;
        }

        private GameObject _player;
        private ActorController _controller;

        private GameObject _camera;

        private float _eulerAngelX;
        private Vector3 _preFramePlayerEulerAngel;

        private GameObject _lockTarget;

        // Use this for initialization
        void Awake()
        {
            _controller = GetComponentInParent<ActorController>();
            _player = _controller.player;

            _camera = UnityEngine.Camera.main.gameObject;
            Cursor.lockState = CursorLockMode.Locked;

            EnableLockDoc(false);
        }

        private void Update()
        {
            if (_controller.InputSignal.LockOn)
            {
                Lock();
            }

            if (IsLock)
            {
                lockDoc.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(_lockTarget.transform.position);
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

            _camera.transform.position = Vector3.Slerp(_camera.transform.position, xAxis.transform.position, 0.5f);
            _camera.transform.eulerAngles = xAxis.transform.eulerAngles;
        }

        private void LockEnemy()
        {
            Vector3 pos = _lockTarget.transform.position - _player.transform.position;
            pos.y = 0;
            yAxis.transform.forward = Vector3.Lerp(yAxis.transform.forward, pos, 0.1f);
        }

        private void TranslateCameraPos()
        {
            _preFramePlayerEulerAngel = _player.transform.eulerAngles;

            yAxis.transform.Rotate(Vector3.up, _controller.InputSignal.CRightValue * volicity * Time.fixedDeltaTime);
            _eulerAngelX -= _controller.InputSignal.CUpValue * volicity * Time.fixedDeltaTime;
            _eulerAngelX = Mathf.Clamp(_eulerAngelX, minAngel, maxAngel);
            xAxis.transform.localEulerAngles = new Vector3(_eulerAngelX, 0, 0);

            _player.transform.eulerAngles = _preFramePlayerEulerAngel;

        }

        private void AutoUnlock()
        {
            if (Vector3.Distance(_lockTarget.transform.position, _player.transform.position) > 8f)
            {
                EnableLockDoc(false);
            }
        }

        private void Lock()
        {
            Vector3 playerOriginPos1 = _player.transform.position;
            Vector3 playerOriginPos2 = playerOriginPos1 + new Vector3(0, 1, 0);
            Vector3 center = playerOriginPos2 + _player.transform.forward * 5f;

            Collider[] cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5f), _player.transform.rotation,
                LayerMask.GetMask(ProjectConstant.Layer.ENEMY));

            if (cols.Length == 0)
            {
                return;
            }

            foreach (var col in cols)
            {
                if (col.gameObject == _lockTarget)
                {
                    _lockTarget = null;
                    EnableLockDoc(false);
                    break;               
                }
            
                _lockTarget = col.gameObject;
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
                _lockTarget = null;
            }
        }

    }
}

