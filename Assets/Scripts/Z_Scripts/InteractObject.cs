using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : InteractBase
{
    [Header("[ 物体交互类 ]")]
    [Header("是否启用边缘光")]
    public bool mIsRimActive = true;
    [Header("边缘光颜色")]
    public Color mRimColor = Color.cyan;

    protected override void Start()
    {
        base.Start();

        InitCollider();
    }

    private void InitCollider()
    {
        mCollider = GetComponent<Collider>();
        if (mCollider == null) mCollider = gameObject.AddComponent<MeshCollider>();
    }
}
