using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AccuracyTestManager : MonoBehaviour
{
    private string AccuracyTest = "CircleTest";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
            StartCoroutine(LoadCurrentScene());
		if(Input.GetKeyUp(KeyCode.N))
			gameObject.SetActive(false);
    }

    IEnumerator LoadCurrentScene()
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(AccuracyTest
            , LoadSceneMode.Additive);

        while (!asyncScene.isDone)
        {
            yield return null;
        }
    }
}
