using UnityEngine;
using System.Collections;

public class FallDeath : MonoBehaviour {

	public GameObject deathPlane;
	public GameObject player;

	
	// Update is called once per frame
	void Update () {
		//Maintain y position, but follow player x and y positions
		deathPlane.transform.position = new Vector3 (player.transform.position.x, deathPlane.transform.position.y, player.transform.position.z);
	}

	void OnCollisionEnter(Collision other){
		Debug.Log ("DeathCollision: " + other.gameObject.tag);
		if (other.gameObject.CompareTag ("player")) {
			HUDManager.i.modifyHealth (-100);
		}
	}
}
