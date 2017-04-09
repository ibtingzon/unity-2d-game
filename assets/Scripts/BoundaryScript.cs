using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour {

	public Transform spaceShip;
	public Texture2D warningImage;
	bool warning = false;

	void Update(){
		if (Vector2.Distance (spaceShip.position, transform.position) < 5)
			warning = true;
		else
			warning = false;
	}

	void OnGUI(){
		if (warning) {
			float size = 160.0f;
			GUI.BeginGroup (new Rect (Screen.width/3, Screen.height/2, size , size));
			GUI.Label(new Rect (0,0, size, size), warningImage);
			GUI.EndGroup ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.name == "Space Ship" ){
			warning = true;
		}
	}
}
