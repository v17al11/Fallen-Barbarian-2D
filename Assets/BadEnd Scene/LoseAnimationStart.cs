using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseAnimationStart : MonoBehaviour
{
    [SerializeField] private string _animationToPlay;

    private Animator _animator;

    public SpriteRenderer _loseHair;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(_animationToPlay);
        Destroy(gameObject, 2.09f);
    }
}
