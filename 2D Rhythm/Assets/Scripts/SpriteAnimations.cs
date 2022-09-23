using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        MusicManager.onBeat += beatAnimation;
        InputManager.onPressedAttack += attackAnimation;
        InputManager.onPressedDodge += dodgeAnimation;
    }
    private void OnDisable()
    {
        MusicManager.onBeat -= beatAnimation;
        InputManager.onPressedAttack -= attackAnimation;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void beatAnimation()
    {
        anim.Play("Bounce Layer.Player_Bounce", 1);
    }

    private void attackAnimation()
    {
        
    }

    private void dodgeAnimation(string direction)
    {
        switch (direction)
        {
            case "left":
                anim.SetTrigger("SlideLeft");
                break;
            case "right":
                anim.SetTrigger("SlideRight");
                break;
        }
    }
}
