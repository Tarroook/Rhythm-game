using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReact : MonoBehaviour
{
    public float phase = 1; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
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

    protected IEnumerator phaseLoop(double startTime) //first miss start as long as enemy is in windup : miss->good->perfect->good->miss
    {
        phase = 2;
        //Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime, 2, 10);
        phase = 3;
        //Debug.Log("phase = Perfect");
        yield return waitForNextPhase(startTime, 6, 10);
        phase = 2;
        //Debug.Log("phase = Good");
        yield return waitForNextPhase(startTime, 2, 10);
        phase = 1;
        //Debug.Log("phase = Miss");
        yield return waitForNextPhase(startTime, 1, 3);
        if (onTimeOver != null)
            onTimeOver(this);
    }

    IEnumerator waitForNextPhase(double startTime, int multiplier, int divider)
    {
        float totalTime = inputManager.timeToPress;
        double waitTime = ((inputManager.timeToPress * multiplier) / divider) - startTime;
        if (waitTime > 0) // default
        {
            yield return new WaitForSeconds((float)waitTime);
        }
        else if (waitTime > ((inputManager.timeToPress * multiplier) / divider)) // if waitTime too big (shouldn't happen) 
        {
            yield return new WaitForSeconds(inputManager.timeToPress / divider);
        }
        else // if waitTime is < 0
        {
            yield return new WaitForSeconds(0);
        }
    }
}
