using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class NoteReceiver : MonoBehaviour, INotificationReceiver
{

    public UnityEvent onWindUpSignal;

    private void Awake()
    {
        if (onWindUpSignal == null)
            onWindUpSignal = new UnityEvent();
    }
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is EnemyWindUpSignal sig) // do this so it doesn't react to all signals
        {
            onWindUpSignal.Invoke();
        }
    }
}
