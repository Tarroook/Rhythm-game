using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NoteReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is NoteMarker noteMarker) // do this so it doesn't react to all signals
        {
            Debug.Log(notification);
        }
    }
}
