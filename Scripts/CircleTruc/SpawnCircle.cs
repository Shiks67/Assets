using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnCircle : MonoBehaviour
{

    public GameObject spawnObject;
    private Vector3[] spawnArea =
    {new Vector3(-30f,30f,-0.1f), new Vector3(0f,30f,-0.1f), new Vector3(30f,30f,-0.1f),
    new Vector3(-30f,0f,-0.1f), new Vector3(0f,0f,-0.1f), new Vector3(30f,0f,-0.1f),
    new Vector3(-30f,-30f,-0.1f), new Vector3(0f,-30f,-0.1f), new Vector3(30f,-30f,-0.1f)};

    public float[] circleFinalSize = new float[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
    private bool[] isVisited = new bool[9];

    private int index;
    private GameObject canvasParent;

    // Use this for initialization
    void Start()
    {
        canvasParent = GameObject.Find("Quadri");
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("hitCircle") == null)
        {
            if (isVisited.Contains(false))
            {
                index = Random.Range(0, 9);
                while (isVisited[index] == true)
                {
                    index = Random.Range(0, 9);
                }
                newCircle(index);
                isVisited[index] = true;
            }
            else
            {
                Result();
            }
        }
    }

    private void newCircle(int id, bool result = false)
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.SetParent(canvasParent.transform);
        newObject.transform.localScale = new Vector3(30, 0.1f, 30);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = spawnArea[id];
        newObject.GetComponent<CircleLife>().Init(id);
        if (result)
        {
            newObject.transform.localScale =
            new Vector3(circleFinalSize[id], 0.1f, circleFinalSize[id]);
        }
    }

    public void Result()
    {
        DestroyAllCircles();
        for (int i = 0; i < 9; i++)
        {
            newCircle(i, true);
        }
    }

    public void DestroyAllCircles(bool retry = false)
    {
        if (retry)
        {
            circleFinalSize = new float[] { 30, 30, 30, 30, 30, 30, 30, 30, 30 };
            isVisited = new bool[9];
        }
        var allCircles = GameObject.FindGameObjectsWithTag("hitCircle");
        for (var i = 0; i < allCircles.Length; i++)
        {
            Destroy(allCircles[i]);
        }
    }
}
