using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCloudRegulator : MonoBehaviour {

	public ParticleSystem abc;
	public int rate = 50;


	void Start()
	{
		abc = GetComponent<ParticleSystem> ();
	}

	void Update () {
		var emission = abc.emission;
		emission.rateOverTime = rate;

	}
}
