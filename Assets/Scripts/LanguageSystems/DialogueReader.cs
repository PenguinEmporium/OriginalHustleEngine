using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    [SerializeField] public string fileName;

    [Tooltip("Assign master language file here")]
    [SerializeField] private TextAsset csvFile;

    [SerializeField] private int numberOfColumns = 0;

    [SerializeField] private string lastTag;
    [SerializeField] private string lastSortFolder;

    public void Startup()
    {
        if(CheckForFile())
        {
            LoadCSV();
        }

        if(subtitleLanguage == null || subtitleLanguage.LanguageName == "")
        {
            subtitleLanguage = languages[0];
        }
    }

    public string CheckForTag(string dialogueTag, LanguageOption selectedLanguage)
    {
        return FindTag(dialogueTag, subtitleLanguage);
    }

    public bool WriteToTag(string dialogueTag)
    {


        return false;
    }

    public bool CheckForFile()
    {
        csvFile = (TextAsset)Resources.Load(fileName);

        return csvFile != null ? true : false;
    }


    

    //Access with gird[row,col]
    string[,] grid;
    int numRows;

    public LanguageOption subtitleLanguage;
    public LanguageOption audioLanguage;
    public LanguageOption UILanguage;

    public List<LanguageOption> languages;

    public void LoadCSV()
    {
        grid = getCSVGrid(csvFile.text);

        languages.Clear();


        bool reachedEnd = false;
        int currentCol = 2;

        while(!reachedEnd)
        {
            LanguageOption newOption = new LanguageOption();

            newOption.colInFile = currentCol;
            newOption.LanguageName = getValueAtIndex(currentCol, 2).Trim();
            newOption.percentComplete = getValueAtIndex(currentCol, 1);

            languages.Add(newOption);

            currentCol++;

            if(currentCol >= numberOfColumns)
            {
                reachedEnd = true;
            }
        }
    }

    string FindTag(string tag, LanguageOption lang)
    {
        lastTag = tag;

        int langCol = 1; //English by default

        langCol = lang.colInFile;

        for (int i = 0; i < numRows; i++)
        {
            //Don't forget. Folder sort tag is on grid 0,x
            if (grid[1, i] == tag)
            {
                lastSortFolder = grid[0, i];

                return grid[langCol, i];
            }
        }

        return "ERROR: No tag found in master for tag: " + tag;
    }

    //Slow method. Do not use on regular 
    public string GetSortFolder(string tag)
    {
        for (int i = 0; i < numRows; i++)
        {
            //Don't forget. Folder sort tag is on grid 0,x
            if (grid[1, i] == tag)
            {
                return grid[0, i];
            }
        }

        return "ERROR: No sort folder found for tag: " + tag;
    }

    //Faster method when loading tags and audio at the same time
    public string GetLastTagSortFolder()
    {
        return lastSortFolder;
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
            numberOfColumns = totalColumns;
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
    string getValueAtIndex(int row, int col)
    {
        string toReturn = grid[row, col];
        Debug.Log(toReturn);
        return toReturn;
    }
}
