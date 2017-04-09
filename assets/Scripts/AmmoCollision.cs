using UnityEngine;
using System.Collections;

public class AmmoCollision : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Ammo" || coll.gameObject.tag == "PlayerAmmo"){
			if(transform.tag == "Dot" && !(coll.gameObject.tag == "PlayerAmmo")){
				Destroy(coll.gameObject);
				DotScript dot =  transform.gameObject.GetComponent<DotScript>();
				dot.hitDot();
			}
			if((transform.tag == "Player" || transform.tag == "PlayerBazooka") && !(coll.gameObject.tag == "PlayerAmmo")){
				int damage = 3;
				Destroy(coll.gameObject);
				PlayerHealth health = transform.gameObject.GetComponent<PlayerHealth>();
				health.reduceHealth(damage);
			}
			if((transform.tag == "RobotGirl" ) && !(coll.gameObject.tag == "PlayerAmmo")){
				int damage = 1;
				Destroy(coll.gameObject);
				EnemyHealth health = transform.gameObject.GetComponent<EnemyHealth>();
				health.reduceHealth(damage);
			}
			else if(coll.gameObject.tag == "PlayerAmmo" && transform.tag == "Enemy"){
				float damage= 1.5f;
				Destroy(coll.gameObject);
				EnemyHealth health = transform.gameObject.GetComponent<EnemyHealth>();
				health.reduceHealth(damage);
			}
		}
	}
}
