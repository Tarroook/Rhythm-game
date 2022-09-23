using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private MusicData currMusic;

    private void OnEnable()
    {
        InputManager.onAttack += attackParticles;
        InputManager.onDodged += dodgedParticles;
        InputManager.onHit += hitParticles;
        MusicManager.onMusicChanged += setMusic;
    }

    private void OnDisable()
    {
        InputManager.onAttack -= attackParticles;
        InputManager.onDodged -= dodgedParticles;
        InputManager.onHit -= hitParticles;
        MusicManager.onMusicChanged -= setMusic;
    }

    private void setMusic(MusicData music)
    {
        currMusic = music;
    }

    private void attackParticles(int timing)
    {
        ParticleSystem particle = null;
        switch (timing)
        {
            case 1:
                particle = currMusic.environment.missTimingParticle;
                break;
            case 2:
                particle = currMusic.environment.goodTimingParticle;
                break;
            case 3:
                particle = currMusic.environment.perfectTimingParticle;
                break;
        }
        Instantiate(particle, transform.position, particle.transform.rotation);
    }

    private void dodgedParticles(int timing)
    {
        ParticleSystem particle = null;
        switch (timing)
        {
            case 1:
                particle = currMusic.environment.missTimingParticle;
                break;
            case 2:
                particle = currMusic.environment.goodTimingParticle;
                break;
            case 3:
                particle = currMusic.environment.perfectTimingParticle;
                break;
        }
        Instantiate(particle, transform.position, particle.transform.rotation);
    }

    private void hitParticles()
    {
        ParticleSystem particle = currMusic.environment.missTimingParticle;
        Instantiate(particle, transform.position, particle.transform.rotation);
    }
}
