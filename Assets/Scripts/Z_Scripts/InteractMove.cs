using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMove : InteractObject
{
    public GameObject laserPoint = null;

    private bool allowMove = false;

    protected override void Update()
    {
        base.Update();

        if (SteamVRControllerBase.Instance.rightHand.touchPadAxis.y > 0.7)
        {
            SetPointPos();
            allowMove = true;
        }

        if (SteamVRControllerBase.Instance.rightHand.touch.GetStateDown(Valve.VR.SteamVR_Input_Sources.RightHand))
        {
            HighlightOnSelfOnTouch();
        }
        //if (SteamVRControllerBase.Instance.leftHand.touch.GetState(Valve.VR.SteamVR_Input_Sources.LeftHand))
        //{
        //    SetPointPos();
        //}
        if (SteamVRControllerBase.Instance.rightHand.touch.GetStateUp(Valve.VR.SteamVR_Input_Sources.RightHand))
        {
            if(allowMove)SetSelfPos();
            HighlightOffSelfOnTouch();
            ResetPointPos();
            allowMove = false;
        }
    }

    private void HighlightOnSelfOnTouch()
    {

    }

    private void HighlightOffSelfOnTouch()
    {

    }

    private void SetPointPos()
    {
        if (laserPoint != null) laserPoint.transform.position = hitInfo[Valve.VR.SteamVR_Input_Sources.RightHand].point;
    }

    private void SetSelfPos()
    {
        SteamVRControllerBase.Instance.transform.position = hitInfo[Valve.VR.SteamVR_Input_Sources.RightHand].point /*+ new Vector3(Camera.main.transform.localPosition.x, 0, Camera.main.transform.localPosition.z)*/;
    }

    private void ResetPointPos()
    {
        if (laserPoint != null) laserPoint.transform.position = Vector3.one * 1000;
    }
}
