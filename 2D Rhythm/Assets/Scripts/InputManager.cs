using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected SpriteRenderer sr;
    public float timeToPress = .4f; // check InputReacts for maths

    public delegate void attackAction();
    public static event attackAction onPressedAttack;

    public delegate void dodgedRightAction();
    public static event dodgedRightAction onDodgeRight;

    public delegate void dodgedLeftAction();
    public static event dodgedLeftAction onLeftRight;

    public delegate void dodgedBackAction();
    public static event dodgedBackAction onBackRight;

    public delegate void hitAction();
    public static event hitAction onHit;

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
        if (Input.GetButtonDown("Attack Forward"))
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
            inputEvents("attack");
        }
    }

    public void dodgePress(AttackReact ar)
    {
        string directionDodged = "none";
        if (Input.GetButtonDown("Dodge Left"))
            directionDodged = "left";
        else if (Input.GetButtonDown("Dodge Right"))
            directionDodged = "right";
        else if (Input.GetButtonDown("Dodge Back"))
            directionDodged = "back";

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
            inputEvents(directionDodged);
        }
    }

    public void inputEvents(string direction)
    {
        switch (direction)
        {
            case "left":
                break;
            case "right":
                break;
            case "back":
                break;
            case "attack":
                break;
        }
    }
}
