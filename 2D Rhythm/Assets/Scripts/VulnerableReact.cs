using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableReact : InputReact
{
    private void OnEnable()
    {
        EnemyBehavior.onDefenseless += defenseless;
    }

    private void OnDisable()
    {
        EnemyBehavior.onDefenseless -= defenseless;
    }

    public void defenseless(double startTime)
    {
        EnemyBehavior.onAttack -= defenseless;
        StartCoroutine(phaseLoop(startTime));
    }
}
