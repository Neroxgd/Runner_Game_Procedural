using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InteractionWall
{
    [SerializeField] float distance;
    [SerializeField] LayerMask _mask;

    public void CheckRayCastWall(Vector3 positionValue)
    {
        RaycastHit hit;

        if (Physics.Raycast(positionValue,Vector3.forward , out hit , distance , _mask))
        {
            Debug.Log("Ta bouffé le mur");
        }
    }

    public void setDistance(float value)
    {
        distance = value;
    }
}
