using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    public Resource.TypeEnum resource;
    public bool inUse;
    public GameObject realPos;

    public Position(Resource.TypeEnum resource, bool inUse, GameObject realPos)
    {
        this.resource = resource;
        this.inUse = inUse;
        this.realPos = realPos;
    }
}
