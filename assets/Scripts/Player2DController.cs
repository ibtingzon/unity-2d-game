using UnityEngine;

public class Player2DController : MonoBehaviour 
{
	public bool facingRight = true;							

	[SerializeField] float maxSpeed = 10f;				
	[SerializeField] float jumpForce = 400f;				

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			
	
	[SerializeField] bool airControl = false;			
	[SerializeField] LayerMask whatIsGround;			
	
	Transform groundCheck;								
	float groundedRadius = .2f;							
	bool grounded = false;								
	Transform ceilingCheck;								
	float ceilingRadius = .01f;							
	Animator anim;									


    void Awake(){
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}


	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
	}


	public void Move(float move, bool crouch, bool jump){

		if(!crouch && anim.GetBool("Crouch")){
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		anim.SetBool("Crouch", crouch);
		if(grounded || airControl){
			move = (crouch ? move * crouchSpeed : move);
			anim.SetFloat("Speed", Mathf.Abs(move));
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			if(move > 0 && !facingRight)
				Flip();
			else if(move < 0 && facingRight)
				Flip();
		}
        if (grounded && jump) {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
	}

	
	public void Flip (){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
