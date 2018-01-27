using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Thief"){
			Debug.Log("Seen!");
		}
	}
}
