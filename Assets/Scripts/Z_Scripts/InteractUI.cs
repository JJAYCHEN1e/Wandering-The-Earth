using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SwitchType
{
    On,
    Off
}

public enum ButtonType
{
    Trigger,
    Switch
}

public enum UIButtonType
{
    Color,
    Image
}

public class InteractUI : InteractBase
{
    protected override void Start()
    {
        base.Start();

        InitCollider();
    }

    private void InitCollider()
    {
        mCollider = GetComponent<Collider>();
        if (mCollider == null) mCollider = gameObject.AddComponent<BoxCollider>();
    }
}
