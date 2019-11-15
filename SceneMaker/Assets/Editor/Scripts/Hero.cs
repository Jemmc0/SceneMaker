using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public HeroConfig data;

    private void Awake()
    {
        Debug.Log(data.godMode);

        foreach (var item in data.inventory)
            Debug.Log(item);
    }
}