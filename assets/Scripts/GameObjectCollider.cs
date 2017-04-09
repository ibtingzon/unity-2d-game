using UnityEngine;
using System.Collections;

public class GameObjectCollider : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Space Ship" ){
			if(transform.gameObject.name == "GameEnder"){
				if(Application.loadedLevelName == "Level1")
					Application.LoadLevel("Level2");
				else if(Application.loadedLevelName == "Level2")
					Application.LoadLevel("Level2point1");
				else if(Application.loadedLevelName == "Level2point1")
					Application.LoadLevel("MainMenu");
			}
			else if(transform.gameObject.name == "GameOverCollider"){
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
