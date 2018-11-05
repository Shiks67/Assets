using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LeaveTest : MonoBehaviour
{

    private string AccuracyTest = "CircleTest";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            SceneManager.UnloadSceneAsync(AccuracyTest);
    }
}
