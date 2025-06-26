using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataPoint : ScriptableObject
{
    public string fieldName;

    public abstract void Serialize();

    public abstract void Deserialize();

    public abstract System.Type DataType();
}
