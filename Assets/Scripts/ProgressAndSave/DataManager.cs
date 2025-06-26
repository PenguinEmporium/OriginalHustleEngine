using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public DataPoint[] dataPoints;

    [SerializeField] int currentSlot = 0;

    public void SaveData(int saveSlot = 0)
    {

    }

    public void LoadData(int saveSlot = 0)
    {

    }

    //Technically you just need to assign the data and it will be managed via scriptable object sharing. No need to really get it from this system unless you want to check
    //If it's being processed in the save file
    public bool CheckForData(DataPoint toCheck)
    {
        foreach(DataPoint dp in dataPoints)
        {
            if(toCheck.fieldName == dp.fieldName)
            {
                return true;
            }
        }

        return false;
    }

    //Technically you just need to assign the data and it will be managed via scriptable object sharing. No need to really get it from this system
    public DataPoint GetData(DataPoint dataToGet)
    {
        foreach(DataPoint dp in dataPoints)
        {
            if (dataToGet.fieldName == dp.fieldName)
            {
                return dp;
            }
        }

        return null;
    }

    public void DeleteData(int saveSlot = 0)
    {

    }

    void CreateSaveFile(int saveSlot)
    {

    }
}
