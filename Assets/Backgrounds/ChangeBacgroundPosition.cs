using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBacgroundPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _positionToChange;

    private void Start()
    {
        transform.position = _positionToChange;
    }
}
