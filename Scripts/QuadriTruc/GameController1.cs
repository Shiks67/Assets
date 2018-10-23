using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController1 : MonoBehaviour
{

    private DataAccess da;
    private RaycastHit hit;
    private GameObject hitObject = null;
    private SpawnCube sc;
    public float timeToFix = 0.3f;
    private float fixCountDown;
    private Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        da = mainCamera.GetComponent<DataAccess>();
        var spawnObject = GameObject.Find("Quadri");
        sc = spawnObject.GetComponent<SpawnCube>();
        fixCountDown = timeToFix;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(da.ray, out hit))
        {
            if (GameObject.FindGameObjectsWithTag("hitCube").Length <= 1)
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "hitCube":
                        fixCountDown -= Time.deltaTime;
                        if (fixCountDown < 0f)
                            DestroyCube();
                        break;
                    case "Finish":
                        sc.Result();
                        break;
                    case "Respawn":
                        sc.DestroyAllCubes(true);
                        break;
                    default:
                        fixCountDown = timeToFix;
                        break;
                }
            }
            else
            {
                if (hit.transform.gameObject.tag == "Respawn")
                {
                    sc.DestroyAllCubes(true);
                    fixCountDown = timeToFix;
                }
                if (hit.transform.gameObject.tag == "Resume")
                {
                    sc.DestroyAllCubes();
                    fixCountDown = timeToFix;
                }
            }
        }
    }

    public bool Missed;
    public float lastSucceded;
    public float lastFailed;

    private void DestroyCube()
    {
        hitObject = hit.transform.gameObject;

        if (sc.cubeSize[sc.index] <= 5 && sc.cubeSize[sc.index] > 1)
        {
            sc.cubeSize[sc.index] -= 1;
        }
        else if (sc.cubeSize[sc.index] == 10)
        {
            sc.cubeSize[sc.index] -= 5;
        }
        else
        {
            sc.cubeSize[sc.index] -= 10;
        }
        fixCountDown = timeToFix;
        print("cube destroyed");
        /* **************************************************************** */

        if (Missed)
        {
            sc.cubeSize[sc.index] = Convert.ToInt32((lastSucceded + hitObject.gameObject.transform.localScale.x) / 2);
        }
        else
        {
            if (sc.cubeSize[sc.index] > 5)
                sc.cubeSize[sc.index] -= 5;
            if (sc.cubeSize[sc.index] <= 5 && sc.cubeSize[sc.index] > 1)
                sc.cubeSize[sc.index] -= 1;
        }

        /* **************************************************************** */
        Destroy(hitObject.gameObject);
    }
}
