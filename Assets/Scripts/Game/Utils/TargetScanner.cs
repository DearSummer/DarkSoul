using System;
using DS.Game.Player;
using UnityEngine;

namespace DS.Game.Utils
{
    [Serializable]
    public class TargetScanner
    {
        public float heightOffset = 0f;
        public float detectionRadius = 10f;
        [Range(0, 360f)]
        public float detectionAngle = 360f;
        public float maxHeightDifference = 1f;
        public LayerMask targetLayer;


        public PlayerController Detect(Transform detector, bool useHeightDifference = true)
        {

            if (PlayerController.Instance == null)
                return null;

            Vector3 eyePos = detector.position + Vector3.up * heightOffset;
            Vector3 toTarget = PlayerController.Instance.transform.position - eyePos;
            Vector3 toTargetEye = PlayerController.Instance.transform.position + Vector3.up * 1.5f - eyePos;

            if (useHeightDifference && Mathf.Abs(toTarget.y + heightOffset) > maxHeightDifference)
            {
                return null;
            }

            Vector3 toTargetFlat = toTarget;
            toTargetFlat.y = 0f;

            if (toTargetFlat.sqrMagnitude < detectionRadius * detectionRadius)
            {
                if (Vector3.Dot(toTargetFlat.normalized, detector.forward.normalized) >=
                    Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {
                    bool canSee = false;

       
                    canSee |= !Physics.Raycast(eyePos, toTarget.normalized, detectionRadius, targetLayer,
                        QueryTriggerInteraction.Ignore);

                    canSee |= !Physics.Raycast(eyePos, toTargetEye.normalized, toTargetEye.magnitude, targetLayer,
                        QueryTriggerInteraction.Ignore);

                    if (canSee)
                        return PlayerController.Instance;
                }
            }

            return null;
        }



#if  UNITY_EDITOR

        public void DrawGizmo(Transform transform)
        {
            Color c = Color.red;
            c.a = 0.2f;
            UnityEditor.Handles.color = c;
            Vector3 rotatedForword = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForword, detectionAngle,
                detectionRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * heightOffset, 0.2f);

        }

#endif
    }

}