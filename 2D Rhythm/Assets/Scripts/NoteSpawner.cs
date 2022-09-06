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
        
        foreach(IMarker marker in markers)
        {
            if (marker is EnemyWindUpSignal || marker is EnemyAttackSignal)
            {
                timelineAsset.GetOutputTrack(1).DeleteMarker(marker);
            }
            else if (marker is BeatEmitter)
            {
                beatEmitters.Add((BeatEmitter)marker);
            }
        }

        int count = beatEmitters.Count;
        double time;
        for (int i = 0; i < beatEmitters.Count; i++)
        {
            BeatEmitter curr = beatEmitters[i];
            curr.beatNb = count--;
            if (i > 0)
            {
                if (curr.time - beatEmitters[i - 1].time < timeToReact)
                {
                    time = beatEmitters[i - 1].time * 0.9;
                }
                else
                {
                    time = curr.time - timeToReact;
                }
            }
            else
            {
                time = curr.time - timeToReact;
            }
            switch (curr.action)
            {
                case "eAttack":
                    timelineAsset.GetOutputTrack(1).CreateMarker<EnemyWindUpSignal>(time);
                    break;
                default:
                    Debug.Log("No actions recognized for marker at time :" + curr.time);
                    break;
            }
        }
    }
}