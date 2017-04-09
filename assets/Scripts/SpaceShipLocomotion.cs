using UnityEngine;
using System.Collections;

public class SpaceShipLocomotion : MonoBehaviour {

	public Transform player;
	bool move = false;

	void Update () {
		PauseGame pause = player.GetComponent<PauseGame> ();
		if(move && !pause.paused){
			transform.Translate(-0.45f, 0, 0);
		}
	}

	void moveSpaceShip(){
		move = true;
	}

	void haltSpaceShip(){
		move = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			transform.rigidbody2D.isKinematic = true;
		}
	}

}
