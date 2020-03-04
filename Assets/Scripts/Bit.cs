using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : MonoBehaviour
{
    int magic;
    int flyMagic = 1;
    int fireMagic = 1 << 2;
    int iceMagic = 1 << 3;


    /*
     * |= - ADD 
     * &= ~ - REMOVE 
     * ~ - Reverse
     * & - AND
     * << - BIT SHIFT LEFT
     * >> BIT SHIFT RIGHT
     * ^= SHITFT BIT ON and OFF (XOR)
     */

    private void Start()
    {
        flyMagic >>= 1;
        AddMagic(flyMagic);
        LookUpBits(magic);
        //RemoveMagic(flyMagic);
        HasMagic(flyMagic);
        AddMagic(fireMagic);
        HasMagic(fireMagic);
        HasMagic(flyMagic);
        AddMagic(iceMagic);
        HasMagic(fireMagic);
        LookUpBits(flyMagic);
    }
    void HasMagic(int magicToCheck)
    {
        if ((magic & magicToCheck) == magicToCheck)
        {
            Debug.Log("Can fly");
        }
    }

    void AddMagic(int magicToAdd)
    {
        magic |= magicToAdd;
    }

    void RemoveMagic(int magicToRemove)
    {
        magic &= ~magicToRemove;
    }

    void LookUpBits(int bitsToShow)
    {
        Debug.Log(Convert.ToString(bitsToShow, 2).PadLeft(8, '0')); // 1 decides how many of´the bytes will be shown
    }
}


