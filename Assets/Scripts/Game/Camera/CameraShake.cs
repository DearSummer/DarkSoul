using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DS.Game.Camera
{
    [RequireComponent(typeof(CinemachineVirtualCameraBase))]
    public class CameraShake : MonoBehaviour
    {
        private static List<CameraShake> cameraShakeList = new List<CameraShake>();

        private CinemachineVirtualCameraBase cinemachineBase;

        private Vector3 originalLocalPosition;

        private bool isInShake = false;
        private float shakeTime = 0f;
        private float radius = 0f;

        

        private void Awake()
        {
            cinemachineBase = GetComponent<CinemachineVirtualCameraBase>();
        }

        private void OnEnable()
        {
            cameraShakeList.Add(this);
        }

        private void OnDisable()
        {
            cameraShakeList.Remove(this);
        }

        public static void Shake(float r, float time)
        {
            for (int i = 0; i < cameraShakeList.Count; ++i)
            {
                cameraShakeList[i].StartShake(r, time);
            }
        }

        public static void Stop()
        {
            for (int i = 0; i < cameraShakeList.Count; ++i)
            {
                cameraShakeList[i].StopShake();
            }
        }

        private void StartShake(float r,float time)
        {
            if (!isInShake)
            {
                originalLocalPosition = cinemachineBase.LookAt.localPosition;
            }

            isInShake = true;
            shakeTime = time;
            radius = r;
        }

        private void StopShake()
        {
            originalLocalPosition = cinemachineBase.LookAt.localPosition;
            isInShake = false;
            shakeTime = 0f;
            radius = 0f;
        }

        private void LateUpdate()
        {
            if (isInShake)
            {
                cinemachineBase.LookAt.localPosition = originalLocalPosition + Random.insideUnitSphere * radius;
                shakeTime -= Time.deltaTime;

                if (shakeTime < 0)
                {
                    isInShake = false;
                    cinemachineBase.LookAt.localPosition = originalLocalPosition;
                }
            }
        }
    }
}
