using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NoteSpawner : MonoBehaviour
{
    private PlayableDirector playableDirector;
    public float bpm;
    private float bps;
    [SerializeField]private float windUpTime; // this is in bps
    private float timeToReact = .3f; // this is in seconds



    // Start is called before the first frame update
    void Start()
    {
        bps = 1 / (bpm / 60);
        windUpTime = 1 * bps;

        playableDirector = gameObject.GetComponent<PlayableDirector>();

        TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
        IMarker[] beatMarkers = timelineAsset.GetOutputTrack(1).GetMarkers().ToArray();
        IMarker[] otherMarkers = timelineAsset.GetOutputTrack(2).GetMarkers().ToArray();


        List<BeatEmitter> beatEmitters = new List<BeatEmitter>();
        
        foreach(IMarker marker in beatMarkers)
        {
            if (marker is BeatEmitter)
            {
                beatEmitters.Add((BeatEmitter)marker);
            }
        }
        foreach(IMarker marker in otherMarkers)
        {
            if (marker is EnemyWindUpSignal || marker is EnemyAttackSignal)
            {
                timelineAsset.GetOutputTrack(2).DeleteMarker(marker);
            }
        }

        beatEmitters.Sort(VariousMethods.sortByTime);
        int count = 0;
        double windUpSigTime;
        double attackSigTime;
        double attackSigOffset;
        for (int i = 0; i < beatEmitters.Count; i++)
        {
            BeatEmitter curr = beatEmitters[i];
            curr.beatNb = ++count;
            windUpSigTime = curr.time - windUpTime;
            attackSigTime = curr.time - timeToReact / 2;
            attackSigOffset = 0;
            if (i > 0)
            {
                if (curr.time - beatEmitters[i - 1].time < windUpTime)
                {
                    windUpSigTime = beatEmitters[i - 1].time + ((curr.time - beatEmitters[i - 1].time) * 0.1);
                    if (curr.time - windUpSigTime < timeToReact)
                    {
                        attackSigTime = windUpSigTime + ((curr.time - windUpTime) * 0.05);
                        attackSigOffset = curr.time - windUpSigTime;
                    }
                }
            }
            switch (curr.action)
            {
                case "eAttack":
                    timelineAsset.GetOutputTrack(2).CreateMarker<EnemyWindUpSignal>(windUpSigTime).name = "EnemyWindUpSignal :" + curr.beatNb;
                    timelineAsset.GetOutputTrack(2).CreateMarker<EnemyAttackSignal>(attackSigTime).setData("EnemyAttackSignal :" + curr.beatNb, attackSigOffset);
                    break;
                default:
                    Debug.Log("No actions recognized for marker at time :" + curr.time);
                    break;
            }
        }
    }
}