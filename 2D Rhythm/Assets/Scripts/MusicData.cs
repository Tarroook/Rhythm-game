using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "NewMusicData", menuName = "MusicData")]
public class MusicData : ScriptableObject
{
    public new string name;
    public float bpm;
    public TimelineAsset[] timeline;

    public float getBps()
    {
        return 1 / (bpm / 60);
    }
}
