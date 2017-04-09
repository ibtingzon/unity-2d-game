using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

	public Transform Message1;
	public Transform Message2;
	public Transform player;
	public Transform bazooka;
	bool collided = false;
	bool paused = false;

	void Start(){
		Message2.renderer.enabled = false;
	}

	void Update(){
		if(Vector2.Distance(player.position, transform.position) > 2)
			Message1.renderer.enabled = false;
		if(!paused && collided && Input.GetKeyDown("e")){
			Message1.renderer.enabled = false;
			if (transform.gameObject.name == "Pencil")
				player.SendMessage("enableDrawing");
			if (transform.gameObject.name == "Eraser")
				player.SendMessage("enableErase");
			if (transform.gameObject.name == "Bazooka"){
				bazooka.SendMessage("enableBazooka", true);
				player.SendMessage("enableBazooka");
			}
			if (!(transform.gameObject.name == "Heart")){
				Message1.renderer.enabled = false;
				Message2.renderer.enabled = true;
				player.SendMessage("pauseGame");
				paused = true;
			}
			else{
				//player.SendMessage("AddHearts", 1);
				player.SendMessage("healPlayer");
			}
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
