using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerManager
{
    public static FingerManager Instance = new FingerManager();

    public delegate void GestureEventDrag(DragGesture gesture);
    public delegate void GestureEventPinch(PinchGesture gesture);
    public delegate void EventFingerDown(FingerDownEvent e);
    public delegate void EventFingerUp(FingerUpEvent e);
    public delegate void EventLongPress(LongPressGesture gesture);
    public delegate void EventSwipe(SwipeGesture gesture);

    public static GestureEventDrag OnDrag;
    public static GestureEventPinch OnPinch;
    public static EventFingerDown OnFingerDown;
    public static EventFingerUp OnFingerUp;
    public static EventLongPress OnLongPress;
    public static EventSwipe OnSwipe;

    public bool IsEnable = true;

    public void Init()
    {
        Enable();
    }

    public void Enable()
    {
        if (FingerGestures.Instance == null)
        {
            return;
        }

        GameObject root = FingerGestures.Instance.gameObject;

        root.AddComponent<DragRecognizer>().OnGesture += OnFingerDrag;
        root.AddComponent<PinchRecognizer>().OnGesture += OnFingerPinch;
        root.AddComponent<FingerDownDetector>().OnFingerDown += OnEventFingerDown;
        root.AddComponent<FingerUpDetector>().OnFingerUp += OnEventFingerUp;
        root.AddComponent<LongPressRecognizer>().OnGesture += OnEventLongPress;
        root.AddComponent<SwipeRecognizer>().OnGesture += OnEventSwipe;

        IsEnable = true;
    }

    public void Disable()
    {
        if (FingerGestures.Instance == null)
        {
            return;
        }

        IsEnable = false;
    }

    void OnFingerDrag(DragGesture gesture)
    {
        if (IsEnable && OnDrag != null)
        {
            OnDrag(gesture);
        }
    }

    void OnFingerPinch(PinchGesture gesture)
    {
        if (IsEnable && OnPinch != null)
        {
            OnPinch(gesture);
        }
    }

    void OnEventFingerDown(FingerDownEvent e)
    {
        if (IsEnable && OnFingerDown != null)
        {
            OnFingerDown(e);
        }
    }

    void OnEventFingerUp(FingerUpEvent e)
    {
        if (IsEnable && OnFingerUp != null)
        {
            OnFingerUp(e);
        }
    }

    void OnEventLongPress(LongPressGesture gesture)
    {
        if (IsEnable && OnLongPress != null)
        {
            OnLongPress(gesture);
        }
    }

    void OnEventSwipe(SwipeGesture gesture)
    {
        if (IsEnable && OnSwipe != null)
        {
            OnSwipe(gesture);
        }
    }

}
