using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using System;

public class RollReturnDamage : MonoBehaviour
{
    private void Awake()
    {
        UFE.OnParry += OnParry;
    }

    private void OnParry(HitBox strokeHitBox, MoveInfo move, ControlsScript player)
    {
        var enemy = UFE.GetPlayer(2);
        Debug.Log(enemy);
        Debug.Log("OnParry: " + player.character.name);
        Debug.Log(move);
        Debug.Log(strokeHitBox.bodyPart);
        if (move)
        {
            Debug.Log("Apply Damage");
            enemy.lifePoints -= 10;
        }
    }
}
