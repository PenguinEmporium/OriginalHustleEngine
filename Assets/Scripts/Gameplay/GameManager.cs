using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    //The default is what the game starts at. Probably can just have the starting game mode hold this info
    public GameObject defaultPlayerPref;
    public GameObject currentPlayerPref;
    [SerializeField] public GameObject playerReference;

    public GameObject constantCanvas;
    public GameObject loadscreenPref;
    GameObject loadScreenInstance;
    
    [SerializeField] private bool PlayerControlActive = true;
    [SerializeField] private bool GameplayPaused = false;

    //[SerializeField] private string currentLevel;
    [SerializeField] private string currentLevel;
    private string managerLevel = "GameRunner";

    CursorManager cursorManager;
    CutsceneManager cutsceneManager;
    DialogueReader dialogueReader;
    OptionsManager optionsManager;

    ScaleLayer currentBaseScale;

    void Awake()
    {
        //Look for the test player. We will need to remove it when loading the main menu
        if (playerReference == null)
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
        }

        cursorManager = GetComponent<CursorManager>();
        cutsceneManager = GetComponent<CutsceneManager>();
        dialogueReader = GetComponent<DialogueReader>();
        optionsManager = GetComponent<OptionsManager>();





        if (SceneManager.sceneCount > 1)
        {
            //Look into making a manager for sub scale layers that everything can look for similar to getting a scale
            if (currentBaseScale == null)
            {
                currentBaseScale = FindObjectOfType<ScaleLayer>();
            }
        }
        else
        {
            //Could add check here to leave the player to navigate the main menu
            HidePlayer(false);
            cursorManager.ChangeCursor("Hand");

            Scene toLoad = SceneManager.GetSceneByName("MainMenu");
            if (toLoad != null)
            {
                SceneManager.LoadScene("MainMenu",LoadSceneMode.Additive);
                currentLevel = "MainMenu";
            }
            else
            {
                Debug.LogError("MainMenu scene was renamed. Please change it back to 'MainMenu'");
            }          
        }
    }


    private void Update()
    {
        if(Input.GetButtonDown("Cancel") && PlayerControlActive)
        {
            GameplayPaused = !GameplayPaused;
            optionsManager.OpenCloseOptionsMenu(GameplayPaused);

            if(GameplayPaused)
            {
                cursorManager.ChangeCursor("Hand");
            }
            else
            {
                cursorManager.ChangeCursor("Default");
            }
        }
    }

    public void EnableDisablePlayerControl(bool newState, bool cinematic = false)
    {
        if(newState)
        {
            cinematic = false;

            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(playerReference.transform.position);
            Vector2 newCursorPoint = new Vector2(playerScreenPos.x, playerScreenPos.y);

            //Set cursor to player position to avoid unnecessary movement
            Mouse.current.WarpCursorPosition(newCursorPoint);

            cursorManager.ChangeCursor("Default");
        }
        else
        {

            cursorManager.ChangeCursor("Wait");
        }

        PlayerControlActive = newState;
        playerReference.GetComponent<PlayerController>().EnableDisablePlayer(PlayerControlActive, cinematic);
    }

    public void ChangePlayerCharacter(GameObject newPlayer)
    {
        currentPlayerPref = newPlayer;
        Vector3 playerPosition = playerReference.transform.position;
        Quaternion playerQuat = playerReference.transform.rotation;
        Destroy(playerReference);
        playerReference = Instantiate(currentPlayerPref, playerPosition, playerQuat);
    }

    public void HidePlayer(bool playerState)
    {
        playerReference.gameObject.SetActive(playerState);

        if(playerState)
        {
            cursorManager.ChangeCursor("Default");
        }
        else
        {
            cursorManager.ChangeCursor("Hand");
        }
    }

    public void ReturnToMenu()
    {
        Scene toLoad = SceneManager.GetSceneByName("MainMenu");
        if (toLoad != null)
        {
            StartCoroutine(IE_LoadMainMenu());
        }
        else
        {
            Debug.LogError("MainMenu scene was renamed. Please change it back to 'MainMenu'");
        }
    }

    public void LoadLevel(string level, int entryPoint)
    {
        Scene toLoad = SceneManager.GetSceneByName(level);

        if (toLoad != null)
        {
            StartCoroutine(IE_LoadLevel(level, entryPoint));
        }
        else
        {
            Debug.LogError("Scene '" + level + "' does not exist in build list");
        }
    }

    IEnumerator IE_LoadLevel(string level, int entryPoint)
    {
        EnableDisablePlayerControl(false, true);

        //Move all this functionality to the door side
        //yield return new WaitForSeconds(exit.HandleEnter(playerReference.GetComponent<PlayerController>()));

        if (level != currentLevel)
        {
            //Black screen for loading
            loadScreenInstance = Instantiate(loadscreenPref, constantCanvas.transform);


            AsyncOperation load = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);

            yield return new WaitUntil(() => load.isDone);


            AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

            yield return new WaitUntil(() => unload.isDone);

            currentLevel = level;
        }
        //else
        //skip ahead to the next section

        yield return new WaitForFixedUpdate();


        playerReference.GetComponent<Collider2D>().enabled = false;


        //Consider using a secondary manager that simply looks for specific things on level load and cache them. Once cached, set a bool to true that this 
        //function can reference for a WaitUntil pause
        SceneDoor[] doors = FindObjectsOfType<SceneDoor>();

        SceneDoor entryDoor = null;

        foreach(SceneDoor sd in doors)
        {
            if (sd.entryNumber == entryPoint)
            {
                entryDoor = sd;

                sd.DisableReEntry();

                Vector3 targetDest = sd.transform.position;

                targetDest.z = 0f;

                playerReference.transform.SetPositionAndRotation(sd.transform.position, playerReference.transform.rotation);
            }
        }

        //Replace with manager as needed
        currentBaseScale = FindObjectOfType<ScaleLayer>();

        playerReference.GetComponentInChildren<ScalableObject>().AssignNewLayer(currentBaseScale);

        yield return new WaitForSeconds(0.5f);

        if (loadScreenInstance != null)
            Destroy(loadScreenInstance);

        yield return new WaitForSeconds(entryDoor.HandleExit(playerReference.GetComponent<PlayerController>()));


        playerReference.GetComponent<Collider2D>().enabled = true;

        EnableDisablePlayerControl(true);
    }

    IEnumerator IE_LoadMainMenu()//string level, int entryPoint, Vector3 walkTransition)
    {
        EnableDisablePlayerControl(false);

        //Black screen for loading
        loadScreenInstance = Instantiate(loadscreenPref, constantCanvas.transform);

        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentLevel);

        yield return new WaitUntil(() => unload.isDone);

        currentLevel = "MainMenu";

        AsyncOperation load = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);

        yield return new WaitUntil(() => load.isDone);

        //else
        //skip ahead to the next section

        yield return new WaitForFixedUpdate();

        Destroy(playerReference);
        cursorManager.ChangeCursor("Hand");

        yield return new WaitForSeconds(0.5f);

        if (loadScreenInstance != null)
            Destroy(loadScreenInstance);
    }

    public void StartCutscene(PlayableDirector director, Cutscene dialogue)
    {
        EnableDisablePlayerControl(false,true);

        cutsceneManager.StartCutscene(director, dialogue);
    }

    public void CutsceneEnded()
    {
        EnableDisablePlayerControl(true);
    }
}
