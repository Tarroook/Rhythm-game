using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    private Animator anim;
    private MusicData currMusic;
    private bool stopDodgeRunning;
    private Coroutine stopDodge;

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
        MusicManager.onMusicChanged -= setMusic;
        InputManager.onPressedAttack -= attackAnimation;
        InputManager.onPressedDodge -= dodgeAnimation;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void setMusic(MusicData music)
    {
        currMusic = music;
    }

    private void beatAnimation()
    {
        anim.SetTrigger("Bounce");
    }

    private void attackAnimation()
    {
        
    }

    private void dodgeAnimation(string direction)
    {
        if(stopDodge != null)
            StopCoroutine(stopDodge);
        switch (direction)
        {
            case "left":
                anim.SetTrigger("SlideLeft");
                break;
            case "right":
                anim.SetTrigger("SlideRight");
                break;
        }
        stopDodge = StartCoroutine(stopDodgeCoroutine());
    }

    private IEnumerator stopDodgeCoroutine()
    {
        Debug.Log("started coroutine");
        yield return new WaitForSeconds(currMusic.getBps());
        Debug.Log("return to middle");
        anim.SetTrigger("ReturnToMiddle");
    }
}
