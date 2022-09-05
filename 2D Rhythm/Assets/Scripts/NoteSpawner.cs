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
        
        
        List<BeatEmitter> beatEmitters = new List<BeatEmitter>();
        int count = 1;
        foreach(IMarker marker in markers)
        {
            if (marker is EnemyWindUpSignal || marker is EnemyAttackSignal)
            {
                timelineAsset.GetOutputTrack(1).DeleteMarker(marker);
            }
            else if (marker is BeatEmitter)
            {
                beatEmitters.Add((BeatEmitter)marker);
                ((BeatEmitter)marker).beatNb = count++;
            }
        }
        foreach (BeatEmitter be in beatEmitters)
        {
            switch (be.action)
            {
                case "eAttack": 
                    timelineAsset.GetOutputTrack(1).CreateMarker<EnemyWindUpSignal>(be.time - timeToReact);
                    break;
                default:
                    Debug.Log("No actions recognized for marker at time :" + be.time);
                    break;
            }
        }
    }
}