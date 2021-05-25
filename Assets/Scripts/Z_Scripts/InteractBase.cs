using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBase : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<Valve.VR.SteamVR_Input_Sources,RaycastHit> hitInfo = new Dictionary<Valve.VR.SteamVR_Input_Sources, RaycastHit>();

    public UnityEvent triggerDown = null;
    [HideInInspector]
    public UnityEvent trigger = null;
    [HideInInspector]
    public UnityEvent triggerUp = null;
    [HideInInspector]
    public UnityEvent touchDown = null;
    [HideInInspector]
    public UnityEvent touch = null;
    [HideInInspector]
    public UnityEvent touchUp = null;
    [HideInInspector]
    public UnityEvent snapTurnLeft = null;
    [HideInInspector]
    public UnityEvent snapTurnRight = null;

    protected Collider mCollider = null;

    protected virtual void Awake() { }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void OnDestroy()
    {
        triggerDown.RemoveAllListeners();
        trigger.RemoveAllListeners();
        triggerUp.RemoveAllListeners();
        touchDown.RemoveAllListeners();
        touch.RemoveAllListeners();
        touchUp.RemoveAllListeners();
    }

    public virtual void OnLaserEnter() { }

    public virtual void OnLaserExit() { }

    public virtual void OnTriggerDown() { if (triggerDown != null) triggerDown.Invoke(); }
    public virtual void OnTrigger() { if (trigger != null) trigger.Invoke(); }
    public virtual void OnTriggerUp() { if (triggerUp != null) triggerUp.Invoke(); }
    public virtual void OnTouchDown() { if (touchDown != null) touchDown.Invoke(); }
    public virtual void OnTouch() { if (touch != null) touch.Invoke(); }
    public virtual void OnTouchUp() { if (touchUp != null) touchUp.Invoke(); }
    public virtual void OnSnapTurnLeft() { if (touch != null) snapTurnLeft.Invoke(); }
    public virtual void OnSnapTurnRight() { if (touchUp != null) snapTurnRight.Invoke(); }
}
