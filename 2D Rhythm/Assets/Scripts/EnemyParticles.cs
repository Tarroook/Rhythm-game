using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticles : MonoBehaviour
{
    public ParticleSystem tiredParticle;

    private void OnEnable()
    {
        EnemyBehavior.onVulnerable += vulnerablePart;
    }

    private void OnDisable()
    {
        EnemyBehavior.onVulnerable -= vulnerablePart;
    }


    private void vulnerablePart(VulnerableReact vr)
    {
        Instantiate(tiredParticle, gameObject.transform.position, tiredParticle.transform.rotation);
    }
}
