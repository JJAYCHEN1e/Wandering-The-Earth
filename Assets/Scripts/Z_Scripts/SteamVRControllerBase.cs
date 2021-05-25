using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVRControllerBase : MonoBehaviour
{
    private static SteamVRControllerBase instance;

    public static SteamVRControllerBase Instance
    {
        get
        {
            return instance;
        }
    }

    public Hand leftHand = null;

    public Hand rightHand = null;

    public Camera mainCamera = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        leftHand = transform.Find("Controller(left)").GetComponent<Hand>();
        rightHand = transform.Find("Controller(right)").GetComponent<Hand>();
        mainCamera = transform.Find("Camera").GetComponent<Camera>();
    }
}
