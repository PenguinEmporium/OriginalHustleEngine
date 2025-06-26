using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceJapaneseText : MonoBehaviour
{
    GameManager gameManager;
    DialogueReader reader;

    [SerializeField] AudioClip testClip;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        reader = GetComponent<DialogueReader>();
    }

    void Start()
    {
        //AudioClip test = Resources.Load<AudioClip>("Audio/Japanese/Test/test01_Japanese");
        //testClip = test;


        //gameManager.ChangeLanguage(reader.languages[1]);

        /*var audioClip = Resources.Load<AudioClip>("Audio/" + reader.chosenLang.LanguageName + "/" + reader.GetLastTagSortFolder() + "/test01" + "_" + reader.chosenLang.LanguageName);
        testClip = audioClip;


        string lname = "Japanese";

        Debug.Log(lname);
        Debug.Log(reader.chosenLang.LanguageName);

        Debug.Log(StringComparison(lname, reader.chosenLang.LanguageName.Trim()));*/

    }

    public static bool StringComparison(string s1, string s2)
    {
        if (s1.Length != s2.Length) return false;
        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                Debug.Log("The " + i.ToString() + "th character is different.");
                return false;
            }
        }
        return true;
    }
}
