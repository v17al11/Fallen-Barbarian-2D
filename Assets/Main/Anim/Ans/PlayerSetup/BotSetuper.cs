using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSetuper : BasePlayerSetiuper
{
    public Color HairColor;

    protected override void ChangeHairColor()
    {
        _hairSpriteRenderer.color = HairColor;
    }
}
