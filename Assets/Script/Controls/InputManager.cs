using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static public InputManager instance = null;
    private IInput input;

    void Awake()
    {
        instance = this;

        #if   UNITY_EDITOR
        input = new InputAndroid();
        #elif UNITY_STANDALONE_WIN
        input = new InputAndroid();
        #elif UNITY_ANDROID
        input = new InputAndroid();
        #endif
    }

    public GameObject RightClic()
    {
        return input.RightClic();
    }

    public bool RightClicEnded()
    {
        return input.RightClicEnded();
    }

    public GameObject LeftClic()
    {
        return input.LeftClic();
    }

    public bool LeftClicEnded()
    {
        return input.LeftClicEnded();
    }
}