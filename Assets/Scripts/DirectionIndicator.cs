using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionIndicator : MonoBehaviour
{
    public GameObject earth;
    public GameObject origin;
    public GameObject terminal;
    public Slider directionSlider;

    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        transform.position = earth.transform.position;
        direction = (terminal.transform.position - origin.transform.position).normalized;
    }

    public Vector3 getDirection ()
    {
        return direction;
    }

    public void OnDirectionSliderValueChanged()
    {
        transform.rotation = Quaternion.Euler(0, directionSlider.value - 180, 0);
    }
}
