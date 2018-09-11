using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    public class AnimatorEventController : MonoBehaviour
    {
        private Animator animator;
        // Use this for initialization
        void Awake ()
        {
            animator = GetComponent<Animator>();
        }

        private void ResetAttackTrigger()
        {
            animator.ResetTrigger(ProjectConstant.AnimatorParameter.ATTACK);
        }

    }

