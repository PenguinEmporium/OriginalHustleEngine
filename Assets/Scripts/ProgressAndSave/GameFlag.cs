using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFlag", menuName = "SaveData/GameFlag", order = 0)]
public class GameFlag : DataPoint
{
    public bool hasBeenTriggered;

    public override void Serialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Deserialize()
    {
        throw new System.NotImplementedException();
    }

    public override Type DataType()
    {
        return this.GetType();
    }
}
