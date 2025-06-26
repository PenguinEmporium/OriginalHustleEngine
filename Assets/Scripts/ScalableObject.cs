using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObject : MonoBehaviour
{
    [SerializeField] private float objectScale = 1f;

    [SerializeField] ScaleLayer scaleLayer;

    [SerializeField] bool scaleEnabled = true;

    private void Awake()
    {
        scaleLayer = FindObjectOfType<ScaleLayer>();
    }


    void Update()
    {
        if(!scaleEnabled)
        {
            SetScale(1f, true);
            return;
        }


        if (scaleLayer != null)
        {
            SetScale(scaleLayer.GetScaleAtPoint(transform.position));
        }
        else
        {
            //Fall back
            scaleLayer = FindObjectOfType<ScaleLayer>();
        }
    }

    public float GetScaleValue()
    {
        return objectScale;
    }


    public void SetScale(float newScale, bool lossy = false)
    {
        float difference = newScale - objectScale;

        gameObject.transform.localScale += new Vector3(difference, difference, difference);

        if(!lossy)
            objectScale = newScale;
    }

    public void ChangeScale(float changeInScale)
    {
        float difference = changeInScale;

        gameObject.transform.localScale += new Vector3(difference, difference, difference);

        objectScale += changeInScale;
    }

    public void AssignNewLayer(ScaleLayer newLayer)
    {
        scaleLayer = newLayer;
    }
}
