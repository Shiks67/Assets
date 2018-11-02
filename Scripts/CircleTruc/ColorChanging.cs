using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{

    private float timer = 0.25f;
    private float waitTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime < timer)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        if (waitTime > timer)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (waitTime > timer * 2)
        {
            waitTime = 0;
        }
    }
}
