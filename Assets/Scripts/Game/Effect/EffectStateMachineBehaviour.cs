using System;
using System.Collections.Generic;
using DS.Game.Core;
using UnityEngine;

namespace DS.Game.Effect
{
    public class EffectStateMachineBehaviour : MonoLinkedStateMachineBehviour<EffectEvent> {

        public enum EventType
        {
            Enter,
            Exit
        }

        [Serializable]
        public class EventInstance
        {
            public string name;
            public EventType type;
        }

        public EventInstance[] eventArray;

        private readonly List<EventInstance> onEnterEventList = new List<EventInstance>();
        private readonly List<EventInstance> onExitEventList = new List<EventInstance>();

        private void OnEnable()
        {
            for (int i = 0; i < eventArray.Length; ++i)
            {
                if(eventArray[i].type == EventType.Enter)
                    onEnterEventList.Add(eventArray[i]);
                else
                    onExitEventList.Add(eventArray[i]);
            }
        }

        protected override void OnLinkedStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            for (int i = 0; i < onEnterEventList.Count; ++i)
            {
                monoBehaviour.PlayEvent(onEnterEventList[i].name);
            }
        }


        protected override void OnLinkedStateExit(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            for (int i = 0; i < onExitEventList.Count; ++i)
            {
                monoBehaviour.PlayEvent(onExitEventList[i].name);
            }
        }
    }
}
