using UnityEngine;
using System.Collections;

public class LeadCollider : MonoBehaviour {

	public Transform Message1;
	public Transform player;
	bool collided = false;
	bool paused = false;

	void Update(){
		if(Vector2.Distance(player.position, transform.position) > 2)
			Message1.renderer.enabled = false;
		if(collided && Input.GetKeyDown("e")){
			player.SendMessage("getLead");
			Message1.renderer.enabled = false;
			Destroy (gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			Message1.renderer.enabled = true;
			collided = true;
		}
	}
}
