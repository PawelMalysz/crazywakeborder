using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPill : MonoBehaviour, IBonus
{
    public int speedUp;
    public float duration;
    public Engine engine;

    public void Start()
    {
        engine = GameObject.FindGameObjectWithTag("GM").GetComponent<Engine>();
        duration = 5f;
        if (engine!=null)
            engine.bonuses.Add(gameObject);

    }

    public void UpdateActions(GameObject go)
    {
        go.transform.Rotate(0, 1, 0);
    }

    public void GiveEffect(PlayerController pc)
    {
        pc.Multi += speedUp;
    }
}
