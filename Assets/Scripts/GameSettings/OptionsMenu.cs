using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class OptionsMenu : MonoBehaviour
{
    
    public OptionsManager optionsManager;

    //For testing purposes. It should only hang on to the optionsManager
    //public DialogueReader dialogueManager;

    public TMP_Dropdown textLanguage;
    public TMP_Dropdown audioLanguage;
    public TMP_Dropdown uiLanguage;

    public Slider masterAudio;
    public Slider dialogueAudio;
    public Slider musicAudio;
    public Slider sfxAudio;

    void Start()
    {
        //Testing purposes
        //Initilize(optionsManager, dialogueManager);

    }


    public void Initilize(OptionsManager options, DialogueReader dialogue)
    {
        #region Initilize Audio Options

        masterAudio.value = options.audioSettings.masterAudioLevel;
        dialogueAudio.value = options.audioSettings.dialogueAudioLevel;
        musicAudio.value = options.audioSettings.musicAudioLevel;
        sfxAudio.value = options.audioSettings.sfxAudioLevel;

        masterAudio.onValueChanged.RemoveAllListeners();
        masterAudio.onValueChanged.AddListener(options.ChangeMaster);

        dialogueAudio.onValueChanged.RemoveAllListeners();
        dialogueAudio.onValueChanged.AddListener(options.ChangeDialogue);

        musicAudio.onValueChanged.RemoveAllListeners();
        musicAudio.onValueChanged.AddListener(options.ChangeMusic);

        sfxAudio.onValueChanged.RemoveAllListeners();
        sfxAudio.onValueChanged.AddListener(options.ChangeSFX);

        #endregion

        #region Initilize Language Options

        textLanguage.ClearOptions();
        audioLanguage.ClearOptions();
        uiLanguage.ClearOptions();

        foreach (LanguageOption lo in dialogue.languages)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData(lo.LanguageName + ":" + lo.percentComplete);
            textLanguage.options.Add(newOption);
            audioLanguage.options.Add(newOption);
            uiLanguage.options.Add(newOption);
        }

        //Subtract 2 since that's where the language names start
        textLanguage.value = dialogue.subtitleLanguage.colInFile - 2;
        audioLanguage.value = dialogue.audioLanguage.colInFile - 2;
        uiLanguage.value = dialogue.UILanguage.colInFile - 2;

        audioLanguage.onValueChanged.RemoveAllListeners();
        audioLanguage.onValueChanged.AddListener(options.ChangeDialogueLanguage);

        textLanguage.onValueChanged.RemoveAllListeners();
        textLanguage.onValueChanged.AddListener(options.ChangeSubtitleLanguage);

        uiLanguage.onValueChanged.RemoveAllListeners();
        uiLanguage.onValueChanged.AddListener(options.ChangeUILanguage);
        #endregion
    }

}
