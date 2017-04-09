using UnityEngine;
using System.Collections;

public class MoveAmmo : MonoBehaviour {

	bool move = true;
	float damage = 1.0f;
	float targetSpeed = 50.0f;
	float currentSpeed = 10.0f;
	public float acceleration = 20;
	public Vector2 direction = new Vector2 (1,0);
	public bool shotEnemy = false;

	void Start(){
		Destroy (gameObject, 20);
	}
	void Update () {
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		//rigidbody2D.velocity = (direction*currentSpeed);
		Vector3 newDirection = new Vector3 (direction.x, direction.y, 0);
		transform.position += currentSpeed * newDirection * Time.deltaTime;
	}

	private float IncrementTowards(float currentSpeed, float targetSpeed, float alpha) 
	{
		if (currentSpeed == targetSpeed) 
			return currentSpeed;
		else {
			float direction = Mathf.Sign(targetSpeed - currentSpeed); 
			currentSpeed += alpha * Time.deltaTime * direction;
			return (direction == Mathf.Sign(targetSpeed - currentSpeed))? currentSpeed: targetSpeed; 
		}
	}

}
