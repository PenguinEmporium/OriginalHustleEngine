using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageLoader : MonoBehaviour
{
    //public LanguageItem[] allLangItems;

    [Tooltip("Assign master language file here")]
    public TextAsset langCSVFile;

    //Access with gird[row,col]
    string[,] grid;
    int numRows;

    public enum SetLanguage
    {
        English,
        Spanish,
        Japanese
    }

    public SetLanguage chosenLang;

    public void Start()
    {
        //GameStart();
    }

    public bool GameStart()
    {
        /*allLangItems = FindObjectsOfType<LanguageItem>();

        grid = getCSVGrid(langCSVFile.text);


        foreach (LanguageItem li in allLangItems)
        {
            int NumOfSubItems = li.languageList.Length;
            for (int i = 0; i < NumOfSubItems; i++)
            {
                li.languageList[i].TextToChange = FindTag(li.languageList[i].tag, chosenLang);
                li.onLangLoaded.Invoke();
            }
        }
        */
        return true;
    }

    string FindTag(string tag, LanguageOption lang)
    {
        int langCol = 1; //English by default

        langCol = lang.colInFile;

        for (int i = 0; i < numRows; i++)
        {
            if (grid[0, i] == tag)
            {
                return grid[langCol, i];
            }
        }

        return "ERROR: No tag found in master for tag: " + tag;
    }


    string[,] getCSVGrid(string csvText)
    {
        //split the data on split line character
        string[] lines = csvText.Split("\n"[0]);

        // find the max number of columns
        numRows = lines.Length;
        int totalColumns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            totalColumns = Mathf.Max(totalColumns, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[totalColumns + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(',');
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];
            }
        }

        return outputGrid;
    }

    //I think this is fliped. DOUBLE CHECK if it should be col, row
    void getValueAtIndex(int row, int col)
    {
        Debug.Log(grid[row, col]);
    }
}