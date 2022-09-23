using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MusicManager : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private MusicList musicList;
    public MusicData currMusicData;
    public delegate void changedMusicAction(MusicData music);
    public static event changedMusicAction onMusicChanged;
    private InputManager inputManager;
    public delegate void beatAction();
    public static event beatAction onBeat;
    private IEnumerator beatLoop;


    // Start is called before the first frame update
    void Awake()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();
        musicList = gameObject.GetComponent<MusicList>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        beatLoop = BeatLoop();
        setMusic("Harlem Shuffle", 0);
    }

    public void startPlaying()
    {
        playableDirector.Play();
    }

    public void songStarted()
    {
        StartCoroutine(beatLoop);
    }

    public void setMusic(string musicName, int difficulty)
    {
        currMusicData = musicList.getMusicFromName(musicName);
        if (onMusicChanged != null)
            onMusicChanged(currMusicData);
        Debug.Log("set Music");
        playableDirector.playableAsset = currMusicData.timeline;
        setSignals();
    }

    public void setSignals()
    {
        TimelineAsset timelineAsset = currMusicData.timeline;
        IMarker[] beatMarkers = timelineAsset.GetOutputTrack(1).GetMarkers().ToArray();
        IMarker[] otherMarkers = timelineAsset.GetOutputTrack(2).GetMarkers().ToArray();


        List<BeatEmitter> beatEmitters = new List<BeatEmitter>();

        foreach (IMarker marker in beatMarkers)
        {
            if (marker is BeatEmitter)
            {
                beatEmitters.Add((BeatEmitter)marker);
            }
        }
        foreach (IMarker marker in otherMarkers)
        {
            if (marker is EnemyWindUpSignal || marker is EnemyAttackSignal || marker is EnemyVulnerableSignal || marker is PlayerAttackSignal)
            {
                timelineAsset.GetOutputTrack(2).DeleteMarker(marker);
            }
        }

        beatEmitters.Sort(VariousMethods.sortByTime);
        int count = 0;
        float windUpTime;
        double firstSigTime;

        double reactSigTime;
        double attackSigOffset;
        for (int i = 0; i < beatEmitters.Count; i++)
        {
            BeatEmitter curr = beatEmitters[i];
            curr.beatNb = ++count;
            windUpTime = curr.timeToReact * currMusicData.getBps();
            firstSigTime = curr.time - windUpTime;
            reactSigTime = curr.time - inputManager.timeToPress / 2;
            attackSigOffset = 0;
            if (i > 0)
            {
                if (curr.time - beatEmitters[i - 1].time < windUpTime)
                {
                    firstSigTime = beatEmitters[i - 1].time + ((curr.time - beatEmitters[i - 1].time) * 0.1);
                    if (curr.time - firstSigTime < inputManager.timeToPress)
                    {
                        reactSigTime = firstSigTime + ((curr.time - windUpTime) * 0.05);
                        attackSigOffset = curr.time - firstSigTime;
                    }
                }
            }
            switch (curr.action)
            {
                case "eAttack":
                    timelineAsset.GetOutputTrack(2).CreateMarker<EnemyWindUpSignal>(firstSigTime).setData("EnemyWindUpSignal :" + curr.beatNb, curr.direction);
                    timelineAsset.GetOutputTrack(2).CreateMarker<EnemyAttackSignal>(reactSigTime).setData("EnemyAttackSignal :" + curr.beatNb, attackSigOffset);
                    break;
                case "pAttack":
                    timelineAsset.GetOutputTrack(2).CreateMarker<EnemyVulnerableSignal>(firstSigTime).name = "EnemyVulnerableSignal : " + curr.beatNb;
                    timelineAsset.GetOutputTrack(2).CreateMarker<PlayerAttackSignal>(reactSigTime).setData("PlayerAttackSignal :" + curr.beatNb, attackSigOffset);
                    break;
                default:
                    Debug.Log("No actions recognized for marker at time :" + curr.time);
                    break;
            }
        }
    }

    IEnumerator BeatLoop() // needs to be started by event
    {
        float bps = currMusicData.getBps();
        //int count = 0;
        while (true)
        {
            if (onBeat != null)
                onBeat();
            //Debug.Log("Beat : " + count++);
            yield return new WaitForSeconds(bps);
        }
    }
}