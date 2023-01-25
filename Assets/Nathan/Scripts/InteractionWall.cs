using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InteractionWall
{
    [SerializeField] float distance;
    [SerializeField] LayerMask _mask;
    [SerializeField] bool takingWall;

    public void CheckRayCastWall(Vector3 positionValue)
    {
        RaycastHit hit;

        if (Physics.Raycast(positionValue,Vector3.forward , out hit , distance , _mask))
        {
            takingWall = true;
        }
    }

    public void setDistance(float value)
    {
        distance = value;
    }

    public bool getTakingWall()
    {
        return takingWall;
    }
    public void setTakingWall(bool value)
    {
        takingWall = value;
    }
}
