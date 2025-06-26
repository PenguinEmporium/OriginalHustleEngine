using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscenes/Cutscene", order = 1)]
public class Cutscene : PreloadableScriptable
{
    public List<CutsceneSegment> segments;
    public int length;

    [SerializeField] private LanguageOption currentlyLoadedLanguage;

    public override void Preload(DialogueReader reader)
    {
        string subtitleLanguage = reader.subtitleLanguage.LanguageName;
        string audioLanguage = reader.audioLanguage.LanguageName;

        //if (reader.chosenLang != currentlyLoadedLanguage)
        {
            foreach (CutsceneSegment cs in segments)
            {
                if (cs.dialogueTag != null && cs.dialogueTag != "")
                {
                    string dialogue = reader.CheckForTag(cs.dialogueTag, reader.subtitleLanguage);

                    if (dialogue == null)
                    {
                        cs.dialogueText = subtitleLanguage + " : " + cs.dialogueTag + " : dialogue missing";
                    }
                    else
                    {
                        cs.dialogueText = dialogue;
                    }

                    try
                    {
                        var audioClip = Resources.Load<AudioClip>("Audio/" + audioLanguage + "/" + reader.GetLastTagSortFolder() + "/" + cs.dialogueTag + "_" + audioLanguage);

                        if (audioClip != null)
                        {
                            cs.dialogueTrack = audioClip;
                        }
                        else
                        {
                            Debug.Log("Can't find audio clip at Audio/" + audioLanguage + "/" + reader.GetLastTagSortFolder() + "/" + cs.dialogueTag + "_" + audioLanguage);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        Debug.LogError("Error loading audio for: " + reader.GetLastTagSortFolder() + "/" + audioLanguage + " : " + cs.dialogueTag);
                    }
                }
            }

            currentlyLoadedLanguage = reader.subtitleLanguage;
        }
    }
}

[Serializable]
public class CutsceneSegment
{
    public float fallBackTimer = 2f;

    //Potentially put all the language specific syncing here. Leave the rest as timeline based animations
    //public ActorAnimation[] actorAnimations;
    public PlayableAsset cutsceneTimeline;
    public ActorTag talkingTarget;
    public AudioClip dialogueTrack;
    [SerializeField] private bool isDialoguePresent;
    [SerializeField] private bool isAudioPresent;
    public string dialogueTag;
    public string dialogueText;
}

[Serializable]
public class ActorAnimation
{
    public ActorTag actorTag;
    public AnimationClip animation;
}
