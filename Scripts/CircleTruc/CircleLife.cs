using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleLife : MonoBehaviour
{

    private Vector3 lastSize;
    private Vector3 currentSize;
    private float missTimer = 2f;
    private int nbOfChange;
    private float limitTimer;
    public float TTFF;
    private bool isBigger, isSmaller;
    public bool isTTFF;
    private SpawnCircle sc;
    private int index;
    private float life = 3f;

    public void Init(int index)
    {
        this.index = index;
    }

    // Use this for initialization
    void Start()
    {
        nbOfChange = 0;
        lastSize = gameObject.transform.localScale;
        var gameC = GameObject.Find("Quadri");
        sc = gameC.GetComponent<SpawnCircle>();
        isSmaller = true;
        isBigger = true;
        isTTFF = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("hitCircle").Length > 1)
            return;
        if (isTTFF)
            TTFF += Time.deltaTime;
        currentSize = gameObject.transform.localScale;
        if (currentSize.x < lastSize.x)
        {
            Reducing();
            isTTFF = false;
        }
        if (currentSize.x > lastSize.x)
        {
            Extending();
        }
        if (currentSize.x <= 0 || nbOfChange > 5)
        {
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }
        limitTimer -= Time.deltaTime;
        if (limitTimer < 0)
        {
            limitTimer = 1f;
            nbOfChange = 0;
        }

        life -= Time.deltaTime;
        if (life < 0)
        {
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }

    }

    private void Reducing()
    {
        lastSize = currentSize;
        missTimer = 1f;
        if (isSmaller)
        {
            nbOfChange++;
            isSmaller = false;
            isBigger = true;
        }
    }

    private void Extending()
    {
        missTimer -= Time.deltaTime;
        lastSize = currentSize;
        if (missTimer < 0f)
        {
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }
        if (isBigger)
        {
            nbOfChange++;
            isBigger = false;
            isSmaller = true;
        }
    }
}
