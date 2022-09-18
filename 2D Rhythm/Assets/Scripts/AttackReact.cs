using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReact : InputReact
{
    public string[] directions;
    private void OnEnable()
    {
        EnemyBehavior.onAttack += attack;
    }

    private void OnDisable()
    {
        EnemyBehavior.onAttack -= attack;
    }

    public void attack(double startTime)
    {
        EnemyBehavior.onAttack -= attack;
        StartCoroutine(phaseLoop(startTime));
    }

    public bool hits(string direction)
    {
        foreach(string s in directions)
        {
            if(directions == null)
                Debug.Log("null info");
            
            if (s.Equals(direction))
                return true;
        }
        return false;
    }
}
