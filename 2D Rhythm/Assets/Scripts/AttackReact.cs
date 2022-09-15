using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReact : InputReact
{
    private void OnEnable()
    {
        EnemyBehavior.onAttack += attack;
    }


    void attack(double startTime)
    {
        EnemyBehavior.onAttack -= attack;
        StartCoroutine(vulnerableLoop(startTime));
    }
}
