using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    public interface IPlayerState
    {
        void OnEnter(GameObject player,ActorController controller);
        void Update(PlayerInput inputSignal,Animator animator);
        void FixedUpdate(Rigidbody rigidbody);
        void OnExit();
    }

