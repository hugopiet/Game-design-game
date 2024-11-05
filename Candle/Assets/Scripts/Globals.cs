using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Bounds WorldBounds;

    private void Awake()
    {

        var bounds = GetComponent<SpriteRenderer>().bounds;
        Globals.WorldBounds = bounds; 
    }
}
