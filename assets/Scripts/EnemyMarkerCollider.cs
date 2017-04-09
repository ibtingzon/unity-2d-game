using UnityEngine;
using System.Collections;

public class EnemyMarkerCollider : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.name == "Enemy"){
			coll.gameObject.SendMessage("flipDirection");
		}
	}
}
