using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward
{
    //reward class
   
    public Sprite baseSprite;
    public int baseAmount;

    public Reward(Sprite sprite, int amount)
    {
        baseSprite = sprite;
        baseAmount = amount;
    }

}
