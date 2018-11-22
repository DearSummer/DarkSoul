using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

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

        private Vector3 scale = Vector3.zero;

        // Use this for initialization
        void Start()
        {
            init();
        }

        private void OnEnable()
        {
            if (scale == Vector3.zero)
                return;

            this.transform.localScale = scale;
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
            if (scale == Vector3.zero)
                scale = this.transform.localScale;

            damageText.text = "";
            timer = 0f;
            canvasGroup.alpha = 1;
        }
    }
}
