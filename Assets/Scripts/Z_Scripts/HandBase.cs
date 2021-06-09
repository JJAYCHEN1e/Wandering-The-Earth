using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandBase : SteamVR_Behaviour_Pose
{
    public SteamVR_Action_Boolean trigger = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    public SteamVR_Action_Boolean touch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    public SteamVR_Action_Vector2 touchPad = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");

    public SteamVR_Action_Boolean grip = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");

    public SteamVR_Action_Boolean snapTurnLeft = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnLeft");

    public SteamVR_Action_Boolean snapTurnRight = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnRight");

    public SteamVR_Action_Boolean snapTurnUp = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnUp");

    public SteamVR_Action_Boolean snapTurnDown = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SnapTurnDown");

    public Vector2 touchPadAxis = Vector2.zero;

    protected override void Start()
    {
        base.Start();

        trigger[inputSource].onStateDown += OnTriggerDown;
        trigger[inputSource].onStateUp += OnTriggerUp;
        trigger[inputSource].onState += OnTrigger;

        touch[inputSource].onStateDown += OnTouchDown;
        touch[inputSource].onStateUp += OnTouchUp;
        touch[inputSource].onState += OnTouch;
    }

    protected virtual void OnDestroy()
    {
        trigger[inputSource].onStateDown -= OnTriggerDown;
        trigger[inputSource].onStateUp -= OnTriggerUp;
        trigger[inputSource].onState -= OnTrigger;

        touch[inputSource].onStateDown -= OnTouchDown;
        touch[inputSource].onStateUp -= OnTouchUp;
        touch[inputSource].onState -= OnTouch;
    }

    protected virtual void OnTriggerDown(SteamVR_Action_Boolean trigger, SteamVR_Input_Sources hand) { }

    protected virtual void OnTriggerUp(SteamVR_Action_Boolean trigger, SteamVR_Input_Sources hand) { }

    protected virtual void OnTrigger(SteamVR_Action_Boolean trigger, SteamVR_Input_Sources hand) { }

    protected virtual void OnTouchDown(SteamVR_Action_Boolean touch, SteamVR_Input_Sources hand) { this.touchPad.actionSet.Activate(); }

    protected virtual void OnTouchUp(SteamVR_Action_Boolean touch, SteamVR_Input_Sources hand) { this.touchPad.actionSet.Deactivate(); touchPadAxis = Vector2.zero; }

    protected virtual void OnTouch(SteamVR_Action_Boolean touch, SteamVR_Input_Sources hand) { touchPadAxis = touchPad.axis; }
}
