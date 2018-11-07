using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private CircleData da;
    private RaycastHit hit;
    // private GameObject mainCamera;
    private SpawnCircle sc;

    // Use this for initialization
    void Start()
    {
        // da = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        da = Camera.main.GetComponent<CircleData>();
        var spawnCircle = GameObject.Find("Quadri");
        sc = spawnCircle.GetComponent<SpawnCircle>();
        SceneManager.UnloadSceneAsync("CircleTest 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(da.ray, out hit))
        {
            //if there is max 1 circle on the grid
            if (GameObject.FindGameObjectsWithTag("hitCircle").Length == 1)
            {
                //Switch on hitted object's tag by the gaze
                switch (hit.transform.gameObject.tag)
                {
                    case "hitCircle": //if the tag is hitCircle, reduce the size of the circle
                        ReduceCircle(hit.transform.gameObject);
                        break;
                    case "Finish": //show the result, size of every circle
                        sc.Result();
                        break;
                    case "Respawn": //reset all circles so we can try again
                        sc.DestroyAllCircles(true);
                        break;
                    default: //by default we extend the size of the circle
                        ExtendCircle(GameObject.FindGameObjectWithTag("hitCircle").gameObject);
                        return;
                }
            }
            else
            {
                //after Retry button is focused
                if (hit.transform.gameObject.tag == "Respawn")
                {
                    sc.DestroyAllCircles(true);
                }
                //after Resume button is focused
                if (hit.transform.gameObject.tag == "Resume")
                {
                    sc.DestroyAllCircles();
                }
            }
        }
    }

    /// <summary>
    /// Reduce the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ReduceCircle(GameObject circle)
    {
        //10f * Time.deltaTime so the computers speed doesn't affect the speed
        circle.transform.localScale =
        new Vector3(circle.transform.localScale.x - 15f * (Time.deltaTime * 3),
        0.1f, circle.transform.localScale.z - 15f * (Time.deltaTime * 3));
        // var childs = circle.GetComponentsInChildren<Transform>();
        // foreach (Transform child in childs)
        // {
        //     child.localScale = new Vector3(child.localScale.x + 10f * Time.deltaTime,
        //     child.localScale.y + 10f * Time.deltaTime, 0.0001f);
        // }
    }

    /// <summary>
    /// Extend the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ExtendCircle(GameObject circle)
    {
        //if it's smaller than the max circle size
        if (circle.transform.localScale.x < 30)
        {
            //2f * Time.deltaTime so the computers speed doesn't affect the speed
            circle.transform.localScale =
            new Vector3(circle.transform.localScale.x + 2f * Time.deltaTime,
            0.1f, circle.transform.localScale.z + 2f * Time.deltaTime);
        }
    }
}