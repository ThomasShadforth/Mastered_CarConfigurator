using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //Script used to add long press functionality to a button
    [SerializeField]
    [Tooltip("How long the button is held to trigger a long press")]
    private float holdTime = 1f;

    //Stores events that are trigger when longpress is triggered and released (If necessary)
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onLongPressRelease = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Triggered while the mouse is held down on the object
    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("OnLongPress", holdTime);
    }

    //Triggered when the mouse is released from the object
    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");

        if(onLongPressRelease != null)
        {
            onLongPressRelease.Invoke();
        }
    }

    //If the mouse is still being held down, triggered when it moves off of the object
    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");

        if (onLongPressRelease != null)
        {
            onLongPressRelease.Invoke();
        }
    }

    //Automatically invokes the long press event(s) assigned
    void OnLongPress()
    {
        onLongPress.Invoke();
    }
}
