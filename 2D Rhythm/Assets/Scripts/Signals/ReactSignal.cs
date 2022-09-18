using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ReactSignal : SignalEmitter
{
    public double offset;

    public void setData(string name, double offset)
    {
        this.name = name;
        this.offset = offset;
    }
}
