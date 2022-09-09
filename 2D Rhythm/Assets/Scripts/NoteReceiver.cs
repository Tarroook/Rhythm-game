using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class NoteReceiver : MonoBehaviour, INotificationReceiver
{

    public UnityEvent onWindUpSignal;
    public DoubleUnityEvent onAttackSignal;

    private void Awake()
    {
        if (onWindUpSignal == null)
            onWindUpSignal = new UnityEvent();
        if (onAttackSignal == null)
            onAttackSignal = new DoubleUnityEvent();
    }
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is EnemyWindUpSignal ewu) // do this so it doesn't react to all signals
        {
            onWindUpSignal.Invoke();
        }
        if (notification is EnemyAttackSignal ea) // do this so it doesn't react to all signals
        {
            onAttackSignal.Invoke(ea.offset);
        }
    }
}
