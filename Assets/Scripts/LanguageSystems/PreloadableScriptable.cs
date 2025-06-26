using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PreloadableScriptable : ScriptableObject
{
    public abstract void Preload(DialogueReader reader);
}
