using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite attackSprite;
    public bool isAttacking = false;
    //public float phase = 0; // 0 = not attacking; 1 = miss; 2 = good; 3 = perfect
    private InputManager inputManager;

    public delegate void windUpAction(AttackReact attackReact, string[] dir);
    public static event windUpAction onWindUp;
    public delegate void attackAction(double startTime);
    public static event attackAction onAttack;
    public delegate void vulnerableAction(VulnerableReact vr);
    public static event vulnerableAction onVulnerable;
    public delegate void defenselessAction(double startTime);
    public static event defenselessAction onDefenseless;
    public GameObject nextReactsGB;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        sr.color = new Color(1, 1, 1, 1);
    }

    public void windUp(string[] directions)
    {
        //StopCoroutine(phaseLoop(0));
        //Debug.Log("Enemy winds up");
        if (onWindUp != null)
        {
            AttackReact ar = nextReactsGB.AddComponent<AttackReact>();
            ar.sr = sr;
            onWindUp(ar, directions);
        }
        sr.color = new Color(1, 0.92f, 0.016f, 1);
        //phase = 1;
    }// maybe make an event at beginning and end of an attack so player can't press more than once per attackEvent ?

    public void attack(double startTime)
    {
        if (onAttack != null)
            onAttack(startTime);
        //StartCoroutine(phaseLoop(startTime));
        // Debug.Log("Enemy has attacked");
    }

    public void vulnerable()
    {
        if (onVulnerable != null)
        {
            Debug.Log("Enemy vul");
            VulnerableReact vr = nextReactsGB.AddComponent<VulnerableReact>();
            vr.sr = sr;
            onVulnerable(vr);
        }
        sr.color = new Color(1, 0.92f, 0.016f, 1);
    }

    public void defenseless(double startTime)
    {
        Debug.Log("Enemy defenseless");
        if (onDefenseless != null)
            onDefenseless(startTime);
    }
}
