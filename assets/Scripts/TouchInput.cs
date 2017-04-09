using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class TouchInput : MonoBehaviour {

	LineRenderer lineRenderer;
	ArrayList linePoints = new ArrayList();
	float threshold = 0.001f;
	float startWidth = 1.0f;
	float endWidth = 1.0f;
	int lineCount = 0;
	
	public GUITexture pencilGUI;
	public GameObject Dot;
	public GameObject Bazooka;
	public Camera camera;
	Vector3 lastPosition;	
	bool lastPointExists;

	public bool draw = false;
	public bool erase = false;
	public bool bazooka = false;
	public bool obtainedEraser = false;
	public bool obtainedBazooka = false;
	float pencilLead;
	float maxLead;

	public Texture2D pencilMode;
	public Texture2D eraserMode;
	public Texture2D pencilMode1;
	public Texture2D eraserMode1;
	public Texture2D bazookaMode;
	public Texture2D forward;
	public Texture2D back;
	public Texture2D jump;
	public Texture2D X;
	private GUIStyle currentStyle;
	public ArrayList dots = new ArrayList();
	BazookaScript newBazooka;

	void Start(){
		newBazooka = Bazooka.GetComponent<BazookaScript>();
		Input.simulateMouseWithTouches = true;
		lineRenderer = GetComponent<LineRenderer>();
		lastPointExists = false;
		maxLead = 100.0f;
		pencilLead = maxLead;
	}

	void Update () {
		//Debug.Log (Application.loadedLevelName);
		if (Input.GetMouseButton(0) && draw && pencilLead > 0) {
			Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
			Vector3 newDotPosition = mouseRay.origin - mouseRay.direction / mouseRay.direction.z * mouseRay.origin.z;
			if (newDotPosition != lastPosition){
				MakeADot(newDotPosition);
				//drawLine(lastPosition, newDotPosition);
			}
		}
		foreach (Touch touch in Input.touches){
			Ray touchRay = camera.ScreenPointToRay(touch.position);
			Vector3 newDotPosition = touchRay.origin - touchRay.direction / touchRay.direction.z * touchRay.origin.z;
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled){
				MakeADot(newDotPosition);
			}
		}
		if (Input.GetKeyDown("q") && obtainedEraser && obtainedBazooka) {
			if(draw){
				draw = false;
				erase = true;
				bazooka = false;
				newBazooka.enableBazooka(false);
			}
			else if (erase){
				erase = false;
				bazooka = true;
				draw = false;
				newBazooka.enableBazooka(true);
			}
			else if (obtainedBazooka && bazooka){
				erase = false;
				draw = true;
				bazooka = false;
				newBazooka.enableBazooka(false);
			}
		}
		else if (Input.GetKeyDown("q") && obtainedEraser){
			if(draw){
				Debug.Log ("Erase");
				draw = false;
				erase = true;
			}
			else if (erase){
				Debug.Log ("Draw");
				erase = false;
				draw = true;
			}
		}
	}

	void drawLine(Vector3 lastDotPosition, Vector3 newDotPosition){
		GameObject line = new GameObject();
		LineRenderer lineRenderer = line.AddComponent<LineRenderer> ();
		float dist = Vector3.Distance(lastDotPosition, newDotPosition);
		if(dist <= threshold)
			return;

		lastDotPosition = newDotPosition;
		if(linePoints == null)
			linePoints = new ArrayList();
		linePoints.Add(newDotPosition);
		
		lineRenderer.SetWidth(startWidth, endWidth);
		lineRenderer.SetVertexCount(linePoints.Count);

		if(linePoints.Count > 0){
			for(int i = lineCount; i < linePoints.Count; i++){
				lineRenderer.SetPosition(i, (Vector3)linePoints[i]);
			}
			lineCount = linePoints.Count;
		}
	}
	
	void MakeADot(Vector3 newDotPosition){
		GameObject dot = Instantiate(Dot, newDotPosition, Quaternion.identity) as GameObject; 
		dots.Add (dot);
		if (lastPointExists)
			dot.transform.position = Vector3.Lerp(newDotPosition, lastPosition, 0.1f);
		lastPosition = newDotPosition;
		lastPointExists = true;
		pencilLead -= 0.85f;
		Destroy (dot, 200);
	}
		
	void OnGUI(){
		float btnSize = 200.0f;
		GUI.Label (new Rect (Screen.width - 100, 5*Screen.height/6 -150, btnSize, btnSize), jump);
		GUI.Label (new Rect (Screen.width - 300, 5*Screen.height/6 + 10, btnSize, btnSize), X);
		GUI.BeginGroup (new Rect (85, 4*Screen.height/5, btnSize, btnSize));
		GUI.Label(new Rect (100,0, btnSize, btnSize), forward);
		GUI.Label(new Rect (0,0, btnSize, btnSize), back);
		GUI.EndGroup ();
		if (!draw && !erase && !bazooka)
			return;
		InitStyles ();
		float BarLength = 30.0f;
		Vector2 screenPosition = new Vector2 (Screen.width/3 + 50, 5);
		GUI.Box (new Rect(screenPosition.x, screenPosition.y, 400, BarLength), "");
		if(pencilLead > 0){
			if (pencilLead > maxLead){
				pencilLead = maxLead;
			}
			GUI.Box (new Rect(screenPosition.x, screenPosition.y, (pencilLead/maxLead)*400, BarLength), "", currentStyle);
		}
		if (obtainedEraser) {
			float size = 200.0f;
			GUI.BeginGroup (new Rect (5*Screen.width/6, 2*Screen.height/3, size , size));
			if(!obtainedBazooka){
				if (draw)
					GUI.Label(new Rect (0,0, size, size), pencilMode);
				else if(erase)
					GUI.Label(new Rect (0,0, size, size), eraserMode);
			}
			else{
				if (draw)
					GUI.Label(new Rect (0,0, size, size), pencilMode1);
				else if(erase)
					GUI.Label(new Rect (0,0, size, size), eraserMode1);
				else if(bazooka)
					GUI.Label(new Rect (0,0, size, size), bazookaMode);
			}
			GUI.EndGroup ();
		}
	}

	private void InitStyles(){
		currentStyle = new GUIStyle( GUI.skin.box );
		currentStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 0.55f, 1f, 0.5f ) );
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

	void getLead(){
		pencilLead += 100;
	}

	void enableDrawing(){
		draw = true;
		erase = false;
	}

	void enableErase(){
		obtainedEraser = true;
	}

	void enableBazooka(){
		obtainedBazooka = true;
		bazooka = true;
		draw = false;
		erase = false;
	}
	
}
