using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMyPos : MonoBehaviour
{
    public float timer = 0.1f;
    public Vector3 offect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dbg(timer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Dbg (float tm)
    {
        Debug.Log(transform.position.x + offect.x + " " + transform.position.y + offect.y);
        yield return new WaitForSeconds(tm);
        StartCoroutine(Dbg(timer));
    }

}
