using UnityEngine;
using System.Collections;

public class PlaceHolderScript : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Space Ship"){
			coll.gameObject.SendMessage("haltSpaceShip");
		}
	}
}
