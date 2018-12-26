using System.Collections;
using UnityEngine;

namespace DS.Game.Core
{
    [RequireComponent(typeof(GameCommandReceiver))]
    public abstract class GameCommandHandler : MonoBehaviour
    {
        public GameCommandType type;
        public bool actionOnce = false;
        public float coolDown = 0;
        public float startDelay = 0;

        private bool hasAction = false;
        private float startTime = 0;

        private float timer = 0;

        private void Awake()
        {
            GetComponent<GameCommandReceiver>().Register(type, this);
        }

        public abstract void PerformInteractor(object sender);

        public virtual void OnInteractor(object sender)
        {
            if (actionOnce && hasAction)
                return;

            hasAction = true;

            if (coolDown > 0)
            {
                if (Time.time > startTime + coolDown)
                {
                    startTime += Time.time + startDelay;
                    ExcuteInteractor(sender);
                }
            }
            else
                ExcuteInteractor(sender);
        }

        public void ExcuteInteractor(object sender)
        {
            
            if (startDelay > 0)
                StartCoroutine(ExcuteInteractorDelay(sender));
            else
                PerformInteractor(sender);
        }

        private IEnumerator ExcuteInteractorDelay(object sender)
        {
            while (timer < startDelay)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            PerformInteractor(sender);
            timer = 0;
           
        }
    }
}
