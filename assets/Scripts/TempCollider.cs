using UnityEngine;
using System.Collections;

public class TempCollider : MonoBehaviour {

	public Transform Message3;
	public Transform player;
	public Camera camera;
	bool paused = false;
	
	void Start(){
		Message3.renderer.enabled = false;
	}
		
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && !paused){
			paused = true;
			Message3.renderer.enabled = true;
			player.SendMessage("pauseGame");
		}
	}
}
