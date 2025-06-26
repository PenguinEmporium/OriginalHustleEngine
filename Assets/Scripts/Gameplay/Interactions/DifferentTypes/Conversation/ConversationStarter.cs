using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ConversationStarter : Interactable
{
    
    public GameManager gameManager;

    public Cutscene targetCutscene;

    public PlayableDirector director;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        director = GetComponent<PlayableDirector>();
    }

    public override void Interact()
    {
        base.Interact();
        gameManager.EnableDisablePlayerControl(false, true);

        gameManager.StartCutscene(director, targetCutscene);
    }
}
