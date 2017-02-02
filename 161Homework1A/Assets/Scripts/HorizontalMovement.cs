using UnityEngine;
using System.Collections;

public class HorizontalMovement : MonoBehaviour {

	float startTimer;
	float timer = 0;
	public int moveSpeed;
	public GameObject obj;

	// Use this for initialization
	void Start () {
		startTimer = Random.Range (1, 30) / 10f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Increment Timer
		if (startTimer > 0) {
			startTimer -= Time.deltaTime;
		} else {
			timer += Time.deltaTime;

			//Move object
			if ((int)timer % 2 == 0) {
				obj.transform.position = obj.transform.position + transform.right * moveSpeed * Time.deltaTime;
			} else {
				obj.transform.position = obj.transform.position - transform.right * moveSpeed * Time.deltaTime;
			}
		}
	}

}
