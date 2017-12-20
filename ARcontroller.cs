using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ARcontroller : MonoBehaviour {

	private Rigidbody rb;
	public static int count;
	private int ownCount = 0;
	public Text countText;
	public Text timeText;
	public Text winText;
	public GameObject bot;
	private Vector3 botPosition = new Vector3 (20, 0, 1.25f);
	private Quaternion botRotation = Quaternion.Euler(0, -90, 0);
	public static bool canFire = false;
	public Rigidbody shot;
	public Transform shotSpawn;
	public static int dartLifespan = 5;
	private bool gameRunning = false;
	private float gameTime = 30;
	private float frameTime;
	private float lastSpawnTime = 0f;
	public GameObject playField;
	public Transform playFieldSpawn;
	private float startTime = 0f;
	private GameObject clonedPlayField;
	public static GameObject clonedShot;
	public AudioSource ouch;
	public AudioSource pop;
	public AudioSource hooray;
	public GameObject fireworks;
	public GameObject fireworksSpawn;
	public GameObject hazard;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		frameTime = 0;
		/*SetCountText ();
		winText.text = "Shoot 15 balloons to win. You have " + gameTime.ToString() + " seconds! Press Y to play. " +
			"\n Shoot with left mouse button. Curve the shot with arrow keys";
		lastSpawnTime = 0f;*/
		clonedPlayField = Instantiate(playField, playFieldSpawn.position, playFieldSpawn.rotation);



	}

	IEnumerator SpawnWaves ()
	{
		//yield return new WaitForSeconds (1);
		while(gameRunning){
			//for (int i=0; i<5; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range (13.5f, 15), Random.Range (0.3f, 2), 0.65f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (hazard, spawnPosition, spawnRotation);

			//yield return new WaitForSeconds (1);
			//}
			yield return new WaitForSeconds (2);
		}
	}

	// Update is called once per frame
	void Update () {

		/*for (var touch : Touch in Input.touches) {
			if (Touch.phase == TouchPhase.Began) {
				gameObject.GetComponent<Animation>().Play ("Shoot");
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				canFire = false;
			}
		}*/

		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0)
		{
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				//touchOrigin = myTouch.position;
				gameObject.GetComponent<Animation>().Play ("Shoot");
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				canFire = false;
			}
				
		}
		//if (Input.GetButton("Fire1") && canFire)
		/*if (OVRInput.Get(OVRInput.Button.Two) && canFire)
		{
			gameObject.GetComponent<Animation>().Play ("Shoot");
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			canFire = false;
		}

		//if (Input.GetButton("Yes") && !gameRunning)
		if (OVRInput.Get(OVRInput.Button.One) && canFire)
		{
			InstantiateGame ();
		}*/

	}



	void LateUpdate () {
		frameTime = Time.time;

		if (ownCount != count || Time.time >= gameTime && gameRunning){
			if (ownCount != count) {
				pop.Play ();
			}
			SetCountText ();
		}
		if (frameTime <= startTime+gameTime && gameRunning){
			float finalTime = frameTime-startTime;
			timeText.text = finalTime.ToString("f1")+"s";
		}
		/*if (gameRunning){
			int randomNumber = Random.Range(0,30);
			if (randomNumber==1 && frameTime-lastSpawnTime>=0.5f) {
				Instantiate(bot, botPosition, botRotation);
				lastSpawnTime = frameTime;
			}
		}*/
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count == -1) {
			winText.text = "You shot an innocent bystander. You loose! Press Y to play again";
			ouch.Play ();
			gameRunning = false;
			canFire = false;
		} else if (frameTime >= startTime+gameTime) {
			winText.text = "Time's up. You loose! Press Y to play again";
			ouch.Play ();
			gameRunning = false;
			canFire = false;
		} else if (count >= 15) {
			float finalTime = frameTime-startTime;
			GameObject clonedFireworks = Instantiate (fireworks, fireworksSpawn.transform.position, fireworksSpawn.transform.rotation); 
			hooray.Play ();
			winText.text = "You Win! Your time was " + finalTime.ToString("f1") + " seconds. Press Y to play again";
			gameRunning = false;
			canFire = false;
		}
		ownCount = count;
	}

	void InstantiateGame ()
	{
		Destroy (clonedPlayField);
		clonedPlayField = Instantiate(playField, playFieldSpawn.position, playFieldSpawn.rotation);
		count = 0;
		ownCount = 0;
		canFire = true;
		gameRunning = true;
		startTime = Time.time;
		countText.text = "Count: " + count.ToString ();
		//Instantiate(bot, botPosition, botRotation);
		//lastSpawnTime = startTime;
		//StartCoroutine (SpawnWaves());
	}

}
