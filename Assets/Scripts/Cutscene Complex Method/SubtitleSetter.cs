using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleSetter : MonoBehaviour
{
    public TextMeshProUGUI shadowText;
    public TextMeshProUGUI colorText;

    public void SetText(string text, Color color)
    {
        shadowText.text = text;
        colorText.color = color;
        colorText.text = text;
    }

    public void SetText(string text)
    {
        shadowText.text = text;
        colorText.text = text;
    }
}
