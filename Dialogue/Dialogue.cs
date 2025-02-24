using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // character(speaker)'s name
    public string name;

    // dialogue text
    [TextArea(3, 10)]
    public string[] sentences;
}
