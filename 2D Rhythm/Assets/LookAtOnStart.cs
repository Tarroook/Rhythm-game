using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtOnStart : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        transform.LookAt(2 * transform.position - target.position);
    }
}
