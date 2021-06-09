using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public GameObject wheel;

    public GameObject trigger;
    public GameObject rotatePoint;

    public EarthConroller earthConroller;

    public void TurnOnTrigger()
    {
        trigger.transform.RotateAround(rotatePoint.transform.position, trigger.transform.right, -40);
    }

    public void TurnOffTrigger()
    {
        trigger.transform.RotateAround(rotatePoint.transform.position, trigger.transform.right, 40);
    }

    public void TurnRightWheel()
    {
        wheel.transform.RotateAround(wheel.transform.position, wheel.transform.forward, -1);
    }

    public void TurnLeftWheel()
    {
        wheel.transform.RotateAround(wheel.transform.position, wheel.transform.forward, 1);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TurnLeftWheel();
            //TurnOnTrigger();
        }
    }
}
