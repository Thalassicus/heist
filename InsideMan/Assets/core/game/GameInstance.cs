using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour {
	public GameObject ThiefCam;
	public GameObject HackerCam;
	public GameObject thief;
	public GameObject hacker;
	public GameObject thiefGameOver;
	public GameObject hackerGameOver;

	public int successEarnings = 50000;

	public bool isGameOver = false;

	public void ShowGameOver(bool wasVictory){
		isGameOver = true;
		thiefGameOver.SetActive (true);
		hackerGameOver.SetActive (true);
		int sharedEarnings = 0;
		int thiefEarnings = 0;
		int hackerEarnings = 0;
		if (wasVictory) {
			sharedEarnings = successEarnings;
		}
		hackerGameOver.GetComponent<GameOver> ().UpdateText (wasVictory, sharedEarnings, thiefEarnings, hackerEarnings);
		thiefGameOver.GetComponent<GameOver> ().UpdateText (wasVictory, sharedEarnings, thiefEarnings, hackerEarnings);
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
	