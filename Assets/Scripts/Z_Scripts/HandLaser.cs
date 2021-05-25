using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLaser : MonoBehaviour
{
    private LineRenderer laser;

    private void Awake()
    {
        if (laser == null)
        {
            laser = GetComponent<LineRenderer>();
        }

        if (laser == null)
        {
            laser = gameObject.AddComponent<LineRenderer>();
            laser.SetPosition(1, Vector3.forward * 100);
            laser.startWidth = 0.01f;
            laser.endWidth = 0.01f;
            laser.startColor = Color.magenta;
            laser.endColor = Color.magenta;
            laser.useWorldSpace = false;
        }
    }
}
