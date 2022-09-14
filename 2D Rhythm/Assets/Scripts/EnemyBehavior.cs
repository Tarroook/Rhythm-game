using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;
    public bool isAttacking;
    public float phase = 0; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        sr.color = new Color(1, 1, 1, 1);
    }

    public void windUp()
    {
        Debug.Log("Enemy winds up");
        sr.color = new Color(1, 0.92f, 0.016f, 1);
        phase = 1;
    }// maybe make an event at beginning and end of an attack so player can't press more than once per attackEvent ?

    public void attack(double startTime)
    {
        
        StartCoroutine(attackLoop(startTime));
        //Debug.Log("Enemy has attacked");
    }

    IEnumerator attackLoop(double startTime) //first miss start as long as enemy is in windup : miss->good->perfect->good->miss
    {
        phase = 2;
        sr.color = new Color(1, 0.5f, 0.016f, 1);
        //Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime);
        phase = 3;
        sr.color = new Color(1, 0, 0, 1);
        //Debug.Log("phase = Perfect");
        yield return waitForNextPhase(startTime);
        phase = 2;
        sr.color = new Color(1, 0.5f, 0.016f, 1);
        //Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime);
        phase = 1;
        sr.color = new Color(0, 1, 0, 1);
        //Debug.Log("phase = Miss");
        yield return waitForNextPhase(startTime);
        sr.color = new Color(1, 1, 1, 1);
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
    }
}
