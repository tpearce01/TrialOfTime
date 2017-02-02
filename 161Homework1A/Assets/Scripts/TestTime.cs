using UnityEngine;
using System.Collections;

public class TestTime : MonoBehaviour {
	
	float timer = 0;
	public int moveSpeed;
	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Increment Timer
		timer += Time.deltaTime;

		//Move object
		if ((int)timer % 2 == 0) {
			obj.transform.position = obj.transform.position + transform.up * moveSpeed * Time.deltaTime;
		} else {
			obj.transform.position = obj.transform.position - transform.up * moveSpeed * Time.deltaTime;
		}
	}
}
