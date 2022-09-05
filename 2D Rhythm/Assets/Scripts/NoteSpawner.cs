using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NoteSpawner : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private float timeToReact = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();

        TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
        IMarker[] markers = timelineAsset.GetOutputTrack(1).GetMarkers().ToArray();
        //playableDirector.time = markers[0].time;
        Debug.Log("Time : " + markers[0].time);
        bool canCreate;
        for(int i = 0; i < markers.Length; i++)
        {
            canCreate = false;
            for (int y = i; y < markers.Length; y++)
            {
                // code to check if there is alerady a marker
            }
            // code to create marker if none are made
        }
        foreach(IMarker marker in markers)
        {
            //timelineAsset.GetOutputTrack(1).CreateMarker<SignalEmitter>(marker.time - timeToReact);
        }
    }
}
