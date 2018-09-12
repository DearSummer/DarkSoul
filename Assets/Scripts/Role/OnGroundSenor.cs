using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    public class OnGroundSenor : MonoBehaviour
    {
    
        [Header("-----player collider------")]
        public new CapsuleCollider collider;
        [Header("-----senor offset---------")]
        public float offset = 0.05f;

        private Vector3 _capsulePoint1;
        private Vector3 _capsulePoint2;
        private float _radius;

        // Use this for initialization
        void Start ()
        {
            _radius = collider.radius - 0.05f;
        }
        
        // Update is called once per frame
        void FixedUpdate ()
        {
            SendMessageUpwards(CheckIsOnGround() ? "OnGround" : "NotOnGround");
        }

        private bool CheckIsOnGround()
        {
            _capsulePoint1 = transform.position + transform.up * (_radius - offset);
            _capsulePoint2 = transform.position + transform.up * (collider.height - offset) -
                            transform.up * (_radius - offset);

            Collider[] outputColliders = Physics.OverlapCapsule(_capsulePoint1, _capsulePoint2, _radius,
                LayerMask.GetMask(ProjectConstant.Layer.GROUND));
            return outputColliders.Length > 0;
        }
    }
