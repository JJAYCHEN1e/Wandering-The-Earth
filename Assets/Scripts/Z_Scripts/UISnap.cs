using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISnap : InteractUI
{
    [Header("SnapUp")]
    public UnityEvent mSnapUp = null;
    [Header("SnapDown")]
    public UnityEvent mSnapDown = null;
    [Header("SnapLeft")]
    public UnityEvent mSnapLeft = null;
    [Header("SnapRight")]
    public UnityEvent mSnapRight = null;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnSnapTurnLeft()
    {
        base.OnSnapTurnLeft();
        if (mSnapLeft != null) mSnapLeft.Invoke();
    }

    public override void OnSnapTurnRight()
    {
        base.OnSnapTurnRight();
        if (mSnapRight != null) mSnapRight.Invoke();
    }

    public override void OnSnapTurnUp()
    {
        base.OnSnapTurnUp();
        if (mSnapUp != null) mSnapUp.Invoke();
    }

    public override void OnSnapTurnDown()
    {
        base.OnSnapTurnDown();
        if (mSnapDown != null) mSnapDown.Invoke();
    }
}


