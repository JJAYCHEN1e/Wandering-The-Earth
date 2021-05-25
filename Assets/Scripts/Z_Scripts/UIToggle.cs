using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : InteractUI
{
    private Toggle mToggle = null;

    protected override void Start()
    {
        base.Start();

        mToggle = GetComponent<Toggle>();
        if (mToggle == null) mToggle = gameObject.AddComponent<Toggle>();

        trigger.AddListener(SwitchToggle);
    }

    private void SwitchToggle()
    {
        if (mToggle.isOn)
        {
            mToggle.isOn = false;
        }
        else
        {
            mToggle.isOn = true;
        }
    }
}
