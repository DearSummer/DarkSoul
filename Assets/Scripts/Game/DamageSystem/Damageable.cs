using System;
using System.Collections.Generic;
using DS.Game.Core.Message;
using UnityEngine;
using UnityEngine.Events;

namespace DS.Game.DamageSystem
{
    public class Damageable : MonoBehaviour
    {
        public int hpMax;
        public float invnlnerabilityTime;

        [Range(0,360f)]
        public float hitAngle = 360f;

        public UnityEvent OnDeath, OnReceiverDamage, OnHitWhileInvnlnerable, OnBecomeVnlnerable, OnHeathRecover;
        public List<IMessageReceiver<DamageData>> onDamageMessageReceiverList = new List<IMessageReceiver<DamageData>>();

        public int hp { private set; get; }

        [HideInInspector]
        public bool isInvnlerable = false;


        private float timeSinceLastHit = 0f;

        private Action schedule;

        public void Register(IMessageReceiver<DamageData> receiver)
        {
            onDamageMessageReceiverList.Add(receiver);
        }

        private void Start()
        {
            AddHeath(hpMax);
        }

        public void AddHeath(int value)
        {
            hp += value;
            hp = Mathf.Clamp(hp, 0, hpMax);

            isInvnlerable = false;
            timeSinceLastHit = 0f;
    
            OnHeathRecover.Invoke();
        }


        public void TryGetDamage(DamageData msg)
        {
            if (hp < 0)
                return;


            Vector3 attackerPos = msg.attacker.transform.position;
            Vector3 recevierPos = this.transform.position;

            Vector3 hitDir = attackerPos - recevierPos;
            if (Vector3.Angle(hitDir, this.transform.forward) > hitAngle * 0.5f)
                return;

            MessageType mt;
            if (isInvnlerable)
            {
                OnHitWhileInvnlnerable.Invoke();
                mt = MessageType.INVNLERABLE;
            }
            else
            {
                hp -= msg.damage;
                mt = hp > 0 ? MessageType.DAMAGE : MessageType.DIE;
            }

            if (invnlnerabilityTime > 0f)
                isInvnlerable = true;

            if (hp < 0)
                schedule += OnDeath.Invoke;
            else
                OnReceiverDamage.Invoke();


            for (int i = 0; i < onDamageMessageReceiverList.Count; i++)
            {
                onDamageMessageReceiverList[i].OnMessageReceiver(mt, this, msg);
            }

        }

        private void LateUpdate()
        {
            if (isInvnlerable && invnlnerabilityTime > 0f)
            {
                timeSinceLastHit += Time.deltaTime;
                if (timeSinceLastHit > invnlnerabilityTime)
                {
                    isInvnlerable = false;
                    timeSinceLastHit = 0f;
                    OnBecomeVnlnerable.Invoke();
                }
            }

            if (schedule != null)
            {
                schedule();
                schedule = null;
            }
        }

    }
}
