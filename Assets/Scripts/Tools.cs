using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools {

    public static void DestroyChildrens(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}
