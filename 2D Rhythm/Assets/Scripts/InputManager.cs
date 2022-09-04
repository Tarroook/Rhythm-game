using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private SpriteRenderer sr;
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
