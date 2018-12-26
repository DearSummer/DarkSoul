using DS.Game.DamageSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace DS.Game.Playable
{
    [System.Serializable]
    public class DamageableAsset : PlayableAsset
    {

        public ExposedReference<Damageable> damageable;
        public DamageData data;

        // Factory method that generates a playable based on this asset
        public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var script = ScriptPlayable<DamageablePlayable>.Create(graph);
            script.GetBehaviour().damageable = damageable.Resolve(graph.GetResolver());
            script.GetBehaviour().data = data;
            return script;
        }
    }
}
