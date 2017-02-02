using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	GameObject player;
	public float baseLocation;
	public float dropTimer; 

	void Start(){
		player = GameObject.FindGameObjectWithTag ("player");
		//Drop timer to be 0s for base location
		dropTimer = Random.Range (0, 1);
	}

	void FixedUpdate(){
		
	}


}
