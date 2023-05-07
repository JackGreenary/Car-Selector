using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartColour
{
    public Color32 color;
    public string name;
    public float price;

    public PartColour(Color32 c, string n, float p)
    {
        color = c;
        name = n;
        price = p;
    }
}