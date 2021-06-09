using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : HandBase
{
    /// <summary>
    /// 激光射线
    /// </summary>
    private HandLaser laser;
    /// <summary>
    /// 物理射线
    /// </summary>
    private Ray mRay;
    /// <summary>
    /// 射线是否射中物体
    /// </summary>
    private bool mIsRayHit = false;
    /// <summary>
    /// 射线射中的信息集合
    /// </summary>
    private RaycastHit mRaycastHit;
    /// <summary>
    /// 射线射中的物体
    /// </summary>
    [HideInInspector]
    public GameObject mRayHitObj = null;
    /// <summary>
    /// 射线捕捉到物体
    /// </summary>
    [HideInInspector]
    public Vector3 mRayHitPoint = Vector3.zero;
    /// <summary>
    /// 射线捕捉到上一个物体
    /// </summary>
    [HideInInspector]
    public GameObject mRayHitLastObj = null;
    /// <summary>
    /// 射线交互脚本
    /// </summary>
    [HideInInspector]
    public InteractBase mRayHitInteract = null;
    /// <summary>
    /// 射线触发最远距离
    /// </summary>
    public float mMaxRayDistance = 500f;
    /// <summary>
    /// 拖拽物体
    /// </summary>
    [HideInInspector]
    public GameObject mDragObj = null;
    /// <summary>
    /// 拖拽物体交互组件
    /// </summary>
    [HideInInspector]
    public ObjectDrag mDragInteract = null;
    /// <summary>
    /// 拖拽坐标
    /// </summary>
    private Vector3 mDragPoint = Vector3.zero;
    /// <summary>
    /// 拖拽初始坐标
    /// </summary>
    private Vector3 mDragStartPoint = Vector3.zero;
    /// <summary>
    /// 拖拽坐标偏移值
    /// </summary>
    private Vector3 mDragDifferPoint = Vector3.zero;
    /// <summary>
    /// 拖拽物体初始坐标
    /// </summary>
    private Vector3 mDragObjStartPoint = Vector3.zero;
    /// <summary>
    /// 拖拽物体拖拽距离
    /// <para>3D模式下开始拖拽物体时物体与手柄的距离</para>
    /// </summary>
    private float mDragObjDistance = 0;

    /// <summary>
    /// 射线是否可以进入物体
    /// </summary>
    private bool mIsEnter = true;
    /// <summary>
    /// 射线是否可以退出物体
    /// </summary>
    private bool mIsExit = false;
    /// <summary>
    /// 射线是否可以退出物体
    /// </summary>
    private bool mIsDrag = false;
    [HideInInspector]
    public bool mIsHide = false;

    private void Awake()
    {
        laser = GetComponent<HandLaser>();
    }

    private void Update()
    {
        RefreshRay();
        RefreshControl();
    }

    private void RefreshRay()
    {
        mRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        mIsRayHit = Physics.Raycast(mRay, out mRaycastHit, mMaxRayDistance);
    }

    private void RefreshControl()
    {
        if (mIsRayHit)
        {
            mRayHitObj = mRaycastHit.transform.gameObject;
            mRayHitPoint = mRaycastHit.point;

            mRayHitInteract = mRayHitObj.GetComponent<InteractBase>();

            /// 射线上一帧的捕捉物体和当前帧不一样时
            /// 射线可以退出上一帧的物体               
            if (mRayHitLastObj != null && mRayHitObj != mRayHitLastObj)
            {
                mIsEnter = true;
                mIsExit = false;
                mRayHitLastObj.GetComponent<InteractBase>().OnLaserExit();
            }

            if (mRayHitInteract == null) return;
            mRayHitInteract.hitInfo[inputSource] = mRaycastHit;

            /// 射线可以进入当前帧的物体时
            if (mIsEnter)
            {
                mIsEnter = false;
                mIsExit = true;
                mRayHitInteract.OnLaserEnter();
            }

            if (trigger.GetStateDown(inputSource))
            {
                mRayHitInteract.OnTriggerDown();

                StartDrag(mRayHitInteract);
            }
            if (touch.GetStateDown(inputSource))
            {
                mRayHitInteract.OnTouchDown();
            }

            if (trigger.GetState(inputSource))
            {
                mRayHitInteract.OnTrigger();
            }
            if (touch.GetState(inputSource))
            {
                mRayHitInteract.OnTouch();
            }

            if (snapTurnLeft.GetState(inputSource))
            {
                mRayHitInteract.OnSnapTurnLeft();
            }

            if (snapTurnRight.GetState(inputSource))
            {
                mRayHitInteract.OnSnapTurnRight();
            }

            if (snapTurnUp.GetState(inputSource))
            {
                mRayHitInteract.OnSnapTurnUp();
            }

            if (snapTurnDown.GetState(inputSource))
            {
                mRayHitInteract.OnSnapTurnDown();
            }

            if (trigger.GetStateUp(inputSource))
            {
                mRayHitInteract.OnTriggerUp();
            }
            if (touch.GetStateUp(inputSource))
            {
                mRayHitInteract.OnTouchUp();
            }

            mRayHitLastObj = mRayHitObj;
        }
        else
        {
            if (mRayHitObj != null && mIsExit)
            {
                mIsEnter = true;
                mIsExit = false;
                if (mRayHitInteract != null) mRayHitInteract.OnLaserExit();
                mRayHitObj = null;
            }

            mRayHitPoint = Vector3.zero;
        }

        if (trigger.GetStateUp(inputSource))
        {
            EndDrag();
        }

        RefreshDrag();
    }

    private void StartDrag(InteractBase mRayHitInteract)
    {
        if (null != mRayHitInteract.gameObject.GetComponent<ObjectDrag>())
        {
            if (mIsHide)
            {
                ToolCenter.SetActive(mRayHitObj, false);
            }
            else
            {
                mIsDrag = true;
                mDragObj = mRayHitObj;
                mDragInteract = mRayHitInteract.gameObject.GetComponent<ObjectDrag>();

                mDragStartPoint = mRaycastHit.point;
                mDragObjStartPoint = mDragObj.transform.position;
                mDragObjDistance = mRaycastHit.distance;

                mDragInteract.mOnDragStart.Invoke();
            }
        }
    }

    private void RefreshDrag()
    {
        if (mIsDrag)
        {
            Vector3 direction = this.transform.TransformDirection(Vector3.forward);
            mDragPoint = this.transform.position + Vector3.Normalize(direction) * mDragObjDistance;
            mDragDifferPoint = mDragPoint - mDragStartPoint;
            mDragObj.transform.position = mDragObjStartPoint + mDragDifferPoint;
        }
    }

    private void EndDrag()
    {
        mIsDrag = false;
        mDragObj = null;
        mDragInteract = null;
    }

    private void OnSceneLoadFinish()
    {
        mIsEnter = true;
        mIsExit = false;
        mIsDrag = false;
    }
}
