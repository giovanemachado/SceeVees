using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection
{
    public Collection(int gemQtd, int goldQtd, int mineralQtd, int rubinQtd, int id)
    {
        this.id = id;
        this.GemQtd = gemQtd;
        this.GoldQtd = goldQtd;
        this.MineralQtd = mineralQtd;
        this.RubinQtd = rubinQtd;
    }

    public int id;
    public int GemQtd;
    public int GoldQtd;
    public int MineralQtd;
    public int RubinQtd;
}
