using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLifeTime : MonoBehaviour
{

    private float countDown;
    public float LifeTime = 1.3f;
    // Use this for initialization
    void Start()
    {
        countDown = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("hitCube").Length == 1)
        {
            countDown -= Time.deltaTime;
            if (countDown < 0f)
            {
                Destroy(gameObject);
                countDown = LifeTime;
                print("CubeMissed");
            }
        }
    }
}