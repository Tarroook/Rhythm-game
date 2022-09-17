using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected SpriteRenderer sr;
    public float timeToPress = .4f; // 1/3 && 3/3 = good; 2/3 = perfect; 4/3 = miss (in seconds)
    public string attackButton;
    public string dodgeRightButton;
    public string dodgeLeftButton;
    public string dodgeBackButton;
    private EnemyBehavior enemybh;
    private MusicManager musicManager;
    public List<InputReact> nextInputs;


    private void OnDisable()
    {
        EnemyBehavior.onWindUp -= addAttackReact;
        foreach (InputReact ir in nextInputs)
        {
            ir.onTimeOver -= removeReact;
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        musicManager = GameObject.FindGameObjectWithTag("Music Director").GetComponent<MusicManager>();
        enemybh = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehavior>();
        nextInputs = new List<InputReact>();
        EnemyBehavior.onWindUp += addAttackReact;
    }

    private void Update()
    {
        if (nextInputs.Count == 0)
            return;
        InputReact ir = nextInputs[0];
        if (ir is AttackReact)
            dodgePress(ir as AttackReact);
    }

    private void addAttackReact(AttackReact ar, string[] dir)
    {
        nextInputs.Add(ar);
        ar.directions = dir;
        ar.onTimeOver += removeReact;
    }

    private void removeReact(InputReact ir)
    {
        nextInputs.Remove(ir);
        ir.onTimeOver -= removeReact;
        ir.endReact();
    }

    public void attackPress(AttackReact ar) // need to create new type for "vulnerable phases
    {
        if (Input.GetButtonDown(attackButton))
        {
            switch (ar.phase)
            {
                case 0: // enemy not attacking
                    Debug.Log("Player attacked while enemy is not attacking");
                    break;
                case 1: // miss
                    Debug.Log("Player missed");
                    ParticleSystem missParticle = musicManager.currMusicData.environment.missTimingParticle;
                    Instantiate(missParticle, new Vector2(transform.position.x, transform.position.y), missParticle.transform.rotation);
                    removeReact(ar);
                    break;
                case 2: // good
                    Debug.Log("Player attacked good");
                    ParticleSystem goodParticle = musicManager.currMusicData.environment.goodTimingParticle;
                    Instantiate(goodParticle, new Vector2(transform.position.x, transform.position.y), goodParticle.transform.rotation);
                    removeReact(ar);
                    break;
                case 3: // perfect
                    Debug.Log("Player attacked perfect");
                    ParticleSystem perfParticle = musicManager.currMusicData.environment.perfectTimingParticle;
                    Instantiate(perfParticle, new Vector2(transform.position.x, transform.position.y), perfParticle.transform.rotation);
                    removeReact(ar);
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
