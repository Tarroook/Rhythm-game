using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{
    private Animator anim;
    private MusicData currMusic;
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
        if (stopDodge != null)
            StopCoroutine(stopDodge);
        anim.SetTrigger("SlideUp");
        stopDodge = StartCoroutine(returnMiddleCoroutine(.5f));
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
        stopDodge = StartCoroutine(returnMiddleCoroutine(1));
    }

    private IEnumerator returnMiddleCoroutine(float nbBeat)
    {
        Debug.Log("started coroutine");
        yield return new WaitForSeconds(currMusic.getBps() * nbBeat);
        Debug.Log("return to middle");
        anim.SetTrigger("ReturnToMiddle");
    }
}
