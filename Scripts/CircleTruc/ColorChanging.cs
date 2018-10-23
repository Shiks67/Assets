using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{

    private float timer = 0.25f;
    private float waitTime = 0;
	private Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (waitTime < timer)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;

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
