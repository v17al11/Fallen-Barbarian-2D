using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSetuper : BasePlayerSetiuper
{
    protected override void ChangeHairColor()
    {
        _hairSpriteRenderer.color = Color.red;
    }
}
