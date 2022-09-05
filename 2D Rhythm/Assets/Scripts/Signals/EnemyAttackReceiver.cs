using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EnemyAttackReceiver : MonoBehaviour, INotificationReceiver
{
    public SignalAssetEventPair[] signalAssetEventPairs;

    [Serializable]
    public class SignalAssetEventPair
    {
        public SignalAsset signalAsset;
        public ParameterizedEvent events;

        [Serializable]
        public class ParameterizedEvent : UnityEvent<bool> { }
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is EnemyAttackSignal enemyAttackSignal)
        {
            var matches = signalAssetEventPairs.Where(x => ReferenceEquals(x.signalAsset, enemyAttackSignal.asset));
            foreach (var m in matches)
            {
                m.events.Invoke(true);
            }
        }
    }
}
