using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;

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
        Debug.Log("Enemy has attacked");
    }

    IEnumerator attackLoop()
    {
        yield return new WaitForSeconds(1f);
    }
}
