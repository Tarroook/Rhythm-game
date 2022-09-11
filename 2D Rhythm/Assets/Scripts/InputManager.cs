using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private SpriteRenderer sr;
    public float timeToPress = .3f; // 1/3 && 3/3 = good; 2/3 = perfect; 4/3 = miss (in seconds)
    public string button;
    public Sprite idleSprite;
    public Sprite attackSprite;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(button))
        {
            sr.sprite = attackSprite;
        }

        if (Input.GetButtonUp(button))
        {
            sr.sprite = idleSprite;
        }
    }
}
