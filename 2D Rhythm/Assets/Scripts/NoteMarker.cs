using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NoteMarker : Marker, INotification
{
    [SerializeField] private int noteCount;

    public PropertyName id => new PropertyName();
}
