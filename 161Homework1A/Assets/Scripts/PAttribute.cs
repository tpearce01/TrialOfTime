using UnityEngine;
using System.Collections;

public class PAttribute
{
    public float baseValue;
    public float modifier;

    public PAttribute()
    {
        baseValue = 0;
        modifier = 0;
    }

    public float Value()
    {
        return baseValue + modifier;
    }
}
