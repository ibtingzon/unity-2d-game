using UnityEngine;

[RequireComponent(typeof(Player2DController))]
public class PlayerController : MonoBehaviour 
{
	private Player2DController character;
    private bool jump;


	void Awake(){
		character = GetComponent<Player2DController>();
	}

    void Update (){
#if CROSS_PLATFORM_INPUT
        if (Input.GetButtonDown("Jump")) jump = true;
#else
		if (Input.GetButtonDown("Jump")) jump = true;
#endif

    }

	void FixedUpdate(){
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		#if CROSS_PLATFORM_INPUT
		float h = Input.GetAxis("Horizontal");
		#else
		float h = Input.GetAxis("Horizontal");
		#endif
		character.Move( h, crouch , jump );

	    jump = false;
	}
}
