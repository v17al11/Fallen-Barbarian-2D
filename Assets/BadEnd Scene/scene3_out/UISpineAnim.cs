using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class UISpineAnim : MonoBehaviour
{
    private SkeletonGraphic BadEndAnimation;

    private void Awake()
    {
        BadEndAnimation = GetComponent<SkeletonGraphic>();
    }

    void Start()
    {
        BadEndAnimation.AnimationState.SetAnimation(0, "finish_all", true);
    }
}
