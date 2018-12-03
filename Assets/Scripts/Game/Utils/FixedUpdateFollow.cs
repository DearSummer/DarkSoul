using UnityEngine;

namespace DS.Game.Utils
{
    public class FixedUpdateFollow : MonoBehaviour
    {

        public Transform target;

        private void FixedUpdate()
        {
            this.transform.position = target.transform.position;
            this.transform.rotation = target.transform.rotation;
        }
    }
}
