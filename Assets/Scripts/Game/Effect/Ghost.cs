using DS.Runtime;
using UnityEngine;

namespace DS.Game.Effect
{
    public class Ghost : MonoBehaviour
    {

        public float survivalTime = 1.0f;
        public MeshRenderer meshRenderer;

        private float timer = 0f;
       
        
        // Update is called once per frame
        void Update ()
        {
            timer += Time.deltaTime;
            meshRenderer.material.SetFloat("_ThresHold",timer / survivalTime);
            if (timer > survivalTime)
            {
                ObjectPool.Instance.Return(this.gameObject);
            }
        }

        private void OnDisable()
        {
            timer = 0;
            meshRenderer.material.SetFloat("_ThresHold", 0);
        }
    }
}
