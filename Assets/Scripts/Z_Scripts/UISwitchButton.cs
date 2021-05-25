using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISwitchButton : InteractUI
{
    [Header("[ UI开关按钮类 ]")]

    [Header("UI按钮类型")]
    public UIButtonType mUIButtonType = UIButtonType.Color;

    [Header("按钮图片组件")]
    public Image mImage = null;

    [Header("< 按钮图片颜色 >")]
    [Header("开关关闭时正常颜色")]
    public Color mImageOffNormalColor = Color.white;
    [Header("开关关闭时高亮颜色")]
    public Color mImageOffLightColor = Color.cyan;
    [Header("开关打开时正常颜色")]
    public Color mImageOnNormalColor = Color.yellow;
    [Header("开关打开时高亮颜色")]
    public Color mImageOnLightColor = Color.red;

    [Header("< 按钮图片 >")]
    [Header("开关关闭时正常图片")]
    public Sprite mImageOffNormalSprite = null;
    [Header("开关关闭时高亮图片")]
    public Sprite mImageOffLightSprite = null;
    [Header("开关打开时正常图片")]
    public Sprite mImageOnNormalSprite = null;
    [Header("开关打开时高亮图片")]
    public Sprite mImageOnLightSprite = null;

    //[Header("< 按钮音效 >")]
    //[Header("按钮点击音效")]
    //public AudioType mClickEffect = AudioType.None;

    [Header("< 按钮回调 >")]
    [Header("按钮点击执行延迟时间")]
    public float mOnClickTime = 0f;
    [Header("按钮进入回调")]
    public UnityEvent mOnEnter = null;
    [Header("按钮退出回调")]
    public UnityEvent mOnExit = null;
    [Header("按钮点击回调")]
    public UnityEvent mOnClick = null;

    private bool mIsButtonActive = true;

    private SwitchType mSwitchType = SwitchType.Off;

    public override void OnLaserEnter()
    {
        base.OnLaserEnter();

        if (!mIsButtonActive) return;

        if (mImage != null)
        {
            if (mSwitchType == SwitchType.Off)
            {
                mImage.color = mImageOffLightColor;
            }
            else if (mSwitchType == SwitchType.On)
            {
                mImage.color = mImageOnLightColor;
            }
        }

        if (mOnEnter != null) mOnEnter.Invoke();
    }

    public override void OnLaserExit()
    {
        base.OnLaserExit();

        if (!mIsButtonActive) return;

        if (mImage != null)
        {
            if (mSwitchType == SwitchType.Off)
            {
                mImage.color = mImageOffNormalColor;
            }
            else if (mSwitchType == SwitchType.On)
            {
                mImage.color = mImageOnNormalColor;
            }
        }

        if (mOnExit != null) mOnExit.Invoke();
    }

    public override void OnTriggerDown()
    {
        base.OnTriggerDown();

        if (!mIsButtonActive) return;

        if (mSwitchType == SwitchType.Off)
        {
            mSwitchType = SwitchType.On;
        }
        else if (mSwitchType == SwitchType.On)
        {
            mSwitchType = SwitchType.Off;
        }

        if (mImage != null)
        {
            if (mSwitchType == SwitchType.Off)
            {
                mImage.color = mImageOffNormalColor;
            }
            else if (mSwitchType == SwitchType.On)
            {
                mImage.color = mImageOnNormalColor;
            }
        }

        //AudioManager.PlayEffect(mClickEffect);
        if (mOnClick != null) mOnClick.Invoke();
    }

    public void SetClick(SwitchType type)
    {
        if (type != mSwitchType)
        {
            OnTriggerDown();
        }
    }

    public SwitchType GetSwitchType()
    {
        return mSwitchType;
    }

    public bool GetIsSwitchOn()
    {
        if (mSwitchType == SwitchType.Off) return false;
        else if (mSwitchType == SwitchType.On) return true;

        return false;
    }

    IEnumerator DoOnClick()
    {
        yield return new WaitForSeconds(mOnClickTime);

        if (mOnClick != null) mOnClick.Invoke();
    }
}
