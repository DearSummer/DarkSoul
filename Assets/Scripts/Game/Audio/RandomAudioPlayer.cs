using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DS.Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudioPlayer : MonoBehaviour {

        [Serializable]
        public struct MaterialAudioSet
        {
            public Material[] materials;
            public SoundSet[] soundSets;
        }

        [Serializable]
        public struct SoundSet
        {
            public string name;
            public AudioClip[] clips;
        }

        public bool randomPitch = true;
        public float randomPitchRange = 0.2f;

        public float playDelay = 0f;
        public SoundSet sound = new SoundSet();
        public MaterialAudioSet[] sets;

        [HideInInspector]
        public bool isPlaying;
        [HideInInspector]
        public bool canPlay;

        public AudioSource Source { get; private set; }
        public AudioClip Clip { get; private set; }

        private readonly Dictionary<Material,SoundSet[]> soundSetDir = new Dictionary<Material, SoundSet[]>();

        private void Awake()
        {
            Source = GetComponent<AudioSource>();

            for (int i = 0; i < sets.Length; ++i)
            {
                foreach (var mat in sets[i].materials)
                {
                    soundSetDir.Add(mat, sets[i].soundSets);
                }
            }
        }

        public void RandomPlay(Material material, int id = 0)
        {
            if (material == null)
                return;
            Clip = InternalPlaySound(material, id);
        }

        public void RandomPlay()
        {
            Clip = InternalPlaySound(null, 0);
        }



        private AudioClip InternalPlaySound(Material material,int id)
        {
            SoundSet playSoundSet = sound;
            if (material != null)
            {
                SoundSet[] soundSets = null;
                if (soundSetDir.TryGetValue(material, out soundSets))
                {
                    if (id < soundSets.Length)
                        playSoundSet = soundSets[id];
                }
            }

            if (playSoundSet.clips == null || playSoundSet.clips.Length == 0)
                return null;

            var clip = playSoundSet.clips[Random.Range(0, playSoundSet.clips.Length)];

            if (clip == null)
                return null;

            Source.pitch = randomPitch ? Random.Range(1 - randomPitchRange, 1 + randomPitchRange) : 1.0f;
            Source.clip = clip;
            Source.PlayDelayed(playDelay);

            return clip;
        }
    }
}
