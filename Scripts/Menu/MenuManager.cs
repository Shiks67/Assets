using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;
    private string AccuracyTest = "CircleTest";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            menu.gameObject.SetActive(!menu.activeSelf);
        if (menu.gameObject.activeSelf == true)
        {
            if (Input.GetKeyUp(KeyCode.Y))
            {
                StartCoroutine(LoadCurrentScene());
                menu.gameObject.SetActive(!menu.activeSelf);
            }
            if (Input.GetKeyUp(KeyCode.N))
            {
                SceneManager.UnloadSceneAsync(AccuracyTest);
                menu.gameObject.SetActive(!menu.activeSelf);
            }
        }
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
