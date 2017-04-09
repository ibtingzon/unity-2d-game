using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	void OnMouseUp(){
		Debug.Log ("Play Pressed");
		if (transform.gameObject.name == "Play")
			Application.LoadLevel("Level1");
		else
			Application.Quit();
	}
}
