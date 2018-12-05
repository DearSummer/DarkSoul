using System;
using System.Collections.Generic;
using DS.Game.Core;
using DS.Runtime;
using UnityEngine;

namespace DS.Game.Effect
{
    public class EffectEvent : MonoBehaviour {

        [Serializable]
        public class EventData
        {
            public string name;
            public EffectData[] effect;

            [Serializable]
            public class EffectData
            {
                public GameObject prefab;
                public Transform postion;
            }
        }


        public EventData[] eventDate;

        private Animator animator;
        private Dictionary<string,EventData> eventDir = new Dictionary<string, EventData>();

        private void Start()
        {
            animator = GetComponent<Animator>();
            if(animator == null)
                throw new Exception("You Need Animator");

            MonoLinkedStateMachineBehviour<EffectEvent>.Initialise(animator, this);

            eventDir.Clear();
            for (int i = 0; i < eventDate.Length; ++i)
            {
                eventDir[eventDate[i].name] = eventDate[i];

                for (int j = 0; j < eventDate[i].effect.Length; ++j)
                {
                    GameObject obj = ObjectPool.Instance.Get(eventDate[i].effect[j].prefab);
                    ObjectPool.Instance.Return(obj);
                }
            }

        }


        public void PlayEvent(string name)
        {
            EventData data;
            if (!eventDir.TryGetValue(name, out data))
            {
                Debug.LogError("Not such event exist");
                return;
            }

            for (int i = 0; i < data.effect.Length; ++i)
            {
                FightingParticleManager.Instance.ShowEffect(data.effect[i].prefab, data.effect[i].postion.position);
            }
        }


    }
}
