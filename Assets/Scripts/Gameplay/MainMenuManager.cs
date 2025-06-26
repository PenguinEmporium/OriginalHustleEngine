using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] string startLevel = "Start";
    [SerializeField] int entryPoint = -1;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void PlayGame()
    {
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        gameManager.HidePlayer(true);
        gameManager.LoadLevel(startLevel, entryPoint);

    }
}
