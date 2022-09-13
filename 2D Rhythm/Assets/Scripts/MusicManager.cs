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
    private MusicData currMusicData;
    private InputManager inputManager;
    private int currDifficulty;
    public UnityEvent beatEvent;
    private IEnumerator beatLoop;


    // Start is called before the first frame update
    void Awake()
    {
        playableDirector = gameObject.GetComponent<PlayableDirector>();
        musicList = gameObject.GetComponent<MusicList>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        beatEvent = new UnityEvent();
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
        currDifficulty = difficulty;
        Debug.Log("set Music");
        playableDirector.playableAsset = currMusicData.timeline[currDifficulty];
        setSignals();
    }

    public void setSignals()
    {
        TimelineAsset timelineAsset = currMusicData.timeline[currDifficulty];
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
            if (marker is EnemyWindUpSignal || marker is EnemyAttackSignal)
            {
                timelineAsset.GetOutputTrack(2).DeleteMarker(marker);
            }
        }

        beatEmitters.Sort(VariousMethods.sortByTime);
        int count = 0;
        float windUpTime;
        double windUpSigTime;

        double attackSigTime;
        double attackSigOffset;
        for (int i = 0; i < beatEmitters.Count; i++)
        {
            BeatEmitter curr = beatEmitters[i];
            curr.beatNb = ++count;
            windUpTime = curr.timeToReact * currMusicData.getBps();
            windUpSigTime = curr.time - windUpTime;
            attackSigTime = curr.time - inputManager.timeToPress / 2;
            attackSigOffset = 0;
            if (i > 0)
            {
                if (curr.time - beatEmitters[i - 1].time < windUpTime)
                {
                    windUpSigTime = beatEmitters[i - 1].time + ((curr.time - beatEmitters[i - 1].time) * 0.1);
                    if (curr.time - windUpSigTime < inputManager.timeToPress)
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

    IEnumerator BeatLoop() // needs to be started by event
    {
        float bps = currMusicData.getBps();
        int count = 0;
        while (true)
        {
            beatEvent.Invoke();
            Debug.Log("Beat : " + count++);
            yield return new WaitForSeconds(bps);
        }
    }
}