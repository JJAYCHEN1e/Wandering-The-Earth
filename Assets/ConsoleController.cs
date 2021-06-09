using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public GameObject wheel;

    public GameObject trigger;
    public GameObject rotatePoint;

    public EarthConroller earthConroller;
    public GameObject smoke;
    public GameObject forceIndicator;

    private bool turnOn = false;

    public void TurnOnTrigger()
    {
        trigger.transform.RotateAround(rotatePoint.transform.position, trigger.transform.right, -40);
        smoke.SetActive(true);
        turnOn = true;
        if (turnOn) earthConroller.UpdateForce(-forceIndicator.transform.forward);
    }

    public void TurnOffTrigger()
    {
        trigger.transform.RotateAround(rotatePoint.transform.position, trigger.transform.right, 40);
        earthConroller.CancelForce();
        smoke.SetActive(false);
        turnOn = false;
    }

    public void TurnRightWheel()
    {
        wheel.transform.RotateAround(wheel.transform.position, wheel.transform.forward, 1);
        smoke.transform.eulerAngles = new Vector3(0, wheel.transform.eulerAngles.z, 90);
        forceIndicator.transform.eulerAngles = new Vector3(0, wheel.transform.eulerAngles.z, 0);
        if (turnOn) earthConroller.UpdateForce(-forceIndicator.transform.forward);
    }

    public void TurnLeftWheel()
    {
        wheel.transform.RotateAround(wheel.transform.position, wheel.transform.forward, -1);
        smoke.transform.eulerAngles = new Vector3(0, wheel.transform.eulerAngles.z, 90);
        forceIndicator.transform.eulerAngles = new Vector3(0, wheel.transform.eulerAngles.z, 0);
        if (turnOn) earthConroller.UpdateForce(-forceIndicator.transform.forward);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurnOnTrigger();
            TurnLeftWheel();
        }
    }
}
