using UnityEngine;
using UnityEngine.UI;

namespace DS.UI
{
    public class DamageText : MonoBehaviour
    {
        public Color color;

        private Text damageText;
        private CanvasGroup canvasGroup;

        private float timer = 0f;
        private float aliveTime = 1.0f;

        private float a;
        private float b;

        // Use this for initialization
        void Start()
        {
            init();
        }

        /*
         * y =-ax`2 + bx;
         * a = 4 / aliveTime ` 2
         * b = 4 / aliveTime
         */
        private void Update()
        {
  
            timer += Time.deltaTime;
            canvasGroup.alpha = -a * timer * timer + b * timer;

            this.transform.position += Vector3.up * 10 * Time.deltaTime;

            if (timer < aliveTime)
                return;

            ObjectPool.Instance.Return(this.gameObject);
        }

        private void init()
        {
            damageText = GetComponent<Text>();
            canvasGroup = GetComponent<CanvasGroup>();

            a = 4 / (aliveTime * aliveTime);
            b = 4 / aliveTime;
        }

        public void SetText(int damage)
        {
            if (damageText == null)
            {
                init();
            }
            damageText.text = damage.ToString();
            damageText.color = color;
        }

        public void SetAliveTime(float aliveTime)
        {
            this.aliveTime = aliveTime;

            a = 4 / (aliveTime * aliveTime);
            b = 4 / aliveTime;
        }

        private void OnDisable()
        {
            damageText.text = "";
            timer = 0f;
            canvasGroup.alpha = 1;
        }
    }
}
