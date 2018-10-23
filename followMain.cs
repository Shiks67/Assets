using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMain : MonoBehaviour {

	private Transform maincam;

	// Use this for initialization
	void Start () {
		maincam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation = maincam.rotation;
		gameObject.transform.position = maincam.position;
	}
}
