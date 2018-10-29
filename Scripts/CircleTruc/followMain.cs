using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMain : MonoBehaviour {

	public GameObject followObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = followObject.transform.rotation;
		gameObject.transform.position = followObject.transform.position;
	}
}
