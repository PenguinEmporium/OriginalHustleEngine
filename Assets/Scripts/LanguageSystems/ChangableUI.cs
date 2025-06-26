using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangableUI : MonoBehaviour
{
    public UITextPreload textLoad;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Awake()
    {
        if(textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();
        //textMesh.text = textLoad.uiText;
    }
}
