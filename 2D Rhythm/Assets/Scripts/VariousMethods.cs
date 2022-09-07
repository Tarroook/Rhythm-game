using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public static class VariousMethods
{
    public static int sortByTime(SignalEmitter s1, SignalEmitter s2)
    {
        return s1.time.CompareTo(s2.time);
    }
}
