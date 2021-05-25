using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionControl : MonoBehaviour
{
    public GameObject Earth;
    public Slider DirectionSlider;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        DirectionSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        DirectionSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Earth.transform.position;
    }

    public void OnSliderValueChanged(float value)
    {
        float angle = value * 360;
        this.transform.localEulerAngles = new Vector3(0, angle, 0);
    }
}
