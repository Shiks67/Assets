using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{

    private float timer = 0.25f;
    private float waitTime = 0;
    private Renderer rend;

    private Transform oldParent;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        oldParent = gameObject.transform.parent;
        gameObject.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(oldParent == null)
        {
            Destroy(gameObject);
        }
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
