using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReact : InputReact
{
    AttackInfo currAttackInfo;

    private void OnEnable()
    {
        EnemyBehavior.onAttack += attack;
    }

    private void OnDisable()
    {
        EnemyBehavior.onAttack -= attack;
    }


    void attack(AttackInfo attackInfo)
    {
        EnemyBehavior.onAttack -= attack;
        currAttackInfo = attackInfo;
        StartCoroutine(vulnerableLoop(attackInfo.offset));
    }
}
