using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public Text gameOverText;
	public Text sharedText;
	public Text thiefText;
	public Text hackerText;
	public GameObject panel;
	public GameObject button;
	public GameObject sharedLabel;
	public GameObject personalLabel;

	void Start(){
	}

	public void UpdateText (bool wasSuccessful, int shared, int thief, int hacker){
		if(wasSuccessful){
			gameOverText.text = "Escaped!";
			sharedText.text = "Shared: $" + shared;
			thiefText.text = "Thief: $" + thief;
			hackerText.text = "Hacker: $" + hacker;
		}
		else{
			gameOverText.text = "Captured!";
			sharedText.text = "";
			thiefText.text = "";
			hackerText.text = "";
			panel.GetComponent<Image>().color = new Color(1f,0f,0f,0.25f);
			button.SetActive(false);
			sharedLabel.SetActive(false);
			personalLabel.SetActive(false);
		}
	}
}