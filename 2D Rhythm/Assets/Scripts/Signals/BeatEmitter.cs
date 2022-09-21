using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BeatEmitter : SignalEmitter
{
    public string action;
    public int beatNb;
    public int timeToReact = 1; // in bps (ex : timeToReact * bps)
    public string[] direction = {"back", "right", "left"}; // need to remove at least one manually
}
