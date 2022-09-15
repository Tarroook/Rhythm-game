using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReact : MonoBehaviour
{

    public float phase = 1; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
    private SpriteRenderer sr;
    public bool canPress = true;
    private InputManager inputManager;
    public delegate void attackOver(AttackReact thisToDelete);
    public event attackOver onAttackOver;
    private bool isStarted = false;

    public AttackReact(SpriteRenderer sr)
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        this.sr = sr;
        EnemyBehavior.onAttack += attack;
    }

    void attack(double startTime)
    {
        EnemyBehavior.onAttack -= attack;
        StartCoroutine(vulnerableLoop(startTime));
    }

    IEnumerator vulnerableLoop(double startTime) //first miss start as long as enemy is in windup : miss->good->perfect->good->miss
    {
        phase = 2;
        sr.color = new Color(1, 0.5f, 0.016f, 1);
        Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime);
        phase = 3;
        sr.color = new Color(1, 0, 0, 1);
        Debug.Log("phase = Perfect");
        yield return waitForNextPhase(startTime);
        phase = 2;
        sr.color = new Color(1, 0.5f, 0.016f, 1);
        Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime);
        phase = 1;
        sr.color = new Color(0, 1, 0, 1);
        Debug.Log("phase = Miss");
        yield return waitForNextPhase(startTime);
        sr.color = new Color(1, 1, 1, 1);
        if (onAttackOver != null)
            onAttackOver(this);
    }

    IEnumerator waitForNextPhase(double startTime)
    {
        double waitTime = (inputManager.timeToPress / 3) - startTime;
        if (waitTime > 0) // default
        {
            yield return new WaitForSeconds((float)waitTime);
        }
        else if (waitTime > inputManager.timeToPress / 3) // if waitTime too big (shouldn't happen) 
        {
            yield return new WaitForSeconds(inputManager.timeToPress / 3);
        }
        else // if waitTime is < 0
        {
            yield return new WaitForSeconds(0);
        }
    }
}
