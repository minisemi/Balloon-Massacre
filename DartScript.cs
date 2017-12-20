using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour {
	
	public float speed;
	public float startTime;
	private Rigidbody rigidbody;
	//public static GameObject gameobject = DartScript.gameobject;
	public GameObject explosion;


	// Use this for initialization
	void Start () {
		//Destroy (gameObject, Controller.dartLifespan);
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.velocity = transform.forward * speed;	
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= startTime+2f)
		{
			
			//gameObject.SetActive (false);
			Controller.canFire = true;
			Destroy (gameObject);
		}
		float moveHorizontal = Input.GetAxis ("Vertical");
		float moveVertical = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (-moveVertical, moveHorizontal, 0);

		rigidbody.AddForce (movement * 2);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Balloon"))
		{
			//set color depending on tag "redBalloon" etc. New particle system for each
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			Destroy (gameObject);
			//gameObject.SetActive (false);
			other.gameObject.SetActive (false);
			Controller.count = Controller.count + 1;
			Controller.canFire = true;
		} else if (other.gameObject.CompareTag ( "Bot"))
		{
			Destroy (gameObject);
			//gameObject.SetActive (false);
			//AudioSource ouch = other..GetComponent<AudioSource>();

			//other.gameObject.SetActive (false);
			Controller.count = -1;
		} else if (other.gameObject.CompareTag ( "baloonWall"))
		{
			Destroy (gameObject);
			//gameObject.SetActive (false);
			Controller.canFire = true;

		}
	}
		
}
