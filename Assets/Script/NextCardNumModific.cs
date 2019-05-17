using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextCardNumModific : MonoBehaviour {
    public Slider slider;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = slider.value.ToString();
    }

    public void ChangeText()
    {
        text.text = slider.value.ToString();
    }
}
