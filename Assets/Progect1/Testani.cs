using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Testani : MonoBehaviour
{
    public SkeletonAnimation skelAn;
    public string NewAn = "frozen";

    void Update()
    {

    }

    public void Anim1 ()
    {
        skelAn.AnimationName = "idle";
    }
    public void Anim2()
    {
        skelAn.AnimationName = "frozen";
    }

}
