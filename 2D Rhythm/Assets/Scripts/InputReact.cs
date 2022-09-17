using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReact : MonoBehaviour
{
    public float phase = 1; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
    public SpriteRenderer sr;
    private InputManager inputManager;
    public delegate void timeOver(InputReact thisToDelete);
    public event timeOver onTimeOver;


    private void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }

    public void endReact()
    {
        StopAllCoroutines();
        Destroy(this);
    }

    protected IEnumerator vulnerableLoop(double startTime) //first miss start as long as enemy is in windup : miss->good->perfect->good->miss
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
        if (onTimeOver != null)
            onTimeOver(this);
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
