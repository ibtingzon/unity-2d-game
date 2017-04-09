using UnityEngine;
using System.Collections;

public class MoveRocket : MonoBehaviour {
		
	bool move = true;
	float damage = 1.0f;
	float speed = 25.0f;
	public Vector2 direction = new Vector2 (1,0);
	public bool shotEnemy = false;


	void Start(){
		Destroy (gameObject, 20);
	}
	void Update () {
		rigidbody2D.velocity = (direction*speed);
	}
}
