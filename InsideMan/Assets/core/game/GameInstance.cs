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

	public bool isGameOver	= false;
	public bool hasLost		= false;
	float lostReloadLevelTimer;
	float lostReloadLevelTimerDuration = 1f;
	

	public float detectionTime = 0.6f;
	public float cameraChance = 0.25f;

	private void Start(){
		var cameras = FindObjectsOfType<NPC_Camera>();
		for (int i = 0; i < cameras.Length; i++) {
			if (cameras[i].isActiveAndEnabled){
				cameras[i].SetCameraNumber(i + 1);
			}
		}
	}

	private void Update()
	{
		if (isGameOver && hasLost)
		{
			if (lostReloadLevelTimer < Time.time)
			{
				RestartGame();
			}
		}
	}

	public void ShowGameOver(bool wasVictory){
		isGameOver = true;
		thiefGameOver.SetActive(true);
		hackerGameOver.SetActive(true);
		int sharedEarnings = 0;
		int thiefEarnings = 0;
		int hackerEarnings = 0;
		if (wasVictory){
			sharedEarnings = successEarnings;
		}else{
			lostReloadLevelTimer = Time.time + lostReloadLevelTimerDuration;
			hasLost = true;
			HackerCam.transform.position = ThiefCam.transform.position;
			HackerCam.GetComponent<Camera>().orthographicSize	= ThiefCam.GetComponent<Camera>().orthographicSize;
		}
		hackerGameOver.GetComponent<GameOver>().UpdateText(wasVictory, sharedEarnings, thiefEarnings, hackerEarnings);
		thiefGameOver.GetComponent<GameOver>().UpdateText(wasVictory, sharedEarnings, thiefEarnings, hackerEarnings);
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
	