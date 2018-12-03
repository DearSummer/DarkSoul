using System.Collections.Generic;
using DS.Runtime;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

namespace DS.Effect
{
    public class GhostEffect : MonoBehaviour
    {

        public bool openGhostEffect;
        public float survivalTime = 1.0f;
        public float intervalTime = 0.2f;
        public Material effectMaterial;

        private GameObject ghostObj;
        private SkinnedMeshRenderer[] skinnedMeshRenderer;

        private float timer;

        // Use this for initialization
        void Start()
        {
            skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!openGhostEffect)
            {
                timer = 0f;
                return;
            }
                

            timer += Time.deltaTime;
            if (timer <= intervalTime)
                return;

            CreateGhost();
            timer = 0f;
        }

        private void CreateGhost()
        {
            for (int i = 0; i < skinnedMeshRenderer.Length; i++)
            {
                Mesh mesh = new Mesh();
                skinnedMeshRenderer[i].BakeMesh(mesh);
                GameObject ghost = ObjectPool.Instance.Get("__Ghost");
                MeshFilter meshFilter = ghost.GetComponent<MeshFilter>();
                if (meshFilter == null)
                    meshFilter = ghost.AddComponent<MeshFilter>();

                MeshRenderer meshRenderer = ghost.GetComponent<MeshRenderer>();
                if (meshRenderer == null)
                    meshRenderer = ghost.AddComponent<MeshRenderer>();
                //meshRenderer.material = skinnedMeshRenderer[i].material;
                meshRenderer.material = effectMaterial;
                meshFilter.mesh = mesh;
                   
                
                Ghost scrpit = ghost.GetComponent<Ghost>();
                if (scrpit == null)
                    scrpit = ghost.AddComponent<Ghost>();
                scrpit.survivalTime = survivalTime;
                scrpit.meshRenderer = meshRenderer;

                ghost.transform.position = skinnedMeshRenderer[i].transform.position;
                ghost.transform.rotation = skinnedMeshRenderer[i].transform.rotation;
            }
        }

    }
}
