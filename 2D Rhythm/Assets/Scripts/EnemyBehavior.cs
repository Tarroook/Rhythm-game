using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;
    public bool isAttacking;
    public float phase = 0; 

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 1);
    }

    public void windUp()
    {
        Debug.Log("Enemy winds up");
        sr.color = new Color(1, 0.92f, 0.016f, 1);
    }

    public void attack(double startTime)
    {
        sr.color = new Color(1, 0, 0, 1);
        StartCoroutine(attackLoop(startTime));
        Debug.Log("Enemy has attacked");
    }

    IEnumerator attackLoop(double startTime) //miss->good->perfect->good->miss
    {
        isAttacking = true;
        phase = 1;
        double waitTime = startTime;
        yield return new WaitForSeconds((float)waitTime);
    }
}
