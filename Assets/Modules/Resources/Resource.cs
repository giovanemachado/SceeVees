using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public Resource(TypeEnum type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }

    public enum TypeEnum
    {
        Gem,
        Gold,
        Mineral,
        Rubin
    }

    public TypeEnum type;
    public int amount;
}

