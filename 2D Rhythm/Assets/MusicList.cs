using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public MusicData[] musicDatas;

    public MusicData getMusicFromName(string name)
    {
        foreach(MusicData md in musicDatas)
        {
            if (md.name.Equals(name))
            {
                return md;
            }
        }
        return null;
    }
}
