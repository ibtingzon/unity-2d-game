using UnityEngine;
using System.Collections;

public class Enemy1Controller : MonoBehaviour {
	
	public GameObject spawn;
	public int maxSpawns;

	GameObject player;
	Vector2 direction;
	bool move = true;
	float speed = 6.5f;
	int spawnCounter = 0;
	float attackTimeRepeat = 5.3f;
	float attackTime;
	float shootingRate = 0.25f;
	bool startSpawn = false;
	float chaseRange = 15.0f;
	float playerDistance;

	void Start(){
		player = GameObject.FindWithTag ("Player");
		direction = new Vector2 (-1, 0);
		attackTime = Time.time;
		Destroy (gameObject, 200);
	}

	void Update () {
		if(startSpawn){
			playerDistance = Vector2.Distance (player.transform.position, transform.position);
			if (Time.time > attackTime && spawnCounter < maxSpawns && playerDistance < chaseRange) {
				spawnEnemy ();
				attackTime = Time.time + attackTimeRepeat;
				spawnCounter += 1;
			}
			rigidbody2D.velocity = (direction*speed);
		}
	}
	
	public void spawnEnemy(){
		GameObject newEnemy= Instantiate(spawn) as GameObject;
		newEnemy.transform.position = transform.position + new Vector3(0, -2, 0);
	}

	void flipDirection(){
		direction = direction*-1;
		Vector3 newScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
		transform.localScale = newScale;
	}
	
	void setStartSpawn(bool val){
		startSpawn = val;
	}
}