using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 10;
	public float currHealth;
	private GUIStyle currentStyle;
	float healTimeRepeat = 2.0f;
	float healTime;

	void Start () {
		currHealth = maxHealth;
	}

	void Update () {
		if(currHealth <= 0)
			Destroy(gameObject);
		if(transform.gameObject.name == "RobotGirl"){
			if (currHealth <= 0){
				Application.LoadLevel(Application.loadedLevelName);
			}
		    if (Time.time > healTime ){
				currHealth += 0.2f;
				healTime = Time.time + healTimeRepeat;
			}
		}
	}

	public void reduceHealth(float damage){
		currHealth -= damage;
	}

	void OnGUI(){
		if (currHealth == 0)
			return;
		InitStyles ();
		float healthBarLength = 9.0f;
		float healthBarWidth = 50.0f;
		if (transform.gameObject.name == "Enemy" || transform.gameObject.name == "RobotGirl") {
			healthBarWidth = 90.0f;
		}
		Vector3 position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
		Vector3 screenPosition =  Camera.main.WorldToScreenPoint(position);
		screenPosition.y = Screen.height - screenPosition.y - 1; 
		GUI.Box (new Rect(screenPosition.x, screenPosition.y, healthBarWidth, healthBarLength), "");
		GUI.Box (new Rect(screenPosition.x, screenPosition.y, (currHealth/maxHealth)*healthBarWidth, healthBarLength), "Enemy", currentStyle);
	}
	
	private void InitStyles(){
		currentStyle = new GUIStyle( GUI.skin.box );
		if(currHealth > maxHealth/3)
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 1f, 0f, 0.5f ) );
		else
			currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 0.5f ) );
	}
	
	private Texture2D MakeTex( int width, int height, Color col ){
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i ){
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

}
