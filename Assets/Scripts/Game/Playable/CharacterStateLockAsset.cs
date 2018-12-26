using UnityEngine;
using UnityEngine.Playables;

namespace DS.Game.Playable
{
    [System.Serializable]
    public class CharacterStateLockAsset : PlayableAsset
    {

        public ExposedReference<Animator> attackerAnimator;
        public ExposedReference<Animator> receiverAnimator;

        // Factory method that generates a playable based on this asset
        public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var script = ScriptPlayable<CharacterStateLockPlayable>.Create(graph);
            script.GetBehaviour().attacker = attackerAnimator.Resolve(graph.GetResolver());
            script.GetBehaviour().receiver = receiverAnimator.Resolve(graph.GetResolver());

            return script;
        }
    }
}
