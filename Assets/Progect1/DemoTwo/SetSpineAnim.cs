using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SetSpineAnim : MonoBehaviour
{
    public SkeletonAnimation skeleton;
    public float speedAn = 1f;

    void Start()
    {
        if (!skeleton)
            skeleton = GetComponent<SkeletonAnimation>();
    }

    public void SetSpeed (float spd)
    {
        skeleton.timeScale = spd;
    }

    public void PlayAnimation(string nm)
    {
        if(skeleton.AnimationName != nm)
            skeleton.AnimationName = nm;
    }

}
