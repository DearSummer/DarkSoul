using DS.Runtime;
using UnityEngine;
using UnityEngine.Playables;

namespace DS.Role.Timeline
{
    public class PlayableDirectorManager : MonoSingleton<PlayableDirectorManager>
    {

        private PlayableDirector _director;
        private void Start()
        {
            _director = GetComponent<PlayableDirector>();
        }




    }
}
