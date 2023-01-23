using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionWall : MonoBehaviour
{
    public static event Action takingWall;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Debug.Log("Ta bouffé le mur");
            takingWall.Invoke();
        }
    }
}
