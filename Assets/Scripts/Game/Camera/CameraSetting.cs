using System;
using Cinemachine;
using UnityEngine;

namespace DS.Game.Camera
{
    public class CameraSetting : MonoBehaviour
    {
        [Serializable]
        public struct InvertSetting
        {
            public bool invertX;
            public bool invertY;
        }

        public Transform follow;
        public Transform lookAt;

        public CinemachineFreeLook keybordFreeLookCamera;

        public InvertSetting keybordInvertSetting;

        public CinemachineFreeLook CurrentCamera
        {
            get { return keybordFreeLookCamera; }
        }

        private PlayerInput playerInput;
            

        private void Awake()
        {
            keybordFreeLookCamera.Follow = follow;
            keybordFreeLookCamera.LookAt = lookAt;
            keybordFreeLookCamera.m_XAxis.m_InvertInput = keybordInvertSetting.invertX;
            keybordFreeLookCamera.m_YAxis.m_InvertInput = keybordInvertSetting.invertY;

            Cursor.lockState = CursorLockMode.Locked;

            playerInput = GameObject.FindObjectOfType<PlayerInput>();
        }



        private void Update()
        {
            keybordFreeLookCamera.m_XAxis.m_InputAxisValue = -playerInput.CRightValue;
            keybordFreeLookCamera.m_YAxis.m_InputAxisValue = -playerInput.CUpValue;
        }
    }
}
