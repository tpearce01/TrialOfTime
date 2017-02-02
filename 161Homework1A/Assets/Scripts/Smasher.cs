using UnityEngine;
using System.Collections;

public class Smasher : MonoBehaviour {
	float startYPos;
	public int force;
	public Rigidbody rgbd;
	bool recovering = false;
	bool recoveringp2 = false;
	float startTimer;

	// Use this for initialization
	void Start () {
		rgbd.useGravity = false;
		startTimer = Random.Range (1, 30) / 10f;
		startYPos = rgbd.gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (startTimer > 0) {
			startTimer -= Time.deltaTime;
		} else {
			rgbd.useGravity = true;
			//If not recovering, slam down
			if (!recovering) {
				rgbd.AddForce (-transform.up * force);
				recovering = true;
			}
			//Return to original position when in recovering p2
			else if (recovering && recoveringp2) {
				rgbd.AddForce (transform.up * force);
			}
			//Once original position is reached, reset booleans
			if (rgbd.gameObject.transform.position.y >= startYPos) {
				recovering = false;
				recoveringp2 = false;
			}
		}
	}

	//When floor is reached, enter recovering p2 
	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("floor")) {
			recoveringp2 = true;
		}
		if(other.gameObject.CompareTag("player")){
			Physics.IgnoreCollision (gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
			HUDManager.i.modifyHealth (-30f);
		}
	}
}
