using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDrag : InteractObject
{
    [Header("拖拽开始回调")]
    public UnityEvent mOnDragStart = null;
    [Header("拖拽结束回调")]
    public UnityEvent mOnDragEnd = null;

    protected Vector3 mPointInitValue = Vector3.zero;
    protected Quaternion mRoatateInitValue = Quaternion.identity;

    protected override void Start()
    {
        base.Start();

        mPointInitValue = transform.localPosition;
        mRoatateInitValue = transform.localRotation;
    }

    protected virtual void OnDragStart()
    {
        if (mOnDragStart != null) mOnDragStart.Invoke();
    }

    protected virtual void OnDragEnd()
    {
        if (mOnDragEnd != null) mOnDragEnd.Invoke();
    }

    public void ResetData()
    {
        transform.localPosition = mPointInitValue;
        transform.localRotation = mRoatateInitValue;
    }
}
