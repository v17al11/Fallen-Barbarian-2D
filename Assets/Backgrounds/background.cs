using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    Transform cameraTr;
    Vector3 lastCamPos;
    [SerializeField] float offect;
    [SerializeField] Vector2 offect3d = Vector2.one;
    bool rerol;

    void Start()
    {
        cameraTr = Camera.main.transform;
    }

    void LateUpdate ()
    {
        if (rerol)
        {
            Vector3 deltaMove = cameraTr.position - lastCamPos;
            deltaMove = new Vector3(deltaMove.x * offect3d.x, deltaMove.y * offect3d.y, 0);
            transform.position += deltaMove * offect / 100;
            lastCamPos = cameraTr.position;
        }
        else
        {
            lastCamPos = cameraTr.position;
            rerol = true;
        }
    }

}
