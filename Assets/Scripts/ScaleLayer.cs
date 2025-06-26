using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLayer : MonoBehaviour
{
    [SerializeField] private float baseLayerScale = 1f;

    [SerializeField] private Transform vanishingPoint;
    [SerializeField] private Transform normalPoint;

    [SerializeField] Vector3 VtoN;

    void Awake()
    {
        VtoN = vanishingPoint.position - normalPoint.position;
    }

    public float GetScaleAtPoint(Vector3 worldPosition)
    {
        float returnScale = baseLayerScale;

        Vector3 playerVector = vanishingPoint.position - worldPosition;

        playerVector = Vector3.Project(playerVector, VtoN);

        float playerMag = Mathf.Clamp(playerVector.magnitude/VtoN.magnitude, 0.1f, 1f);

        returnScale = playerMag * baseLayerScale;

        //Debug.Log("Scale at point: "+ worldPosition.ToString() + " : "+ returnScale);
        return returnScale;
    }
}
