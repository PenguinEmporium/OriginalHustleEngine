using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public AudioSettings audioSettings;
    public LanguageSettings languageSettings;
    private GameManager gameManager;
    private DialogueReader dialogueReader;

    public OptionsMenu optionsMenu;


    public AudioMixerGroup masterMix;
    public AudioMixerGroup dialogueMix;
    public AudioMixerGroup musicMix;
    public AudioMixerGroup sfxMix;


    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        dialogueReader = GetComponent<DialogueReader>();
        dialogueReader.Startup();


        audioSettings = new AudioSettings();
        audioSettings.LoadSettings();

        InitializeAudio();

        languageSettings = new LanguageSettings();
        languageSettings.LoadSettings();

        SendLanguageUpdateToPreloads();

        optionsMenu.Initilize(this, dialogueReader);
        OpenCloseOptionsMenu(false);
    }

    void InitializeAudio()
    {
        ChangeMaster(audioSettings.masterAudioLevel);
        ChangeDialogue(audioSettings.dialogueAudioLevel);
        ChangeMusic(audioSettings.musicAudioLevel);
        ChangeSFX(audioSettings.sfxAudioLevel);
    }


    private void SendLanguageUpdateToPreloads()
    {
        PreloadableScriptable[] preloads = Resources.FindObjectsOfTypeAll<PreloadableScriptable>();

        foreach (PreloadableScriptable ps in preloads)
        {
            ps.Preload(dialogueReader);
        }
    }

    public void OpenCloseOptionsMenu(bool newState)
    {
        //Potentially initilize it everytime this opens
        optionsMenu.gameObject.SetActive(newState);
    }

    public void ChangeSubtitleLanguage(int newLanguage)
    {
        languageSettings.textLanguageSelected = newLanguage;

        if (dialogueReader.languages.Count > newLanguage)
        {
            dialogueReader.subtitleLanguage = dialogueReader.languages[newLanguage];

            SendLanguageUpdateToPreloads();
        }
        else
        {
            Debug.LogError("Language not found");
        }
    }

    public void ChangeDialogueLanguage(int newLanguage)
    {
        languageSettings.audioLanguageSelected = newLanguage;

        if (dialogueReader.languages.Count > newLanguage)
        {
            dialogueReader.audioLanguage = dialogueReader.languages[newLanguage];

            SendLanguageUpdateToPreloads();
        }
        else
        {
            Debug.LogError("Language not found");
        }
    }

    public void ChangeUILanguage(int newLanguage)
    {
        languageSettings.uiLanguageSelected = newLanguage;

        if (dialogueReader.languages.Count > newLanguage)
        {
            dialogueReader.UILanguage = dialogueReader.languages[newLanguage];

            SendLanguageUpdateToPreloads();
        }
        else
        {
            Debug.LogError("Language not found");
        }
    }

    public void ChangeMaster(float level)
    {
        masterMix.audioMixer.SetFloat("MasterVol", Mathf.Log10(level) * 20);
    }
    public void ChangeDialogue(float level)
    {
        dialogueMix.audioMixer.SetFloat("DialogueVol", Mathf.Log10(level) * 20);
    }
    public void ChangeMusic(float level)
    {
        musicMix.audioMixer.SetFloat("MusicVol", Mathf.Log10(level) * 20);
    }
    public void ChangeSFX(float level)
    {
        sfxMix.audioMixer.SetFloat("SFXVol", Mathf.Log10(level) * 20);
    }
}

public abstract class Setting
{
    public abstract void LoadSettings();
    public abstract void SaveSettings();
}

public class AudioSettings : Setting
{
    public float masterAudioLevel = 0.75f;
    public float dialogueAudioLevel = 0.75f;
    public float musicAudioLevel = 0.75f;
    public float sfxAudioLevel = 0.75f;

    public override void LoadSettings()
    {
        masterAudioLevel = PlayerPrefs.GetFloat("masteraudio", 0.75f);
        dialogueAudioLevel = PlayerPrefs.GetFloat("dialogueaudio", 0.75f);
        musicAudioLevel = PlayerPrefs.GetFloat("musicaudio", 0.75f);
        sfxAudioLevel = PlayerPrefs.GetFloat("sfxaudio", 0.75f);
    }

    public override void SaveSettings()
    {
        PlayerPrefs.SetFloat("masteraudio", masterAudioLevel);
        PlayerPrefs.SetFloat("dialogueaudio", dialogueAudioLevel);
        PlayerPrefs.SetFloat("musicaudio", musicAudioLevel);
        PlayerPrefs.SetFloat("sfxaudio", sfxAudioLevel);
        PlayerPrefs.Save();
    }
}

public class LanguageSettings : Setting
{
    public int textLanguageSelected = 0;
    public int audioLanguageSelected = 1;
    public int uiLanguageSelected = 0;

    public override void LoadSettings()
    {
        textLanguageSelected = PlayerPrefs.GetInt("textlanguage", 0);
        audioLanguageSelected = PlayerPrefs.GetInt("audiolanguage", 0);
        audioLanguageSelected = PlayerPrefs.GetInt("uilanguage", 0);
    }

    public override void SaveSettings()
    {
        PlayerPrefs.SetInt("textlanguage", textLanguageSelected);
        PlayerPrefs.SetInt("audiolanguage", audioLanguageSelected);
        PlayerPrefs.SetInt("uilanguage", audioLanguageSelected);
        PlayerPrefs.Save();
    }
}