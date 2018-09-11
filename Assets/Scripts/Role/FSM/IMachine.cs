using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

    public interface IMachine
    {
        void Update();
        void FixUpdate();
        void TranslateTo(IPlayerState newPlayerState);
        IPlayerState GetCurrentState();

    }

