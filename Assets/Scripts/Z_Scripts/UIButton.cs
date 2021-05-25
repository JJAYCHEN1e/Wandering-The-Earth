using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : InteractUI
{
    [Header("按钮类型")]
    public ButtonType mButtonType = ButtonType.Trigger;

    [Header("UI按钮类型")]
    public UIButtonType mUIButtonType = UIButtonType.Color;

    [Header("按钮图片组件")]
    public Image mImage = null;


    [Header("< 按钮图片颜色 >")]
    [Header("正常时颜色")]
    public Color mImageNormalColor = new Color(225f / 255f, 225f / 255f, 225f / 255f, 1f);
    [Header("高亮时颜色")]
    public Color mImageHighlightColor = new Color(0f, 255f / 255f, 255f / 255f, 1f);
    [Header("按下时颜色")]
    public Color mImagePressedColor = new Color(0f, 125f / 255f, 255f / 255f, 1f);
    [Header("失效时颜色")]
    public Color mImageDisableColor = new Color(125f / 255f, 125f / 255f, 125f / 255f, 1f);

    [Header("< 按钮图片 >")]
    [Header("正常时图片")]
    public Sprite mImageNormalSprite = null;
    [Header("高亮时图片")]
    public Sprite mImageLightSprite = null;
    [Header("开关时图片")]
    public Sprite mImageSwitchSprite = null;

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

    private bool mIsSwitchOn = false;

    protected override void Start()
    {
        base.Start();

        if (mButtonType == ButtonType.Trigger)
        {
            if (mUIButtonType == UIButtonType.Color)
            {
                if (mImage != null) mImage.color = mImageNormalColor;
            }
            else if (mUIButtonType == UIButtonType.Image)
            {
                if (mImage != null) mImage.sprite = mImageNormalSprite;
            }
        }
        else if (mButtonType == ButtonType.Switch)
        {
            if (mIsSwitchOn)
            {
                if (mImage != null) mImage.sprite = mImageSwitchSprite;
            }
            else
            {
                if (mImage != null) mImage.sprite = mImageNormalSprite;
            }
        }
    }

    public override void OnLaserEnter()
    {
        base.OnLaserEnter();

        if (!mIsButtonActive) return;

        if (mButtonType == ButtonType.Trigger)
        {
            if (mUIButtonType == UIButtonType.Color)
            {
                if (mImage != null) mImage.color = mImageHighlightColor;
            }
            else if (mUIButtonType == UIButtonType.Image)
            {
                if (mImage != null) mImage.sprite = mImageLightSprite;
            }
        }
        else if (mButtonType == ButtonType.Switch)
        {
            if (mImage != null) mImage.color = mImageHighlightColor;
        }

        if (mOnEnter != null) mOnEnter.Invoke();
    }

    public override void OnLaserExit()
    {
        base.OnLaserExit();

        if (!mIsButtonActive) return;

        if (mButtonType == ButtonType.Trigger)
        {
            if (mUIButtonType == UIButtonType.Color)
            {
                if (mImage != null) mImage.color = mImageNormalColor;
            }
            else if (mUIButtonType == UIButtonType.Image)
            {
                if (mImage != null) mImage.sprite = mImageNormalSprite;
            }
        }
        else if (mButtonType == ButtonType.Switch)
        {
            if (mImage != null) mImage.color = mImageNormalColor;
        }

        if (mOnExit != null) mOnExit.Invoke();
    }

    public override void OnTriggerDown()
    {
        base.OnTriggerDown();

        if (!mIsButtonActive) return;

        if (mButtonType == ButtonType.Switch)
        {
            if (mIsSwitchOn)
            {
                if (mImage != null) mImage.sprite = mImageNormalSprite;
            }
            else
            {
                if (mImage != null) mImage.sprite = mImageSwitchSprite;
            }
        }

        //AudioManager.PlayEffect(mClickEffect);
        if (mOnClick != null) mOnClick.Invoke();

        if (mButtonType == ButtonType.Switch) mIsSwitchOn = !mIsSwitchOn;
    }

    public void SetSwitch(bool isOn)
    {
        if (mButtonType == ButtonType.Switch)
        {
            if (mIsSwitchOn != isOn)
            {
                OnTriggerDown();
            }
        }
    }

    public bool GetSwitch()
    {
        return mIsSwitchOn;
    }

    IEnumerator DoOnClick()
    {
        yield return new WaitForSeconds(mOnClickTime);

        if (mOnClick != null) mOnClick.Invoke();
    }

    public void SetButtonActive(bool isActive)
    {
        mIsButtonActive = isActive;

        if (mImage != null)
        {
            if (mIsButtonActive)
                mImage.color = mImageNormalColor;
            else
                mImage.color = mImageDisableColor;
        }
    }
}
