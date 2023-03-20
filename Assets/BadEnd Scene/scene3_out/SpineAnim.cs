using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAnim : MonoBehaviour
{
    public SkeletonAnimation BadEndAnimation;

    void Start()
    {
        BadEndAnimation.AnimationState.SetAnimation(0, "finish_all", true);
    }
}
