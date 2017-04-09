using UnityEngine;
using System.Collections;

public class SpaceShipController : MonoBehaviour {

	bool collide = false;
	public bool moveSpaceShip = false;
	float speed = 10;
	float acceleration = 30;
	Vector3 targetSpeed;
	Vector3 currentSpeed;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			//Temporarily disable player properties
			coll.gameObject.transform.parent = transform.parent;
			coll.gameObject.transform.GetComponent<Animator>().enabled = false;
			coll.gameObject.GetComponent<PlayerController>().enabled = false;
			coll.gameObject.GetComponent<Player2DController>().enabled = false;
			coll.gameObject.GetComponent<BoxCollider2D>().enabled = true;
			coll.gameObject.GetComponent<CircleCollider2D>().enabled = false;
			coll.gameObject.rigidbody2D.isKinematic = false;
			Destroy(coll.gameObject.rigidbody2D);
			collide = true;
		}
	}

	void Update(){
		if (collide){
			Transform spaceShip = transform.parent;
			spaceShip.rigidbody2D.isKinematic = false;
			if(!moveSpaceShip && Input.GetAxisRaw("Horizontal") > 0){
				moveSpaceShip = true;
			}
			if (moveSpaceShip && Input.GetAxisRaw("Horizontal") == 0){
				spaceShip.Translate(new Vector3(0.5f, 0, 0));
			}
			if (moveSpaceShip && Input.GetKeyDown("space")){
				RocketShooter newRocket = GetComponent<RocketShooter>();
				newRocket.Attack(false);
			}
			targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, /*Input.GetAxisRaw("Vertical") * speed*/ 0, 0);
			currentSpeed = new Vector3(IncrementTowards(currentSpeed.x, targetSpeed.x, acceleration), IncrementTowards(currentSpeed.y, targetSpeed.y, acceleration), 0);
			spaceShip.Translate (currentSpeed * Time.deltaTime);
		}
	}

	private float IncrementTowards(float currentSpeed, float targetSpeed, float alpha) {
		if (currentSpeed == targetSpeed) 
			return currentSpeed;
		else {
			float direction = Mathf.Sign(targetSpeed - currentSpeed); 
			currentSpeed += alpha * Time.deltaTime * direction;
			return (direction == Mathf.Sign(targetSpeed - currentSpeed))? currentSpeed: targetSpeed; 
		}
	}

}
