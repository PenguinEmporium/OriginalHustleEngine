using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTextTag", menuName = "UI Elements/TextPreload", order = 1)]
public class UITextPreload : PreloadableScriptable
{
    public string textTag;

    public string uiText;


    public override void Preload(DialogueReader reader)
    {
        string ui = reader.CheckForTag(textTag, reader.UILanguage);

        if(ui == null)
        {

        }
        else
        {
            //Removing this until finding a good way to preserve UI text if nothing is found
            //uiText = ui;
        }
    }

}
