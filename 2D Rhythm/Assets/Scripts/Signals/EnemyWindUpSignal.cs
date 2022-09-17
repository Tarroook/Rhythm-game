using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyWindUpSignal : SignalEmitter
{
    public string[] directions;

    public void setData(string name, string[] dir)
    {
        this.name = name;
        directions = dir;
    }
}
