using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private CircleData da;
    private RaycastHit hit;
    private Camera mainCamera;
    // private GameObject mainCamera;
    private SpawnCircle sc;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        // mainCamera = GameObject.FindGameObjectWithTag("EditorOnly").gameObject;
        da = mainCamera.GetComponent<CircleData>();
        var spawnCircle = GameObject.Find("Quadri");
        sc = spawnCircle.GetComponent<SpawnCircle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            Destroy(GameObject.FindGameObjectWithTag("hitCircle").gameObject);
            // sc.Result();
        }

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
        new Vector3(circle.transform.localScale.x - 10f * Time.deltaTime,
        0.1f, circle.transform.localScale.z - 10f * Time.deltaTime);
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