using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerSetiuper : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _hairSpriteRenderer;
    protected GameObject _config;

    protected virtual void Start()
    {
        ChangeHairColor();
    }

    protected abstract void ChangeHairColor();
}
 