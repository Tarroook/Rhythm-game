using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    public void attack()
    {
        sr.sprite = attackSprite;
        Debug.Log("Signal done");
    }
}
