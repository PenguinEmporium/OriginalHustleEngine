using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class CutsceneManager : MonoBehaviour
{
    GameManager gameManager;

    PlayableDirector currentDirector;

    Cutscene cutsceneDialogue;
    int currentSegment;

    [SerializeField] private List<CutsceneActor> actors;

    [SerializeField] bool canSkip = false;

    public GameObject subPref;
    public GameObject currentSub;

    public GameObject constantCanvas;

    [SerializeField] private AudioSource dialogueAudio;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canSkip)
        {
            SkipCurrent();
        }
    }


    public void GetAllActors()
    {
        actors.Clear();
        actors.AddRange(FindObjectsOfType<CutsceneActor>());
    }

    public void StartCutscene(PlayableDirector director, Cutscene cutsceneDia)//Cutscene cutscene)
    {
        GetAllActors();

        currentDirector = director;
        currentDirector.stopped += CurrentDone;

        cutsceneDialogue = cutsceneDia;
        currentSegment = 0;

        PlaySegment();
    }

    public void PlaySegment()
    {
        if (currentSub != null)
            Destroy(currentSub);

        currentDirector.playableAsset = cutsceneDialogue.segments[currentSegment].cutsceneTimeline;
        currentDirector.RebuildGraph();

        BindPlayerToGraph();

        currentDirector.Play();

        GameObject actor = null;
        ActorTag actTag = cutsceneDialogue.segments[currentSegment].talkingTarget;


        if (actTag != null)
        {
            foreach (CutsceneActor act in actors)
            {
                if (actTag == act.tag)
                {
                    actor = act.gameObject;
                    break;
                }
            }
        }


        if(cutsceneDialogue.segments[currentSegment].dialogueTag != null && cutsceneDialogue.segments[currentSegment].dialogueTag != "")
        {
            if (actor != null)
            {
                var text = Instantiate(subPref, Camera.main.WorldToScreenPoint(actor.transform.position), Quaternion.identity, constantCanvas.transform).GetComponent<SubtitleSetter>();

                text.SetText(cutsceneDialogue.segments[currentSegment].dialogueText, actTag.subtitleColor);
                currentSub = text.gameObject;
            }
            else
            {
                var text = Instantiate(subPref, constantCanvas.transform).GetComponent<SubtitleSetter>();

                text.SetText(cutsceneDialogue.segments[currentSegment].dialogueText);
                currentSub = text.gameObject;
            }
        }

        if(cutsceneDialogue.segments[currentSegment].dialogueTrack != null)
        {
            dialogueAudio.PlayOneShot(cutsceneDialogue.segments[currentSegment].dialogueTrack);
        }


    }

    //We might want to bind the audio to the graph too so the audio doesn't get clipped
    void BindPlayerToGraph()
    {
        var outputNode = currentDirector.playableGraph.GetOutput(0);


        //V this works to assign the player at runtime to a timeline!!!!

        if (outputNode.IsPlayableOutputOfType<AnimationPlayableOutput>())
        {
            var animationNode = (AnimationPlayableOutput)outputNode;

            animationNode.SetTarget(gameManager.playerReference.GetComponent<Animator>());

            Debug.Log(animationNode.GetTarget().gameObject.name);
        }
    }

    public void CurrentDone(PlayableDirector dir)
    {
        Debug.Log("Segment done");

        currentSegment += 1;
        if (cutsceneDialogue.segments.Count <= currentSegment)
        {
            EndCutscene();
        }
        else
        {
            PlaySegment();
        }
    }

    public void SkipCurrent()
    {
        currentSegment += 1;
        if(cutsceneDialogue.segments.Count <= currentSegment)
        {
            EndCutscene();
        }
        else
        {
            PlaySegment();
        }
    }

    void EndCutscene()
    {
        if (currentSub != null)
            Destroy(currentSub);

        currentDirector.stopped -= CurrentDone;

        Debug.Log("Cutscene Done!");

        gameManager.CutsceneEnded();
    }

}
