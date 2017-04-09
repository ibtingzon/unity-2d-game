using UnityEngine;
using System.Collections;

public class Enemy2Controller : MonoBehaviour {

	GameObject player;
	GameObject ammunition;
	Vector2 direction;
	public bool move = false;
	float speed = 3.5f;

	public float attackTimeRepeat = 2f;
	float attackTime;
	float moveTimeRepeat = 2;
	float reduceTimeRepeat = 0.3f;
	float shootingRate = 0.25f;

	float reduceTime;
	float moveTime;
	float damage = 2;
	float distance;

	float chaseRange = 15.0f;
	bool facingLeft = true;
	
	void Start(){
		gameObject.tag = "Enemy";
		gameObject.AddComponent<EnemyHealth> ();
		gameObject.AddComponent<AmmoCollision> ();
		gameObject.AddComponent<PauseGame> ();
		player = GameObject.FindWithTag ("Player");
		ammunition = GameObject.FindWithTag ("Ammo");
		direction = new Vector2 (-1, 0);
		moveTime = Time.time;
		Destroy (gameObject, 200);
	}
	
	void Update () {
		distance = Vector2.Distance (player.transform.position, transform.position);
		if (Time.time > attackTime && move && distance < chaseRange) {
			spawnAmmo();
			attackTime = Time.time + attackTimeRepeat;
		}
		if (Time.time > moveTime && move) {
			flipDirection ();
			moveTime = Time.time + moveTimeRepeat;
		}
		if(move){
			rigidbody2D.velocity = (direction*speed);
		}
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

	void setMoveToTrue(){
		move = true;
	}

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			if (Time.time > reduceTime){
				coll.gameObject.SendMessage("reduceHealth", damage);
				reduceTime = Time.time + reduceTimeRepeat;
			}
		}
	}

	public void spawnAmmo(){
		Debug.Log ("Shoot");
		GameObject newAmmo= Instantiate(ammunition) as GameObject;
		newAmmo.transform.position = transform.position + new Vector3(0, 1, 0);
		MoveAmmo move = newAmmo.AddComponent<MoveAmmo> ();
		//if(facingLeft)
		//	move.direction = new Vector2(-1,0);
		//else
			//move.direction = new Vector2(1, 0);
		move.direction =  new Vector2(player.transform.position.x -transform.position.x , player.transform.position.y - transform.position.y).normalized;
	}
}
