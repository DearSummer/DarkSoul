using DS.Role;
using DS.Role.Interface;
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
            protected set;
            get;
        }

        protected GameObject player;
        protected ActorController controller;
  
        protected GameObject Camera;

        protected float eulerAngelX;
        protected Vector3 preFramePlayerEulerAngel;

        protected GameObject lockTarget;
    }
}

