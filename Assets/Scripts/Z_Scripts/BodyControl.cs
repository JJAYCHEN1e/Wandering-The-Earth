using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : InteractBase
{
    public enum SymbolType
    {
        Negative = -1,
        Positive = 1
    }

    [Header("[ 观察控制类 ]")]

    [Header("观察物体")]
    public GameObject mObserveObj = null;

    [Header("< 三维旋转 >")]
    [Header("旋转速度")]
    public float mRotateSpeed = 60f;
    [Header("旋转坐标系")]
    public Space mRotateSpace = Space.World;
    [Header("是否允许绕X轴旋转")]
    public bool mIsRotateXActive = true;
    [Header("X轴旋转方向")]
    public SymbolType mRotateXDirection = SymbolType.Negative;
    [Header("是否允许绕Y轴旋转")]
    public bool mIsRotateYActive = true;
    [Header("Y轴旋转方向")]
    public SymbolType mRotateYDirection = SymbolType.Positive;
    private Quaternion mRotateInitValue = Quaternion.identity;

    [Header("< 放大缩小 >")]
    [Header("是否允许放大缩小")]
    public bool mIsScaleActive = true;
    [Header("缩放最小值")]
    [Range(0, 10)]
    public float mScaleMinValue = 0;
    [Header("缩放最大值")]
    [Range(0, 10)]
    public float mScaleMaxValue = 5;
    private float mScaleValue = 1;
    private Vector3 mScaleInitValue = Vector3.one;

    [Header("< 改变深度 >")]
    [Header("是否允许改变深度")]
    public bool mIsDepthActive = true;
    [Header("深度最小值")]
    [Range(-10, 0)]
    public float mDepthMinValue = -2;
    [Header("深度最大值")]
    [Range(0, 10)]
    public float mDepthMaxValue = 2;
    private float mDepthValue = 0;
    private Vector3 mDepthInitValue = Vector3.one;

    private ObjectDrag[] mDragArray;

    protected override void Start()
    {
        if (mObserveObj == null) mObserveObj = this.gameObject;
        mDragArray = mObserveObj.GetComponentsInChildren<ObjectDrag>();
        mRotateInitValue = mObserveObj.transform.localRotation;
        mScaleInitValue = mObserveObj.transform.localScale;
        mDepthInitValue = mObserveObj.transform.localPosition;
    }

    protected override void Update()
    {
        RefreshRotate();
        RefreshScale();
        RefreshDepth();
    }

    private void RefreshRotate()
    {
        //if (Hand.Self.mDragObj == null)
        //{
        //    RefreshRotate(mObserveObj);
        //}
        //else
        //{
        //    RefreshRotate(ControlManager.Self.mDragObj);
        //}
        RefreshRotate(mObserveObj);
    }

    private void RefreshRotate(GameObject obj)
    {
        if (mIsRotateXActive)
        {
            if (SteamVRControllerBase.Instance.leftHand.touchPadAxis.x > 0.7)
            {
                obj.transform.Rotate(Vector3.up * (int)mRotateXDirection * mRotateSpeed * Time.deltaTime, mRotateSpace);
            }

            if (SteamVRControllerBase.Instance.leftHand.touchPadAxis.x < -0.7)
            {
                obj.transform.Rotate(Vector3.down * (int)mRotateXDirection * mRotateSpeed * Time.deltaTime, mRotateSpace);
            }
        }

        if (mIsRotateYActive)
        {
            if (SteamVRControllerBase.Instance.leftHand.touchPadAxis.y > 0.7)
            {
                obj.transform.Rotate(Vector3.right * (int)mRotateYDirection * mRotateSpeed * Time.deltaTime, mRotateSpace);
            }

            if (SteamVRControllerBase.Instance.leftHand.touchPadAxis.y < -0.7)
            {
                obj.transform.Rotate(Vector3.left * (int)mRotateYDirection * mRotateSpeed * Time.deltaTime, mRotateSpace);
            }
        }
    }

    private void RefreshScale()
    {
        if (mIsScaleActive)
        {
            if (SteamVRControllerBase.Instance.rightHand.touchPadAxis.x > 0.7)
            {
                mScaleValue -= Time.deltaTime;
                if (mScaleValue < mScaleMinValue) mScaleValue = mScaleMinValue;
                mObserveObj.transform.localScale = mScaleInitValue * mScaleValue;
            }

            if (SteamVRControllerBase.Instance.rightHand.touchPadAxis.x < -0.7)
            {
                mScaleValue += Time.deltaTime;
                if (mScaleValue > mScaleMaxValue) mScaleValue = mScaleMaxValue;
                mObserveObj.transform.localScale = mScaleInitValue * mScaleValue;
            }
        }
    }

    private void RefreshDepth()
    {
        if (mIsDepthActive)
        {
            if (SteamVRControllerBase.Instance.rightHand.touchPadAxis.y > 0.7)
            {
                mDepthValue += Time.deltaTime;
                if (mDepthValue > mDepthMaxValue) mDepthValue = mDepthMaxValue;
                mObserveObj.transform.position = mDepthInitValue + Vector3.forward * mDepthValue;
            }

            if (SteamVRControllerBase.Instance.rightHand.touchPadAxis.y < -0.7)
            {
                mDepthValue -= Time.deltaTime;
                if (mDepthValue < mDepthMinValue) mDepthValue = mDepthMinValue;
                mObserveObj.transform.position = mDepthInitValue + Vector3.forward * mDepthValue;
            }
        }
    }

    public void ResetData()
    {
        ResetTransform();

        ResetDrag();
    }

    public void ResetTransform()
    {
        mScaleValue = 1;
        mDepthValue = 0;

        mObserveObj.transform.localRotation = mRotateInitValue;
        mObserveObj.transform.localScale = mScaleInitValue;
        mObserveObj.transform.localPosition = mDepthInitValue;
    }

    public void ResetDrag()
    {
        for (int i = 0; i < mDragArray.Length; i++)
        {
            mDragArray[i].ResetData();
            mDragArray[i].gameObject.SetActive(true);
        }
    }

    public void SetRotation(Quaternion qtn)
    {
        mObserveObj.transform.localRotation = qtn;
    }
}
