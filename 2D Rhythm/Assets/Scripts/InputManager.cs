using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float timeToPress = .4f; // check InputReacts for maths

    public delegate void attackPressedAction();
    public static event attackPressedAction onPressedAttack;

    public delegate void dodgedPressedAction(string direction);
    public static event dodgedPressedAction onPressedDodge;

    public delegate void attackAction(int timing);
    public static event attackAction onAttack;

    public delegate void dodgedAction(int timing);
    public static event dodgedAction onDodged;

    public delegate void hitAction();
    public static event hitAction onHit;

    
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
        nextInputs.Add(ar);
        ar.directions = dir;
        ar.onTimeOver += removeReact;
    }

    private void addVulnerableReact()
    {
        
        VulnerableReact vr = nextReactsGB.AddComponent<VulnerableReact>();
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
            if (onAttack != null)
                onAttack(vr.phase);
            removeReact(vr);
            if(onPressedAttack != null)
                onPressedAttack();
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
                if (onDodged != null)
                    onDodged(1);
            }
            else
            {
                if (ar.phase == 1 && onHit != null)
                    onHit();
                if (onDodged != null)
                    onDodged(ar.phase);
            }
            removeReact(ar);
            if (onPressedDodge != null)
                onPressedDodge(directionDodged);
        }
    }
}
