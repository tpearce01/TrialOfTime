using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //Vars from wiki.unity3d.com
    public Transform target;	//Player's Transform
    public GameObject player;	//Player GameObject
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .3f;
    public float distanceMax = 15f;

    private Rigidbody _rigidbody;

    float x = 0.0f;
    float y = 0.0f;
    //End

    void Start () {
        //Code from wiki.unity3d.com
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        _rigidbody = GetComponent<Rigidbody>();

        //Initialize camera position
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit))
        {
            distance -= hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position + Vector3.up;
    }
	
	void LateUpdate () {
        //Original code from wiki.unity3d.com, modified by Tyler Pearce
        if (target && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            if (distance < 7 && rotation.eulerAngles.x > 0 && rotation.eulerAngles.x < 180)
            {
                //Debug.Log(rotation.eulerAngles.x);
                distance += 5*Time.deltaTime;
            }

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            if (Input.GetMouseButton(1))
            {
                player.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                transform.position = position + Vector3.up;
                transform.rotation = rotation;
            }
            else
            {
                transform.rotation = rotation;
                transform.position = position + Vector3.up;
            }    
        }
    }


    //Function from wiki.unity3d.com
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
