using UnityEngine;
using System.Collections;

public class BazookaScript : MonoBehaviour {
	
	public GameObject[] players;
	public Camera camera;
	public bool obtainedGun = false;
	GameObject ammunition;
	bool gunOn;

	float ammo = 30;

	void Start(){
		gunOn = false;
	}
	
	void Update () {
		ammunition = GameObject.FindWithTag ("PlayerAmmo");
		if (!obtainedGun && !gunOn)
			transform.renderer.enabled = false;
		else if (gunOn){
			transform.renderer.enabled = true;
			if(Input.GetMouseButtonDown(0)){
				spawnAmmo();
			}
		}
		else{
			transform.renderer.enabled = true;
		}
	}

	public void spawnAmmo(){
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			Player2DController controller = player.GetComponent<Player2DController> ();
			TouchInput input = player.GetComponent<TouchInput>();

			//Rotation
			Vector3 mousePos = Input.mousePosition;
			Vector3 lookPos = camera.ScreenToWorldPoint(mousePos);
			lookPos = lookPos - transform.position;
			float angle  = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;

			//Create Ammo
			GameObject newAmmo= Instantiate(ammunition) as GameObject;
			newAmmo.tag = "PlayerAmmo";
			if(!controller.facingRight  && ( (angle > 100 && angle < 180) || (angle  < -100 && angle > -180) ) ){
				MoveAmmo move = newAmmo.AddComponent<MoveAmmo> ();
				transform.rotation = Quaternion.AngleAxis(-(angle + 180), Vector3.forward);
				newAmmo.transform.position = transform.position + new Vector3(lookPos.x, lookPos.y, 0).normalized;
				move.direction = new Vector3(lookPos.x , lookPos.y).normalized;
			}
			else if(controller.facingRight && (angle < 85 && angle > -85)){
				MoveAmmo move = newAmmo.AddComponent<MoveAmmo> ();
				transform.rotation = Quaternion.AngleAxis((angle), Vector3.forward);
				newAmmo.transform.position = transform.position + new Vector3(lookPos.x, lookPos.y, 0).normalized;
				move.direction = new Vector3(lookPos.x , lookPos.y).normalized;
				Debug.Log (move.direction);
			}
			Destroy (newAmmo, 100);
		}
	}

	public void enableBazooka(bool val){
		obtainedGun = true;
		gunOn = val;
	}
}
