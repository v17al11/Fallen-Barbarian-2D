using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetuper : BasePlayerSetiuper
{
    protected override void ChangeHairColor()
    {
        _hairSpriteRenderer.color = Color.green;
    }
}
