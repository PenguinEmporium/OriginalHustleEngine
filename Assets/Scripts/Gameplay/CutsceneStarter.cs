using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStarter : MonoBehaviour
{
    public GameManager gameManager;

    public Cutscene targetCutscene;

    public PlayableDirector director;

    void Start()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (director == null)
            director = GetComponent<PlayableDirector>();
    }

    public void PlayCutscene()
    {
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        if (director == null)
        {
            director = GetComponent<PlayableDirector>();
        }

        gameManager.EnableDisablePlayerControl(false, true);

        gameManager.StartCutscene(director, targetCutscene);
    }
}
