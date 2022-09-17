using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyAttackSignal : SignalEmitter
{
    public AttackInfo attackInfo;
    public void setData(string name, double offset, string[] dirr)
    {
        this.name = name;
        attackInfo.offset = offset;
        attackInfo.direction = dirr;
    }
}
