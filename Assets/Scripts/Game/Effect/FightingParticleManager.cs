using System.Collections.Generic;
using DS.Runtime;
using UnityEngine;

namespace DS.Game.Effect
{
    public class FightingParticleManager : MonoSingleton<FightingParticleManager>
    {
        private readonly List<ParticleMessage> messageList = new List<ParticleMessage>(32);

        private void Update()
        {
            for (int i = messageList.Count - 1; i >= 0; i--)
            {
                messageList[i].aliveTime -= Time.deltaTime;
                if (messageList[i].aliveTime <= 0f)
                {
                    ObjectPool.Instance.Return(messageList[i].obj);
                    messageList.RemoveAt(i);
                }
            }
        }


        public GameObject ShowEffect(GameObject effect,Vector3 pos)
        {
            GameObject go = ObjectPool.Instance.Get(effect);
            go.transform.position = pos;

            ParticleSystem.MainModule setting = go.GetComponent<ParticleSystem>().main;
            float aliveTime = setting.duration + setting.startLifetime.constantMax;

            ParticleMessage pm = new ParticleMessage
            {
                obj = go,
                aliveTime = aliveTime
            };

            messageList.Add(pm);

            return go;
        }


        private class ParticleMessage
        {
            public GameObject obj;
            public float aliveTime;
        }
    }
}
