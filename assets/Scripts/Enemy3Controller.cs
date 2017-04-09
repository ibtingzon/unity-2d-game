using UnityEngine;
using System.Collections;

public class Enemy3Controller : MonoBehaviour {

	public GameObject player;
	public GameObject target;
	public GameObject ammunition;
	public bool move = true;
	Vector2 direction;
	float speed = 2.3f;

	bool playerOnSight = false;
	
	public float attackTimeRepeat = 2f;
	float attackTime;
	float moveTimeRepeat = 2;
	float reduceTimeRepeat = 0.3f;
	float shootingRate = 0.25f;
	
	float reduceTime;
	float moveTime;
	float damage = 2;
	float distance;
	
	float chaseRange = 7.0f;
	bool facingLeft = true;
	
	void Start(){
		gameObject.tag = "Enemy";
		gameObject.AddComponent<AmmoCollision> ();
		gameObject.AddComponent<PauseGame> ();
		direction = new Vector2 (-1, 0);
		moveTime = Time.time;
		Destroy (gameObject, 200);
	}
	
	void Update () {
		distance = Vector2.Distance (player.transform.position, transform.position);
		if(distance < chaseRange){
			playerOnSight = true;
		}
		if (Time.time > attackTime && move) {
			if(playerOnSight)
				spawnAmmo(player);
			else
				spawnAmmo(target);
			attackTime = Time.time + attackTimeRepeat;
		}
		if (Time.time > moveTime && move) {
			if (playerOnSight)
				flipDirection (player);
			else
				flipDirection (target);
			moveTime = Time.time + moveTimeRepeat;
		}
		if(move){
			rigidbody2D.velocity = (direction*speed);
		}
	}

	void setMoveToTrue(){
		move = true;
	}
	
	void flipDirection(GameObject player){
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
		
	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			if (Time.time > reduceTime){
				coll.gameObject.SendMessage("reduceHealth", damage);
				reduceTime = Time.time + reduceTimeRepeat;
			}
		}
	}
	
	public void spawnAmmo(GameObject player){
		Debug.Log ("Shoot");
		GameObject newAmmo= Instantiate(ammunition) as GameObject;
		newAmmo.transform.position = transform.position + new Vector3(0, 1, 0);
		MoveAmmo move = newAmmo.AddComponent<MoveAmmo> ();
		move.direction =  new Vector2(player.transform.position.x -transform.position.x , player.transform.position.y - transform.position.y).normalized;
	}
}
