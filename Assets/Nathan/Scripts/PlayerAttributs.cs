using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributs
{
    int hp;
    float speed;

    void Start()
    {
        setHP(3);
        setSpeed(0.5f);
        
    }
    void Update()
    {
        Debug.Log(hp);
    }

    void setHP(int value)
    {
        hp = value;
    }

    void setSpeed(float value)
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
