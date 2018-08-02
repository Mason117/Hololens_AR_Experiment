using UnityEngine;
using System.Collections;
using System.Globalization;

public class TestPart : MonoBehaviour
{
    private float[] noiseValues;
    private int number = 0;
    void Start()
    {
        Random.InitState(42);
        noiseValues = new float[10];
        for (int i = 0; i < noiseValues.Length; i++)
        {
            noiseValues[i] = Random.value;
            Debug.Log(noiseValues[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(Time.time);
            number++;
            Debug.Log(number);
        }
    }

}