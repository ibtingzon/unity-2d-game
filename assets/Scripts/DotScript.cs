using UnityEngine;
using System.Collections;

public class DotScript: MonoBehaviour {

	public Transform player;
	int hit;

	void OnMouseOver(){
		TouchInput playerInput = player.GetComponent<TouchInput> ();
		if(Input.GetMouseButton(0) && playerInput.erase){
			Destroy (gameObject);
		}
	}

	void Start(){
		hit = 0;
	}

	void Update(){
		if (hit >= 1)
			Destroy (gameObject);
	}

	public void hitDot(){
		hit += 1;
	}
}
