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
	public int sharedEarnings = 0;
	public int thiefEarnings = 0;
	public int hackerEarnings = 0;

	public bool isGameOver	= false;
	public bool hasLost		= false;
	float lostReloadLevelTimer;
	float lostReloadLevelTimerDuration = 1f;
	

	public float detectionTime = 0.6f;
	public float cameraChance = 0.25f;

	public float lootChance = 0.75f;
	public float highValueLootChance = 0.25f;

	private void Start(){
		var cameras = FindObjectsOfType<NPC_Camera>();
		for (int i = 0; i < cameras.Length; i++) {
			if (cameras[i].isActiveAndEnabled){
				cameras[i].SetCameraNumber(i + 1);
			}
		}
	}

	public void AddThiefEarnings(bool wasMarked, int value)
	{
		if (wasMarked)
		{
			hackerEarnings = hackerEarnings - value;
			sharedEarnings = sharedEarnings + (int)(value / 2f);
		}
		else
		{
			thiefEarnings = thiefEarnings + value;
		}
	}

	public void AddHackerEarnings(int value)
	{
		hackerEarnings = hackerEarnings + value;
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
		int totalSharedEarnings	= sharedEarnings + successEarnings;
		int totalThiefEarnings = thiefEarnings;
		int totalHackerEarnings = hackerEarnings;
		if (wasVictory)
		{
			hackerGameOver.GetComponent<GameOver>().UpdateText(wasVictory, totalSharedEarnings, totalThiefEarnings, totalHackerEarnings);
			thiefGameOver.GetComponent<GameOver>().UpdateText(wasVictory, totalSharedEarnings, totalThiefEarnings, totalHackerEarnings);
		}
		else{
			lostReloadLevelTimer = Time.time + lostReloadLevelTimerDuration;
			hasLost = true;
		}
		HackerCam.transform.position = ThiefCam.transform.position;
		HackerCam.GetComponent<Camera>().orthographicSize = ThiefCam.GetComponent<Camera>().orthographicSize;
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
	