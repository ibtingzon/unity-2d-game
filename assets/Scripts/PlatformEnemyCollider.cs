using UnityEngine;
using System.Collections;

public class PlatformEnemyCollider : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Enemy"){
			Debug.Log ("Enemy Collide");
			coll.gameObject.SendMessage("setMoveToTrue");
		}
	}
}
