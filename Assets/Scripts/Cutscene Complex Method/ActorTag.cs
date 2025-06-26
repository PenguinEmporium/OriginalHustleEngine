using UnityEngine;

[CreateAssetMenu(fileName = "NewActor",menuName = "Cutscenes/ActorTag",order = 1)]
public class ActorTag : ScriptableObject
{
    public string actorName = "[...]";
    public Color subtitleColor = Color.yellow;
}
