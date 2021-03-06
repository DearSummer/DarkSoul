﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DS.Game.UI
{
    public class SliderDisappearDelay : MonoBehaviour
    {

        public Slider shadowSlider;
        private Slider self;


        private void Awake()
        {
            self = GetComponent<Slider>();
        }

        public void SetValue(float value)
        {
            StartCoroutine(DisappearShadow(value));
        }


        private IEnumerator DisappearShadow(float value)
        {
            while (shadowSlider.value - value > 0.01f)
            {
                self.value = Mathf.Lerp(self.value, value, 0.5f);
                shadowSlider.value = Mathf.Lerp(shadowSlider.value, value, 0.05f);
                yield return new WaitForEndOfFrame();
            }

 
            shadowSlider.value = value;
            yield return null;
        }
    }
}
