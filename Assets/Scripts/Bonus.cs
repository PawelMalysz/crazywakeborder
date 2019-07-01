using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonus
{

    void UpdateActions(GameObject go);
    void GiveEffect(PlayerController pc);

}
