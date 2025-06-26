using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCursor",menuName = "UI Elements/Cursor", order = 0)]
public class DefinedCursor : ScriptableObject
{
    [SerializeField] public string cursorName;
    [SerializeField] public Texture2D cursorImage;
    [SerializeField] public Texture2D[] cursorAnimation;

    [SerializeField] public Vector2 hotspot = new Vector2(0,0);

    public int frameCount;
    [SerializeField] public float frameRate = 0.2f;

    private void Awake()
    {
        frameCount = cursorAnimation.Length;
    }

}
