using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPreset : MonoBehaviour
{
    //Each flower has these
    public string Petal;
    public string Stem;
    public string Leaf;

    public FlowerPreset()
    {


    }

    public FlowerPreset(string petal, string stem, string leaf)
    {
        Petal = petal;
        Stem = stem;
        Leaf = leaf;
    }
}
