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

        private void Awake()
        {
            GetComponent<GameCommandReceiver>().Register(type, this);
        }

        public abstract void PerformInteractor();

        public virtual void OnInteractor()
        {
            if (actionOnce && hasAction)
                return;

            hasAction = true;

            if (coolDown > 0)
            {
                if (Time.time > startTime + coolDown)
                {
                    startTime += Time.time + startDelay;
                    ExcuteInteractor();
                }
            }
            else
                ExcuteInteractor();
        }

        public void ExcuteInteractor()
        {
            if (startDelay > 0)
                Invoke("PerformInteractor", startDelay);
            else
                PerformInteractor();
        }
    }
}
