using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBalloon : MonoBehaviour {

	// Use this for initialization
	void Update () {
		transform.Translate (new Vector3 (0,0,0.05f));
	}

}
