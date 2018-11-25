
using UnityEngine;

namespace DS.Effect
{
    [RequireComponent(typeof(ParticleSystem))]
    public class SkillEffect : MonoBehaviour
    {


        public void SetColor(Color color)
        {
            ParticleSystem.MainModule setting = GetComponent<ParticleSystem>().main;
            setting.startColor = color;
        }

        public GameObject GetEffectObject()
        {
            return this.gameObject;
        }

    }
}
