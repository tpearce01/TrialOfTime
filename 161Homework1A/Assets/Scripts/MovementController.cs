using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
	public static MovementController i;

	public Animator animations;
	public GameObject model;

    public float moveSpeed;
	public float jumpHeight;
    public Rigidbody rigidbody;
	bool isAirborne = false;
	bool isMoving = false;
	public bool isDead = false;


	void Awake(){
		i = this;
	}

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
		//Determine if player should be considered airborne
		if (rigidbody.velocity.y < -0.5f) {
			isAirborne = true;
		}

		if (!isDead) {
			//Get user input and move player
			MovePlayer ();


			if (!isMoving && !isAirborne) {
				if (Time.timeScale > 0.03f) {
					Time.timeScale -= Mathf.Clamp(Time.deltaTime * 3, .005f, 1.0f);		//Slow time
					Time.fixedDeltaTime = .02f * Time.timeScale;//Decrease time scale
				}
			} else {
				if (Time.timeScale < 1.0f) {
					Time.timeScale += Mathf.Clamp(Time.deltaTime * 3, .005f, 1.0f);		//Speed time
					Time.fixedDeltaTime = .02f * Time.timeScale;//Increase time scale
				}
			}
		}
	}

	//Get player input and move player is applicable
    public void MovePlayer()
    {
		//Is the player moving?
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.Space)) {
			isMoving = true;
			animations.SetFloat ("Speed", 1);
		} 
		else if(Input.GetKey (KeyCode.E)){
			isMoving = true;
			animations.SetFloat ("Speed", 0);
		}else {
			isMoving = false;
			animations.SetFloat ("Speed", 0);
		}

		if (!isAirborne) {
			//Get movement input and apply transformation

			//Move forward
			if (Input.GetKey (KeyCode.W) || (Input.GetMouseButton (0) && Input.GetMouseButton (1))) {
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
					rigidbody.MovePosition (rigidbody.transform.position + transform.forward * moveSpeed * Time.deltaTime * 0.7f);

				} else {
					rigidbody.MovePosition (rigidbody.transform.position + transform.forward * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,0,0));

				}
			}

			//Move backward
			if (Input.GetKey (KeyCode.S)) {
				rigidbody.MovePosition (rigidbody.transform.position - transform.forward * moveSpeed * Time.deltaTime * 0.7f);
				model.transform.localRotation = Quaternion.Euler (new Vector3 (0,180,0));

			}

			//Move left
			if (Input.GetKey (KeyCode.A)) {
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
					if (Input.GetKey (KeyCode.W)) {
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f + transform.forward* moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-45,0));
					}
					else if(Input.GetKey(KeyCode.S)){
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f - transform.forward* moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-135,0));
					}
					else{
						rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-90,0));

					}
				} else {
					rigidbody.MovePosition (rigidbody.transform.position - transform.right * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,-90,0));

				}
			}

			//Move right
			if (Input.GetKey (KeyCode.D)) {
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
					if (Input.GetKey (KeyCode.W)) {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f + transform.forward * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,45,0));

					} else if (Input.GetKey (KeyCode.S)) {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f - transform.forward * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,135,0));

					} else {
						rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime * 0.7f);
						model.transform.localRotation = Quaternion.Euler (new Vector3 (0,90,0));

					}
				} else {
					rigidbody.MovePosition (rigidbody.transform.position + transform.right * moveSpeed * Time.deltaTime);
					model.transform.localRotation = Quaternion.Euler (new Vector3 (0,90,0));

				}
			}
		}

		//Jump
		if (Input.GetKey (KeyCode.Space)) {
			
			if (!isAirborne) {
				rigidbody.AddForce (transform.up * jumpHeight);

				if(Input.GetKey(KeyCode.W)){
					rigidbody.AddForce (transform.forward * jumpHeight * 1.5f);
				}
				if(Input.GetKey(KeyCode.A)){
					rigidbody.AddForce (-transform.right * jumpHeight);
				}
				if(Input.GetKey(KeyCode.S)){
					rigidbody.AddForce (-transform.forward * jumpHeight);
				}
				if(Input.GetKey(KeyCode.D)){
					rigidbody.AddForce (transform.right * jumpHeight);
				}
			}
			animations.SetBool ("Jump", true);
		}

    }

	//Reset airborne boolean
	void OnCollisionEnter(Collision other){
		
		Debug.Log ("Collision: " + other.gameObject.tag);
		if(other.gameObject.CompareTag("floor")){
			isAirborne = false;
		}
	}

	void OnCollisionStay(Collision other){
		Debug.Log ("Stay: " + other.gameObject.tag);
		if(other.gameObject.CompareTag("floor")){
			animations.SetBool ("Jump", false);

			isAirborne = false;
			rigidbody.WakeUp ();
		}
	}

	void OnCollisionExit(Collision other){
		Debug.Log ("Exit: " + other.gameObject.tag);
		if(other.gameObject.CompareTag("floor")){
			isAirborne = true;

		}

	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Trigger: " + other.gameObject.tag);
	}

    
}
