using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private CircleData da;
    private RaycastHit hit;
    private Camera mainCamera;
    private SpawnCircle sc;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        da = mainCamera.GetComponent<CircleData>();
        var spawnCircle = GameObject.Find("Quadri");
        sc = spawnCircle.GetComponent<SpawnCircle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(da.ray, out hit))
        {
            if (GameObject.FindGameObjectsWithTag("hitCircle").Length <= 1)
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "hitCircle":
                        ReduceCircle(hit.transform.gameObject);
                        break;
                    case "Finish":
                        sc.Result();
                        break;
                    case "Respawn":
                        sc.DestroyAllCircles(true);
                        break;
                    default:
                        ExtendCircle(GameObject.FindGameObjectWithTag("hitCircle").gameObject);
                        return;
                }
            }
            else
            {
                if (hit.transform.gameObject.tag == "Respawn")
                {
                    sc.DestroyAllCircles(true);
                }
                if (hit.transform.gameObject.tag == "Resume")
                {
                    sc.DestroyAllCircles();
                }
            }
        }
    }

    private void ReduceCircle(GameObject circle)
    {
        circle.transform.localScale =
        new Vector3(circle.transform.localScale.x - 10f * Time.deltaTime,
        0.1f, circle.transform.localScale.z - 10f * Time.deltaTime);
    }

    private void ExtendCircle(GameObject circle)
    {
        if (circle.transform.localScale.x < 30)
        {
            circle.transform.localScale =
            new Vector3(circle.transform.localScale.x + 2f * Time.deltaTime,
            0.1f, circle.transform.localScale.z + 2f * Time.deltaTime);
        }
    }
}