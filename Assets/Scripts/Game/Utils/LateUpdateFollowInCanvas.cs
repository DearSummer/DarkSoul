using UnityEngine;

namespace DS.Game.Utils
{
    public class LateUpdateFollowInCanvas : MonoBehaviour
    {
        public GameObject obj;
        public Transform target;
        public Canvas canvas;

        private void Start()
        {
            obj.transform.SetParent(canvas.transform);
        }

        private void LateUpdate()
        {
            Vector3 dir = UnityEngine.Camera.main.transform.position - this.transform.position;
            float angle = Vector3.Angle(dir, UnityEngine.Camera.main.transform.forward);

            float distance = Vector3.Distance(UnityEngine.Camera.main.transform.position, this.transform.position);

            if (angle > 90f && distance < 15f)
            {
                obj.SetActive(true);
                obj.transform.position = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main,
                    this.transform.position);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
