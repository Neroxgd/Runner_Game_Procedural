using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributs
{
    [SerializeField] int hp;
    [SerializeField] float speed;

    public void setHP(int value)
    {
        hp = value;
    }

    public void setSpeed(float value)
    {
        speed = value;
    }

    public int getHP()
    {
        return hp;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void LoseHP()
    {
        hp--;
    }

}
