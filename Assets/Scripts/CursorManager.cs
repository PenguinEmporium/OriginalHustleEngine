using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private DefinedCursor currentCursor;

    [SerializeField] private DefinedCursor[] avaliableCursors;

    private int currentFrame;
    private float frameTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (currentCursor != null)
            //Cursor.SetCursor(currentCursor.cursorImage, currentCursor.hotspot,CursorMode.Auto);
            SetActiveCursor(currentCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCursor.cursorAnimation.Length <= 0)
            return;

        frameTimer -= Time.deltaTime;
        if(frameTimer <= 0f)
        {
            frameTimer += currentCursor.frameRate;
            currentFrame = (currentFrame + 1) % currentCursor.frameCount;
            Cursor.SetCursor(currentCursor.cursorAnimation[currentFrame], currentCursor.hotspot, CursorMode.Auto);
        }

    }

    public void ChangeCursor(string cursorName)
    {
        //Find the cursor object and set it via name
        foreach(DefinedCursor dc in avaliableCursors)
        {
            if(cursorName.Equals(dc.cursorName))
            {
                Debug.Log(cursorName + " cursor found");
                SetActiveCursor(dc);
                return;
            }
        }
    }

    public void ChangeCursor(DefinedCursor cursorObject)
    {
        SetActiveCursor(cursorObject);
    }

    private void SetActiveCursor(DefinedCursor cursor)
    {
        currentCursor = cursor;
        Cursor.SetCursor(currentCursor.cursorImage, currentCursor.hotspot, CursorMode.Auto);
        currentFrame = 0;
        frameTimer = currentCursor.frameRate;
    }
}
