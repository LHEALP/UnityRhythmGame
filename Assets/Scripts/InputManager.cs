using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject[] keyEffects = new GameObject[4];

    void Start()
    {
        foreach (var effect in keyEffects)
        {
            effect.gameObject.SetActive(false);
        }
    }

    public void OnD(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keyEffects[0].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            keyEffects[0].gameObject.SetActive(false);
        }
    }
    public void OnF(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keyEffects[1].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            keyEffects[1].gameObject.SetActive(false);
        }
    }
    public void OnJ(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keyEffects[2].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            keyEffects[2].gameObject.SetActive(false);
        }
    }
    public void OnK(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            keyEffects[3].gameObject.SetActive(true);
        }
        else if (context.canceled)
        {
            keyEffects[3].gameObject.SetActive(false);
        }
    }
}
