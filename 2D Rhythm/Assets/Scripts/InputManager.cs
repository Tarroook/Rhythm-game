using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected SpriteRenderer sr;
    public float timeToPress = .4f; // check InputReacts for maths
    public string attackButton;
    public string dodgeRightButton;
    public string dodgeLeftButton;
    public string dodgeBackButton;
    private MusicManager musicManager;
    public List<InputReact> nextInputs;
    public GameObject nextReactsGB;


    private void OnDisable()
    {
        EnemyBehavior.onWindUp -= addAttackReact;
        EnemyBehavior.onVulnerable -= addVulnerableReact;
        foreach (InputReact ir in nextInputs)
        {
            ir.onTimeOver -= removeReact;
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        musicManager = GameObject.FindGameObjectWithTag("Music Director").GetComponent<MusicManager>();
        nextInputs = new List<InputReact>();
        EnemyBehavior.onWindUp += addAttackReact;
        EnemyBehavior.onVulnerable += addVulnerableReact;
    }

    private void Update()
    {
        if (nextInputs.Count == 0)
            return;
        InputReact ir = nextInputs[0];
        if (ir is AttackReact)
            dodgePress(ir as AttackReact);
        else if (ir is VulnerableReact)
            attackPress(ir as VulnerableReact);
    }

    private void addAttackReact(string[] dir)
    {
        AttackReact ar = nextReactsGB.AddComponent<AttackReact>();
        ar.sr = sr;
        nextInputs.Add(ar);
        ar.directions = dir;
        ar.onTimeOver += removeReact;
    }

    private void addVulnerableReact()
    {
        
        VulnerableReact vr = nextReactsGB.AddComponent<VulnerableReact>();
        vr.sr = sr;
        nextInputs.Add(vr);
        vr.onTimeOver += removeReact;
    }

    private void removeReact(InputReact ir)
    {
        nextInputs.Remove(ir);
        ir.onTimeOver -= removeReact;
        ir.endReact();
    }

    public void attackPress(VulnerableReact vr) // need to create new type for "vulnerable phases
    {
        if (Input.GetButtonDown(attackButton))
        {
            switch (vr.phase)
            {
                case 0: // enemy not attacking
                    Debug.Log("Player attacked while enemy is not attacking");
                    break;
                case 1: // miss
                    Debug.Log("Player missed");
                    ParticleSystem missParticle = musicManager.currMusicData.environment.missTimingParticle;
                    Instantiate(missParticle, new Vector2(transform.position.x, transform.position.y), missParticle.transform.rotation);
                    removeReact(vr);
                    break;
                case 2: // good
                    Debug.Log("Player attacked good");
                    ParticleSystem goodParticle = musicManager.currMusicData.environment.goodTimingParticle;
                    Instantiate(goodParticle, new Vector2(transform.position.x, transform.position.y), goodParticle.transform.rotation);
                    removeReact(vr);
                    break;
                case 3: // perfect
                    Debug.Log("Player attacked perfect");
                    ParticleSystem perfParticle = musicManager.currMusicData.environment.perfectTimingParticle;
                    Instantiate(perfParticle, new Vector2(transform.position.x, transform.position.y), perfParticle.transform.rotation);
                    removeReact(vr);
                    break;
                default:
                    Debug.Log("Enemy phase is wrong");
                    break;
            }
        }
        else if (Input.GetButtonUp(attackButton))
        {

        }
    }

    public void dodgePress(AttackReact ar)
    {
        string directionDodged = "none";
        if (Input.GetButtonDown(dodgeLeftButton))
            directionDodged = "left";
        else if (Input.GetButtonDown(dodgeRightButton))
            directionDodged = "right";
        else if (Input.GetButtonDown(dodgeBackButton))
            directionDodged = "front";

        if(!directionDodged.Equals("none"))
        {
            if (ar.hits(directionDodged)){
                Debug.Log("Player got hit in wrong dir");
                removeReact(ar);
            }
            else
            {
                switch (ar.phase)
                {
                    case 0: // enemy not attacking
                        Debug.Log("Player attacked while enemy is not attacking");
                        break;
                    case 1: // miss
                        Debug.Log("Player got hit");
                        ParticleSystem missParticle = musicManager.currMusicData.environment.missTimingParticle;
                        Instantiate(missParticle, new Vector2(transform.position.x, transform.position.y), missParticle.transform.rotation);
                        removeReact(ar);
                        break;
                    case 2: // good
                        Debug.Log("Player dodged good");
                        ParticleSystem goodParticle = musicManager.currMusicData.environment.goodTimingParticle;
                        Instantiate(goodParticle, new Vector2(transform.position.x, transform.position.y), goodParticle.transform.rotation);
                        removeReact(ar);
                        break;
                    case 3: // perfect
                        Debug.Log("Player dodged perfect");
                        ParticleSystem perfParticle = musicManager.currMusicData.environment.perfectTimingParticle;
                        Instantiate(perfParticle, new Vector2(transform.position.x, transform.position.y), perfParticle.transform.rotation);
                        removeReact(ar);
                        break;
                    default:
                        Debug.Log("Enemy phase is wrong");
                        break;
                }
            }
        }
    }
}
