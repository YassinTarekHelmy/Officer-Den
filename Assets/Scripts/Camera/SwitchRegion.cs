using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchRegion : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float _inactiveAlpha;
    [SerializeField][Range(0,1)] private float _activeAlpha;
    public event Action OnMouseEnterEvent = delegate {};

    private Image _regionImage;
    private EventTrigger _eventTrigger;
    private EventTrigger.Entry _mouseEnterEvent;
    private EventTrigger.Entry _mouseExitEvent;
    private void Awake() {
        _regionImage = GetComponent<Image>();
        _eventTrigger = GetComponent<EventTrigger>();

        _mouseEnterEvent = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerEnter,
            callback = new EventTrigger.TriggerEvent()
        };

        _mouseExitEvent = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerExit,
            callback = new EventTrigger.TriggerEvent()
        };

        _eventTrigger.triggers.Add(_mouseEnterEvent);
        _eventTrigger.triggers.Add(_mouseExitEvent);

        _regionImage.color = new Color(_regionImage.color.r, _regionImage.color.g, _regionImage.color.b, _inactiveAlpha);
    }

    void OnEnable()
    {
        _mouseEnterEvent.callback.AddListener(MouseEnterEventTriggered);
        _mouseExitEvent.callback.AddListener(MouseExitEventTriggered);

    }

    void OnDisable()
    {
        _mouseEnterEvent.callback.RemoveListener(MouseEnterEventTriggered);
        _mouseExitEvent.callback.RemoveListener(MouseExitEventTriggered);
    }



    private void MouseEnterEventTriggered(BaseEventData data) {
        _regionImage.CrossFadeAlpha(_activeAlpha, 0.1f, false);

        OnMouseEnterEvent?.Invoke();

    }

    private void MouseExitEventTriggered(BaseEventData data) {
        
        _regionImage.CrossFadeAlpha(_inactiveAlpha, 0.1f, false);
    }
}
