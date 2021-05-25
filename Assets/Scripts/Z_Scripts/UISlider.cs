using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISlider : InteractUI
{
    [Header("一次按键调整占总体的比例")]
    public float sensitivity = 0.01f;
    [Header("Slider")]
    public Slider slider;

    private bool focused = false;


    protected override void Start()
    {
        base.Start();
    }

    public override void OnSnapTurnLeft()
    {
        base.OnSnapTurnLeft();

        slider.value -= (slider.maxValue - slider.minValue) * sensitivity;
        slider.value = slider.value >= slider.minValue ? slider.value : slider.minValue;
    }

    public override void OnSnapTurnRight()
    {
        base.OnSnapTurnRight();

        slider.value += (slider.maxValue - slider.minValue) * sensitivity;
        slider.value = slider.value <= slider.maxValue ? slider.value : slider.maxValue;
    }
}

