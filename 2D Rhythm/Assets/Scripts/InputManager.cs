using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private SpriteRenderer sr;
    public float timeToPress = .4f; // 1/3 && 3/3 = good; 2/3 = perfect; 4/3 = miss (in seconds)
    public string attackButton;
    public string dodgeRightButton;
    public string dodgeLeftButton;
    public string dodgeBackButton;
    private EnemyBehavior enemybh;
    private MusicManager musicManager;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        musicManager = GameObject.FindGameObjectWithTag("Music Director").GetComponent<MusicManager>();
        enemybh = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehavior>();
    }

    private void Update()
    {
        attackPress();
    }

    public void attackPress()
    {
        if (Input.GetButtonDown(attackButton))
        {
            if (enemybh.isAttacking)
            {
                switch (enemybh.phase)
                {
                    case 0: // enemy not attacking
                        Debug.Log("Player attacked while enemy is not attacking");
                        break;
                    case 1: // miss
                        Debug.Log("Player missed");
                        break;
                    case 2: // good
                        Debug.Log("Player attacked good");
                        ParticleSystem goodParticle = musicManager.currMusicData.environment.goodTimingParticle;
                        Instantiate(goodParticle, new Vector2(transform.position.x, transform.position.y), goodParticle.transform.rotation);
                        break;
                    case 3: // perfect
                        Debug.Log("Player attacked perfect");
                        break;
                    default:
                        Debug.Log("Enemy phase is wrong");
                        break;
                }
            }
        }
        else if (Input.GetButtonUp(attackButton))
        {

        }
    }
}
