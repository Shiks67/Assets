using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Text countObj;
    private float countDown = 3f;

    // Use this for initialization
    void Start()
    {
        canvasParent = GameObject.Find("Quadri");
        index = 0;
        Destroy(countObj, 2.4f);
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countObj != null)
        {
            countObj.text = System.Math.Round(countDown, 0).ToString();
            return;
        }

        if (GameObject.FindGameObjectsWithTag("hitCircle").Length < 1
        && GameObject.FindGameObjectsWithTag("hitCircle").Length < 5)
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

    /// <summary> 
    /// Create a new circle with his id's informations 
    /// </summary>
    /// <param name="id">id of the circle in the array that contain all the positions</param>
    /// <param name="result">if true the circle will have the final size (after calibration test)</param>
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

    /// <summary>
    /// Show every circle with their final size
    /// </summary>
    public void Result()
    {
        DestroyAllCircles();
        for (int i = 0; i < 9; i++)
        {
            newCircle(i, true);
        }
    }

    ///<summary> Destroy every GameObject tagged "hitCircle"
    ///<typeparam name="Retry">if true the size and visited positions are reseted</typeparam>
    ///</summary>
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
