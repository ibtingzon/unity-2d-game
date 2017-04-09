using UnityEngine;
using System.Collections;

public class SpaceShipCaller : MonoBehaviour {

	public Transform Message1;
	public Transform spaceShip;
	public Transform player;
	bool collided = false;
	bool paused = false;
	bool summoned = false;
	
	void Update(){
		if(Vector2.Distance(player.position, transform.position) > 2 || summoned)
			Message1.renderer.enabled = false;
		if(!summoned && collided && Input.GetKeyDown("f")){
			spaceShip.SendMessage("moveSpaceShip");
			Message1.renderer.enabled = false;
			summoned = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			Message1.renderer.enabled = true;
			collided = true;
		}
	}
}
