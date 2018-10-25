using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleLife : MonoBehaviour
{

    private Vector3 lastSize;
    private Vector3 currentSize;
    private int nbOfSwitch;
    private float limitTimer;
    public float TTFF;
    private bool isBigger, isSmaller;
    public bool isTTFF;
    private SpawnCircle sc;
    private int index;
    public float lifeTime;

    public void Init(int index)
    {
        this.index = index;
    }

    // Use this for initialization
    void Start()
    {
        nbOfSwitch = 0;
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
        //if there is more than 1 circle, return
        if (GameObject.FindGameObjectsWithTag("hitCircle").Length > 1)
            return;
        if (isTTFF) //update TTFF time until the circle is focused by the gaze point
            TTFF += Time.deltaTime;
        //current size of the circle
        currentSize = gameObject.transform.localScale;
        if (currentSize.x < lastSize.x)
        {
            Reducing();
            //put isTTFF false so the TTFF time doesn't update anymore
            isTTFF = false;
        }
        if (currentSize.x > lastSize.x)
        {
            Extending();
        }
        //if the size of the circle is 0 or less OR if the size edit switched more than 5 times
        if (currentSize.x <= 0 || nbOfSwitch > 5)
        {
            //save the finalsize of the circle and destroy it
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            print(index + " size " + sc.circleFinalSize[index]);
            Destroy(gameObject);
        }
        //every second reset the number of switch between reducing and extending the circle's size
        limitTimer -= Time.deltaTime;
        if (limitTimer < 0)
        {
            limitTimer = 1f;
            nbOfSwitch = 0;
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            sc.circleFinalSize[index] = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// update circle's last size, and increment the number of switch put the reducing bool at false and the extending one to true
    /// </summary>
    private void Reducing()
    {
        lastSize = currentSize;
        if (isSmaller)
        {
            nbOfSwitch++;
            isSmaller = false;
            isBigger = true;
        }
    }

    /// <summary>
    /// update circle's last size, and increment the number of switch put the extending bool at false and the reducing one to true
    /// </summary>
    private void Extending()
    {
        lastSize = currentSize;
        if (isBigger)
        {
            nbOfSwitch++;
            isBigger = false;
            isSmaller = true;
        }
    }
}
