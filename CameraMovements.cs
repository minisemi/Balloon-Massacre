using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour {

	private float sensitivity = 3;
	public float maxYAngle = 80;
	private Vector3 offset;
	private GameObject player;

	private Vector2 currentRotation;

	void Update()
	{
		/*switch (Controller.canFire){
		case true:*/
			currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
			currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
			currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
			currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
			Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x+180,0);
			if (Input.GetMouseButtonDown(0))
				Cursor.lockState = CursorLockMode.Locked;
			/*break;
		case false:
			player = DartScript.gameobject;
			offset = transform.position - player.transform.position;
			transform.position = player.transform.position + offset;
			break;
		}*/

	}
}
