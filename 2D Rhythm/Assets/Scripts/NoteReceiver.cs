using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class NoteReceiver : MonoBehaviour, INotificationReceiver
{

    public StringArrayUnityEvent onWindUpSignal;
    public DoubleUnityEvent onAttackSignal;
    public UnityEvent onVulnerableSignal;
    public DoubleUnityEvent onPlayerAttackSignal;

    private void Awake()
    {
        if (onWindUpSignal == null)
            onWindUpSignal = new StringArrayUnityEvent();
        if (onAttackSignal == null)
            onAttackSignal = new DoubleUnityEvent();
        if (onVulnerableSignal == null)
            onVulnerableSignal = new UnityEvent();
        if (onPlayerAttackSignal == null)
            onPlayerAttackSignal = new DoubleUnityEvent();
    }
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if(notification is EnemyWindUpSignal ewu) // do this so it doesn't react to all signals
        {
            onWindUpSignal.Invoke(ewu.directions);
        }
        if (notification is EnemyAttackSignal ea) // do this so it doesn't react to all signals
        {
            onAttackSignal.Invoke(ea.offset);
        }
        if(notification is EnemyVulnerableSignal ev)
        {
            onVulnerableSignal.Invoke();
        }
        if(notification is PlayerAttackSignal pa)
        {
            onPlayerAttackSignal.Invoke(pa.offset);
        }
    }
}
