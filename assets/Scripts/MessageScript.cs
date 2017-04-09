using UnityEngine;
using System.Collections;

public class MessageScript : MonoBehaviour {

	public Transform player;

	void Update () {
		if (Input.GetKeyDown("f") && transform.renderer.enabled) {
			transform.renderer.enabled = false;
		}
	}
}
