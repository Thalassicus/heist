using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour {
	public GameObject passToObject;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		passToObject.GetComponent<NPC_Camera>().DoTriggerEnter2D(collision);
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		passToObject.GetComponent<NPC_Camera>().DoTriggerExit2D(collision);
	}

	public void OnTriggerStay2D(Collider2D collision)
	{
		passToObject.GetComponent<NPC_Camera>().DoTriggerStay2D(collision);
	}
}
