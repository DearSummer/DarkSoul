﻿using UnityEngine;

namespace DS.Game.Core
{
    public class GameCommandSender : MonoBehaviour
    {
        public GameCommandType interactorType;
        public GameCommandReceiver interactorObj;
        public bool sendOnce = true;

        public AudioSource onSendAudio;
        public float audioPlayDelay;

        private bool hasSend = false;
        private float lastSendTime;

        public void Send(object sender)
        {
            if (sendOnce && hasSend)
                return;

            hasSend = false;
            lastSendTime = Time.time;
            interactorObj.Receive(interactorType,sender);

            if(onSendAudio != null)
                onSendAudio.PlayDelayed(audioPlayDelay);
        }



    }
}
