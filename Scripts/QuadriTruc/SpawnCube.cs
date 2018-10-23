using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnCube : MonoBehaviour
{

    public GameObject spawnObject;
    private Vector3[] spawnArea =
    {new Vector3(-30f,30f,-1f), new Vector3(0f,30f,-1f), new Vector3(30f,30f,-1f),
    new Vector3(-30f,0f,-1f), new Vector3(0f,0f,-1f), new Vector3(30f,0f,-1f),
    new Vector3(-30f,-30f,-1f), new Vector3(0f,-30f,-1f), new Vector3(30f,-30f,-1f)};
    public int[] cubeSize = { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
    public int index;
    private int lastIndex;
    private GameObject canvasParent;

    // Use this for initialization
    void Start()
    {
        canvasParent = GameObject.Find("Quadri");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("hitCube") == null)
        {
            index = Random.Range(0, 9);
            if (cubeSize.Any(size => size > 1))
            {
                while (cubeSize[index] <= 1 || index == lastIndex)
                {
                    index = Random.Range(0, 9);
                }
            }
            else
            {
                Result();
            }
            lastIndex = index;
            newCube(index);
        }
    }

    private void newCube(int id)
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.SetParent(canvasParent.transform);
        newObject.transform.localScale = new Vector3(cubeSize[id], cubeSize[id], 1);
        newObject.transform.localRotation = Quaternion.identity;

        switch (cubeSize[id])
        {
            case 30:
                newObject.transform.localPosition = spawnArea[id];
                break;
            case 20:
                newObject.transform.localPosition =
                new Vector3(Random.Range(spawnArea[id].x - 5, spawnArea[id].x + 5),
                Random.Range(spawnArea[id].y - 5, spawnArea[id].y + 5), -1f);
                break;
            case 10:
                newObject.transform.localPosition =
                new Vector3(Random.Range(spawnArea[id].x - 10, spawnArea[id].x + 10),
                Random.Range(spawnArea[id].y - 10, spawnArea[id].y + 10), -1f);
                break;
        }
        if (cubeSize[id] < 10)
        {
            newObject.transform.localPosition =
            new Vector3(Random.Range(spawnArea[id].x - 12.5f, spawnArea[id].x + 12.5f),
            Random.Range(spawnArea[id].y - 12.5f, spawnArea[id].y + 12.5f), -1f);
        }
    }

    public void Result()
    {
        DestroyAllCubes();
        for (int i = 0; i < 9; i++)
        {
            newCube(i);
        }
    }

    public void DestroyAllCubes(bool retry = false)
    {
        if (retry)
        {
            cubeSize = new int[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
        }
        var allCubes = GameObject.FindGameObjectsWithTag("hitCube");
        for (var i = 0; i < allCubes.Length; i++)
        {
            Destroy(allCubes[i]);
        }
    }
}
