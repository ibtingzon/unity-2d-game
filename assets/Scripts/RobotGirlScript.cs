using UnityEngine;
using System.Collections;

public class RobotGirlScript : MonoBehaviour {

	public GameObject player;
	bool facingLeft = false;
	Vector2 direction;

	void Update () {
		flipDirection ();
	}
	
	void flipDirection(){
		if ((player.transform.position.x > transform.position.x && facingLeft) || (player.transform.position.x < transform.position.x && !facingLeft)) {
			Vector3 newScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
			transform.localScale = newScale;
			if (facingLeft)
				facingLeft = false;
			else
				facingLeft = true;
		}
		direction = direction*-1;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" ){
			Application.LoadLevel("MainMenu");
		}
	}

}
