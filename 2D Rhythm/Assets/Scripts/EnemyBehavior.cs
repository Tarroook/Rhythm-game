using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;
    public bool isAttacking = false;
    //public float phase = 0; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
    private InputManager inputManager;
    public delegate void windUpAction(AttackReact attackReact);
    public static event windUpAction onWindUp;
    public delegate void attackAction(double startTime);
    public static event attackAction onAttack;
    public GameObject nextReactsGB;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        sr.color = new Color(1, 1, 1, 1);
    }

    public void windUp()
    {
        //StopCoroutine(vulnerableLoop(0));
        Debug.Log("Enemy winds up");
        if (onWindUp != null)
        {
            AttackReact ar = nextReactsGB.AddComponent<AttackReact>();
            ar.sr = sr;
            onWindUp(ar);
        }
        sr.color = new Color(1, 0.92f, 0.016f, 1);
        //phase = 1;
    }// maybe make an event at beginning and end of an attack so player can't press more than once per attackEvent ?

    public void attack(double startTime)
    {
        if (onAttack != null)
            onAttack(startTime);
        //StartCoroutine(vulnerableLoop(startTime));
        // Debug.Log("Enemy has attacked");
    }
    /*
    IEnumerator vulnerableLoop(double startTime) // first miss start as long as enemy is in windup : miss->good->perfect->good->miss
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
        isAttacking = false;
        phase = 0;
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
    }*/
}
