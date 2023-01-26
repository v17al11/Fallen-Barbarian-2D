using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class GirlGO : MonoBehaviour
{
    public UnityArmatureComponent Girl1;
    public UnityArmatureComponent Girl2;

    int nnow = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNewAnimation (string st)
    {
        switch (nnow)
        {
            case 1:
                Girl1.SetNewAnim(st);
                break;
            case 2:
                Girl2.SetNewAnim(st);
                break;
            default:
                break;
        }
    }
    public void SetSpeedAnimation(float speed = 1f)
    {
        Girl1.SetSpeedAnim(speed);
        Girl2.SetSpeedAnim(speed);
    }

    public void SetGirl(int n)
    {
        //if (nnow == n)
          //  return;

        switch (n)
        {
            case 1:
                Girl1.gameObject.SetActive(true);
                Girl2.gameObject.SetActive(false);
                break;
            case 2:
                Girl1.gameObject.SetActive(false);
                Girl2.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        n = nnow;
    }

}
