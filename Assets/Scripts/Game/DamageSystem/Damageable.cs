using System;
using System.Collections.Generic;
using DS.Game.Message;
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

        private bool isInvnlerable;
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

            if (isInvnlerable)
            {
                OnHitWhileInvnlnerable.Invoke();
                return;
            }

            Vector3 attackerPos = msg.attacker.transform.position;
            Vector3 recevierPos = this.transform.position;

            Vector3 hitDir = attackerPos - recevierPos;
            if (Vector3.Angle(hitDir, this.transform.forward) > hitAngle * 0.5f)
                return;

            hp -= msg.damage;
            isInvnlerable = true;

            if (hp < 0)
                schedule += OnDeath.Invoke;
            else
                OnReceiverDamage.Invoke();

            MessageType mt = hp > 0 ? MessageType.DAMAGE : MessageType.DIE;

            for (int i = 0; i < onDamageMessageReceiverList.Count; i++)
            {
                onDamageMessageReceiverList[i].OnMessageReceiver(mt, this, msg);
            }

        }

        private void Update()
        {
            if (isInvnlerable)
            {
                timeSinceLastHit += Time.deltaTime;
                if (timeSinceLastHit > invnlnerabilityTime)
                {
                    isInvnlerable = false;
                    timeSinceLastHit = 0f;
                    OnBecomeVnlnerable.Invoke();
                }
            }
        }

        private void LateUpdate()
        {
            if (schedule != null)
            {
                schedule();
                schedule = null;
            }
        }
    }
}
