using System;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Game.Core
{
    public class GameCommandReceiver : MonoBehaviour
    {

        private readonly Dictionary<GameCommandType, List<Action<object>>> handerDic = new Dictionary<GameCommandType, List<Action<object>>>();

        public void Receive(GameCommandType type,object sender)
        {
            List<Action<object>> callbackList;
            if (handerDic.TryGetValue(type, out callbackList))
            {
                foreach (var i in callbackList)
                {
                    i(sender);
                }
            }
        }

        public void Register(GameCommandType type, GameCommandHandler gch)
        {
            List<Action<object>> callbackList;
            if (!handerDic.TryGetValue(type, out callbackList))
            {
                handerDic[type] = new List<Action<object>>();
                callbackList = handerDic[type];
            }

            callbackList.Add(gch.OnInteractor);
        }

        public void Remove(GameCommandType type, GameCommandHandler handler)
        {
            handerDic[type].Remove(handler.OnInteractor);
        }
    }
}
