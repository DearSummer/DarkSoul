using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Effect
{
    public class FightingParticleManager : MonoSingleton<FightingParticleManager>
    {
        public GameObject[] effectArray;
        private readonly List<ParticleMessage> messageList = new List<ParticleMessage>();

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

        public void ShowEffect(int index, Vector3 pos)
        {
            if (index < 0 || index >= effectArray.Length)
                return;

            ShowEffect(effectArray[index], pos);
        }

        public void ShowEffect(GameObject effect,Vector3 pos)
        {
            GameObject go = ObjectPool.Instance.Get(effect);
            go.transform.position = pos;

            float aliveTime = go.GetComponent<ParticleSystem>().main.duration;

            ParticleMessage pm = new ParticleMessage
            {
                obj = go,
                aliveTime = aliveTime
            };

            messageList.Add(pm);
        }


        private class ParticleMessage
        {
            public GameObject obj;
            public float aliveTime;
        }
    }
}
