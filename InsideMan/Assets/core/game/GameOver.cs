using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public Text gameOverText;
	public Text sharedText;
	public Text thiefText;
	public Text hackerText;

	void Start(){
	}

	public void UpdateText (bool wasSuccessful, int shared, int thief, int hacker){
		if(wasSuccessful){
			gameOverText.text = "Escaped!";
		}else{
			gameOverText.text = "Captured!";
		}
		sharedText.text = "Shared: $" + shared;
		thiefText.text = "Thief: $" + thief;
		hackerText.text = "Hacker: $" + hacker;
	}
}
