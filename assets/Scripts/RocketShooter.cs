using UnityEngine;
using System.Collections;

public class RocketShooter : MonoBehaviour {

	public GameObject rocket;
	float shootingRate = 0.25f;
	float shootCoolDown;

	void Start(){
		shootCoolDown = 0f;
	}

	void Update () {
		if (shootCoolDown > 0) 
			shootCoolDown -= Time.deltaTime;
	}

	public bool CanAttack{
		get{
			return shootCoolDown <= 0f;
		}
	}

	public void Attack(bool isEnemy){
		if (CanAttack){
			shootCoolDown = shootingRate;
			GameObject shotTransform = Instantiate(rocket) as GameObject;
			shotTransform.transform.position = transform.position + new Vector3(2, 0, 0);
			MoveRocket move = shotTransform.GetComponent<MoveRocket>();
			if (move != null){
				move.direction = this.transform.right;
				move.shotEnemy = isEnemy; 
			}
		}
	}
}
