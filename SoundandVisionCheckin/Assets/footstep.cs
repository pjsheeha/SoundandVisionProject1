using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class footstep : MonoBehaviour {
	public AudioClip clip;
	private AudioSource a;
	private bool once = false;
	private const float timerReset = 0.18f;
	// Use this for initialization

	void Awake () {
		a = GetComponent<AudioSource> ();
	}
	void Start () {
		
	}


	void onTriggerEnter(Collider col) {
		
		string currTag = col.tag;

		print (currTag);
		if (!once && currTag == "terrain") {
			a.PlayOneShot (clip);
			once = true;
			Invoke ("Reset", timerReset);
		}

	}

	private void Reset() {
		once = false;
	}
}
