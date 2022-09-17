using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimations : MonoBehaviour
{

    private Animator anim;

    private void OnEnable()
    {
        MusicManager.onBeat += beatAnimation;
    }
    private void OnDisable()
    {
        MusicManager.onBeat -= beatAnimation;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void beatAnimation()
    {
        anim.Play("Bounce Layer.Player_Bounce", 1);
    }
}
