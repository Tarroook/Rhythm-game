using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    private Animator anim;
    private MusicData currMusic;
    private IEnumerator stopDodge;

    private void OnEnable()
    {
        MusicManager.onBeat += beatAnimation;
        MusicManager.onMusicChanged += setMusic;
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
        stopDodge = stopDodgeCoroutine();
    }

    private void setMusic(MusicData music)
    {
        currMusic = music;
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

        StopCoroutine(stopDodge);
        StartCoroutine(stopDodge);
    }

    private IEnumerator stopDodgeCoroutine()
    {
        yield return new WaitForSeconds(currMusic.getBps());
        anim.SetTrigger("ReturnToMiddle");
    }
}
