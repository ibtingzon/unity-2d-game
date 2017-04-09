using UnityEngine;
using System.Collections;

public class StartEnemySpawn : MonoBehaviour {

	public Transform enemy;

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			enemy.SendMessage("setStartSpawn", true);
			Destroy(gameObject);
		}
	}
}
