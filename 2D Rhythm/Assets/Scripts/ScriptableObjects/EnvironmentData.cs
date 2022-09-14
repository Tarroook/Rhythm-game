using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnvironmentData", menuName = "EnvironmentData")]
public class EnvironmentData : ScriptableObject
{
    public Sprite playerSprite; // maybe replace by a characterData scriptableObject so I can store the animations and stuff in it, or just a prefab of a character
    public Sprite enemySprite; // same as player
    public ParticleSystem missTimingParticle;
    public ParticleSystem goodTimingParticle;
    public ParticleSystem perfectTimingParticle;
    // add something for the background, not sure what yet
}
