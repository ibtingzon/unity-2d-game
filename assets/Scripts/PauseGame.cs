using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public bool paused = false;

	void Update () {
		if (Input.GetKeyDown(KeyCode.P) && paused == false) {
			Time.timeScale = 0.0f;
			paused = true; 
		}
		else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.F)) && paused == true){
			Time.timeScale = 1.0f;
			paused = false; 
		}
	}
	void pauseGame(){
		Time.timeScale = 0.0f;
		paused = true; 
	}

	void unPauseGame(){
		Time.timeScale = 1.0f;
		paused = false; 
	}
}
