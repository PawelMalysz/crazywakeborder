﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour, IBonus
{
    public Engine engine;

    public void Start()
    {
        engine = GameObject.FindGameObjectWithTag("GM").GetComponent<Engine>();
        if (engine != null)
            engine.bonuses.Add(gameObject);

    }

    public void UpdateActions(GameObject go)
    {
        go.transform.Rotate(0, 1, 0);
    }

    public void GiveEffect(PlayerController pc)
    {
        pc.armor.SetActive(true);
    }
}
