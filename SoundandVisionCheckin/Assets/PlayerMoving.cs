using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.PostProcessing;

public class PlayerMoving : MonoBehaviour {
	Animator m_animator;
	public PostProcessingProfile ppProfile;
	public AudioSource appleIcon;
	public BloomModel.Settings bloomSettings;
	public GameObject icon;
	public ParticleSystem iconParticles;
	public GameObject trip1;
	public float startSize = 0;
	public GameObject trip1A;
	public GameObject trip1B;
	public AudioMixer mastermixer;
	public float value;
	public bool result;
	public string currState = "";
	public bool obj = false;
	private int countDown = 0;
	ParticleSystem.MainModule sizeMod;
	public GameObject focus;
	private GameObject rustle;
	public GameObject focus1;
	Dictionary<int,GameObject[]> dictionaryEnviro=  new Dictionary<int,GameObject[]>();
	Dictionary<int,List<float>> dictionaryEnviroDist = new Dictionary<int,List<float>>();
	Dictionary<int,GameObject> dictionaryHand=  new Dictionary<int,GameObject>();
	public List<float>fList = new List<float>();
	public List<bool>boolList = new List<bool>();
	public List<bool> otherList = new List<bool> ();
	void Start(){
		//Objects in environment
		iconParticles = icon.GetComponent<ParticleSystem>();
		sizeMod = iconParticles.main;
		print(sizeMod.startSize.constant);
		sizeMod.startSize = new ParticleSystem.MinMaxCurve(5f);
		startSize = sizeMod.startSize.constantMax;
		var col = iconParticles.colorOverLifetime;
		col.enabled = true;
		Gradient grad = new Gradient ();
		grad.SetKeys( new GradientColorKey[] {new GradientColorKey(Color.blue,0.0f), new GradientColorKey(Color.red, 0.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(1.0f,1.0f), new GradientAlphaKey(1.0f,1.0f)});

		col.color = grad;
		bloomSettings = ppProfile.bloom.settings;
		bloomSettings.bloom.intensity = 11f;
		bloomSettings.bloom.softKnee = 0f;
		mastermixer.SetFloat ("volRustle", 6f);
		ppProfile.bloom.settings = bloomSettings;
		trip1.SetActive (false);
		trip1A.SetActive (false);

		result= mastermixer.GetFloat ("volRustle", out value);
		rustle = GameObject.FindGameObjectWithTag("rustle");
		dictionaryEnviro.Add (1,GameObject.FindGameObjectsWithTag("Acorn"));
		dictionaryEnviro.Add (2,GameObject.FindGameObjectsWithTag("Apple"));
		dictionaryEnviro.Add (3,GameObject.FindGameObjectsWithTag("Ax"));
		dictionaryEnviro.Add (4,GameObject.FindGameObjectsWithTag("Bottle"));
		dictionaryEnviro.Add (5,GameObject.FindGameObjectsWithTag("Brick"));
		dictionaryEnviro.Add (6,GameObject.FindGameObjectsWithTag("Horn"));
		dictionaryEnviro.Add (7,GameObject.FindGameObjectsWithTag("Leaf"));
		dictionaryEnviro.Add (8,GameObject.FindGameObjectsWithTag("Mushroom"));
		dictionaryEnviro.Add (9,GameObject.FindGameObjectsWithTag("Pinecone"));
		dictionaryEnviro.Add (10,GameObject.FindGameObjectsWithTag("Rock"));
		dictionaryEnviro.Add (11,GameObject.FindGameObjectsWithTag("Rope"));
		dictionaryEnviro.Add (12,GameObject.FindGameObjectsWithTag("Skull"));
		dictionaryEnviro.Add (13,GameObject.FindGameObjectsWithTag("Stick"));
		dictionaryEnviro.Add (0,GameObject.FindGameObjectsWithTag("Wrapper"));

		//objects in hand
		dictionaryHand.Add (1,GameObject.FindGameObjectWithTag ("AcornHand"));
		dictionaryHand.Add (2,GameObject.FindGameObjectWithTag ("AppleHand"));
		dictionaryHand.Add (3,GameObject.FindGameObjectWithTag ("AxHand"));
		dictionaryHand.Add (4,GameObject.FindGameObjectWithTag ("BottleHand"));
		dictionaryHand.Add (5,GameObject.FindGameObjectWithTag ("BrickHand"));
		dictionaryHand.Add (6,GameObject.FindGameObjectWithTag ("HornHand"));
		dictionaryHand.Add (7,GameObject.FindGameObjectWithTag ("LeafHand"));
		dictionaryHand.Add (8,GameObject.FindGameObjectWithTag ("MushroomHand"));
		dictionaryHand.Add (9,GameObject.FindGameObjectWithTag ("PineconeHand"));
		dictionaryHand.Add (10,GameObject.FindGameObjectWithTag ("RockHand"));
		dictionaryHand.Add (11,GameObject.FindGameObjectWithTag ("RopeHand"));
		dictionaryHand.Add (12,GameObject.FindGameObjectWithTag ("SkullHand"));
		dictionaryHand.Add (13,GameObject.FindGameObjectWithTag ("StickHand"));
		dictionaryHand.Add (0,GameObject.FindGameObjectWithTag ("WrapperHand"));
		for (int j = 0; j < dictionaryHand.Count; j++) {
			dictionaryHand [j].GetComponent<SkinnedMeshRenderer> ().enabled = false;
		}
		for (int j = 0; j < dictionaryHand.Count; j++) {
			boolList.Add( false);
		}
		for (int j = 0; j < dictionaryHand.Count; j++) {
			otherList.Add( true);
		}
		m_animator = GetComponent<Animator> ();
	}
	void Update(){
		for (int i = 0; i<dictionaryEnviro.Count;i++){
			
			List<float>fList = new List<float>();
			for (int h = 0; h < dictionaryEnviro [i].Length; h++) {
				if (dictionaryEnviro [i] [h].activeSelf == true) {
					fList.Add (Vector3.Distance (dictionaryEnviro [i] [h].transform.position, transform.position));
				}
			}
			dictionaryEnviroDist.Add (i,fList);
		}
		for (int k = 0; k < dictionaryEnviroDist.Count; k++) {
			for (int l = 0; l<dictionaryEnviroDist[k].Count;l++){
				if (dictionaryEnviroDist [k] [l]<1f && Input.GetKeyDown("space") && currState == "") {
					obj = true;
					focus = dictionaryEnviro [k][l];
					focus1 = dictionaryHand [k];



				}
			}
		}
		if (obj) {
			countDown += 1;
			if (countDown == 142) {
				focus1.GetComponent<SkinnedMeshRenderer> ().enabled = true;
				focus.GetComponent<MeshRenderer> ().enabled = false;
			}
			if (countDown < 180 && countDown > 0) {

				m_animator.SetBool ("isEating", true);


			} if (countDown >= 180) {
				m_animator.SetBool ("isEating", false);
				focus1.GetComponent<SkinnedMeshRenderer> ().enabled = false;
				focus.SetActive (false);

				obj = false;
				countDown = 0;
				currState = focus.tag;


			}
		} 
		dictionaryEnviroDist.Clear ();

		if (currState == "Apple") {
			if ( value > -80.0f) {
				value -= .1f;
				if (sizeMod.startSize.constantMin >= 0f) {
					startSize -= .01f;
					sizeMod.startSize = new ParticleSystem.MinMaxCurve(startSize);

				}
				if (sizeMod.startSize.constantMin < 0f) {
					startSize = 0f;
					sizeMod.startSize = new ParticleSystem.MinMaxCurve(startSize);
				}
			}
			mastermixer.SetFloat ("volRustle", value);
			if (value < -30.0f) {
				boolList [0] = true;
				otherList [0] = false;
				value = -58f;


			}
			if (!otherList[0] && boolList [0] == true) {
				trip1.SetActive (true);
				AudioSource audioSource = trip1.GetComponent<AudioSource> ();
				AudioSource audioSource2 = trip1A.GetComponent<AudioSource>();
				if (audioSource.time > 1) {
					trip1A.SetActive (true);



				}
				if (audioSource2.isPlaying){
					bloomSettings.bloom.intensity += .1f;
					bloomSettings.bloom.softKnee += .001f;

					if (audioSource.time % 5 == 0) {
						AudioSource audioSource3 = trip1B.GetComponent<AudioSource> ();
						audioSource3.Play();
						Debug.Log("D"+audioSource3.isPlaying);
					}
				}
				if (!audioSource2.isPlaying && bloomSettings.bloom.intensity > 11 && value < 6f){
					bloomSettings.bloom.intensity -= 1f;
					bloomSettings.bloom.softKnee -= .01f;
					if (bloomSettings.bloom.softKnee < 0f) {
						bloomSettings.bloom.softKnee = 0f;
					}
						currState = "";
						trip1.SetActive (false);
						trip1A.SetActive (false);
						trip1B.SetActive (false);

						if (value < 6f) {
							value += .1f;
							mastermixer.SetFloat ("volRustle", value);
							appleIcon.volume = 0;


						}
	

					
				}

				ppProfile.bloom.settings = bloomSettings;
			


			}
	}
		if (currState == ""){
			GameObject fogGenerator = GameObject.FindGameObjectWithTag ("point");
			float dist = Vector3.Distance (fogGenerator.transform.position, transform.position);
			print (dist);
			if (dist > 210) {
				bloomSettings.bloom.intensity = (dist-210)*4;
				bloomSettings.bloom.softKnee = (dist-210);


			}
			if (dist <= 210) {
				bloomSettings.bloom.intensity = 0;
				bloomSettings.bloom.softKnee = 0;
			}

			if (dist > 225) {
				if ((transform.localEulerAngles.y > 315 && transform.localEulerAngles.y <= 360) || (transform.localEulerAngles.y >= 0 && transform.localEulerAngles.y < 45) &&transform.position.y > GameObject.Find("north").transform.position.y ){
					transform.position = GameObject.Find ("south").transform.position;
				}
				if ((transform.localEulerAngles.y > 45 && transform.localEulerAngles.y <= 90) || (transform.localEulerAngles.y >= 90 && transform.localEulerAngles.y < 135) &&transform.position.y > GameObject.Find("east").transform.position.z ){
					transform.position = GameObject.Find ("west").transform.position;
				}
				if ((transform.localEulerAngles.y > 135 && transform.localEulerAngles.y <= 180) || (transform.localEulerAngles.y >= 180 && transform.localEulerAngles.y < 225) &&transform.position.y > GameObject.Find("south").transform.position.y ){
					transform.position = GameObject.Find ("north").transform.position;
				}
				if ((transform.localEulerAngles.y > 225 && transform.localEulerAngles.y <= 270) || (transform.localEulerAngles.y >= 270 && transform.localEulerAngles.y < 315) &&transform.position.y > GameObject.Find("west").transform.position.z ){
					transform.position = GameObject.Find ("east").transform.position;
				}
			}
			ppProfile.bloom.settings = bloomSettings;
		}

}
}


/*
	public bool obj = false;

	public GameObject[] listEnviro;

	public HandData han = new <List> ();

	public GameObject[] acornEnviro;
	public GameObject acornHand;
	public GameObject[] appleEnviro;
	public GameObject appleHand;
	public GameObject[] axEnviro;
	public GameObject axHand;
	public GameObject[] bottleEnviro;
	public GameObject bottleHand;
	public GameObject[] brickEnviro;
	public GameObject brickHand;
	public GameObject[] hornEnviro;
	public GameObject hornHand;
	public GameObject[] leafEnviro;
	public GameObject leafHand;
	public GameObject[] mushroomEnviro;
	public GameObject mushroomHand;
	public GameObject[] pineconeEnviro;
	public GameObject pineconeHand;
	public GameObject[] rockEnviro;
	public GameObject rockHand;
	public GameObject[] ropeEnviro;
	public GameObject ropeHand;
	public GameObject[] skullEnviro;
	public GameObject skullHand;
	public GameObject[] stickEnviro;
	public GameObject stickHand;
	public GameObject[] wrapperEnviro;
	public GameObject wrapperHand;
	public GameObject focus;
	public int countDown = 0;
	// Use this for initialization
	void Start () {
		print (listEnviro);
		listEnviro.Add (GameObject.FindGameObjectsWithTag("Acorn"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Apple"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Ax"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Bottle"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Brick"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Horn"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Leaf"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Mushroom"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Pinecone"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Rock"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Rope"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Skull"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Stick"));
		envir.listEnviro.Add (GameObject.FindGameObjectsWithTag("Wrapper"));


		han.listHand.Add (GameObject.FindGameObjectWithTag ("AcornHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("AppleHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("AxHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("BottleHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("BrickHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("HornHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("LeafHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("MushroomHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("PineconeHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("RockHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("RopeHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("SkullHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("StickHand"));
		han.listHand.Add (GameObject.FindGameObjectWithTag ("WrapperHand"));

	}
	
	// Update is called once per frame
	void Update () {
		focus = listEnviro[0][0];

		for (int o = 0; o < envir.listEnviro.Count; o++) {
			print (envir.listEnviro[o]);

			for (int i = 0; i < envir.listEnviro[o].Length; i++) {
				float ook = Vector3.Distance (envir.listEnviro[o][i].transform.position, transform.position);
				if (ook < 1f && Input.GetKeyDown ("space")) {
					print (ook);
					obj = true;
					focus = envir.listEnviro[o][i];
					han.listHand[o].GetComponent<Renderer> ().enabled = true;
					focus.GetComponent<Renderer> ().enabled = false;
				}
				if (obj) {

					if (countDown < 100) {
						countDown += 1;
						print (countDown);
					} else {
						countDown = 0;
						obj = false;
						focus.SetActive (false);
						han.listHand[o].GetComponent<Renderer> ().enabled = false;
					}
				}

			}
		}

	}
}
*/	

/*
* 
* 
* using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnviroData{
	public List<GameObject[]> listEnviro; 
}


[System.Serializable]
public struct HandData{
	public List<GameObject> listHand;
}

public class PlayerMoving : MonoBehaviour {
	public bool obj = false;
	EnviroData envir = new EnviroData ();
	HandData han = new HandData ();
	public List<EnviroData> Enviro = new List <EnviroData>();

	public List<HandData> Hand = new List <HandData> ();

	public GameObject[] acornEnviro;
	public GameObject acornHand;
	public GameObject[] appleEnviro;
	public GameObject appleHand;
	public GameObject[] axEnviro;
	public GameObject axHand;
	public GameObject[] bottleEnviro;
	public GameObject bottleHand;
	public GameObject[] brickEnviro;
	public GameObject brickHand;
	public GameObject[] hornEnviro;
	public GameObject hornHand;
	public GameObject[] leafEnviro;
	public GameObject leafHand;
	public GameObject[] mushroomEnviro;
	public GameObject mushroomHand;
	public GameObject[] pineconeEnviro;
	public GameObject pineconeHand;
	public GameObject[] rockEnviro;
	public GameObject rockHand;
	public GameObject[] ropeEnviro;
	public GameObject ropeHand;
	public GameObject[] skullEnviro;
	public GameObject skullHand;
	public GameObject[] stickEnviro;
	public GameObject stickHand;
	public GameObject[] wrapperEnviro;
	public GameObject wrapperHand;
	public GameObject focus;
	public int countDown = 0;
	// Use this for initialization
	void Start () {
		acornEnviro = GameObject.FindGameObjectsWithTag("Acorn");
		acornHand = GameObject.FindGameObjectWithTag ("AcornHand");
		appleEnviro = GameObject.FindGameObjectsWithTag("Apple");
		appleHand = GameObject.FindGameObjectWithTag ("AppleHand");
		axEnviro = GameObject.FindGameObjectsWithTag("Ax");
		axHand = GameObject.FindGameObjectWithTag ("AxHand");
		bottleEnviro = GameObject.FindGameObjectsWithTag("Bottle");
		bottleHand = GameObject.FindGameObjectWithTag ("BottleHand");
		brickEnviro = GameObject.FindGameObjectsWithTag("Brick");
		brickHand = GameObject.FindGameObjectWithTag ("BrickHand");
		hornEnviro = GameObject.FindGameObjectsWithTag("Horn");
		hornHand = GameObject.FindGameObjectWithTag ("HornHand");
		leafEnviro = GameObject.FindGameObjectsWithTag("Leaf");
		leafHand = GameObject.FindGameObjectWithTag ("LeafHand");
		mushroomEnviro = GameObject.FindGameObjectsWithTag("Mushroom");
		mushroomHand = GameObject.FindGameObjectWithTag ("MushroomHand");
		pineconeEnviro = GameObject.FindGameObjectsWithTag("Pinecone");
		pineconeHand = GameObject.FindGameObjectWithTag ("PineconeHand");
		rockEnviro = GameObject.FindGameObjectsWithTag("Rock");
		rockHand = GameObject.FindGameObjectWithTag ("RockHand");
		ropeEnviro = GameObject.FindGameObjectsWithTag("Rope");
		ropeHand = GameObject.FindGameObjectWithTag ("RopeHand");
		skullEnviro = GameObject.FindGameObjectsWithTag("Skull");
		skullHand = GameObject.FindGameObjectWithTag ("SkullHand");
		stickEnviro = GameObject.FindGameObjectsWithTag("Stick");
		stickHand = GameObject.FindGameObjectWithTag ("StickHand");
		wrapperEnviro = GameObject.FindGameObjectsWithTag("Wrapper");
		wrapperHand = GameObject.FindGameObjectWithTag ("WrapperHand");



		envir.listEnviro.Add (acornEnviro);
		envir.listEnviro.Add (appleEnviro);
		envir.listEnviro.Add (axEnviro);
		envir.listEnviro.Add (bottleEnviro);
		envir.listEnviro.Add (brickEnviro);
		envir.listEnviro.Add (hornEnviro);
		envir.listEnviro.Add (leafEnviro);
		envir.listEnviro.Add (mushroomEnviro);
		envir.listEnviro.Add (pineconeEnviro);
		envir.listEnviro.Add (rockEnviro);
		envir.listEnviro.Add (ropeEnviro);
		envir.listEnviro.Add (skullEnviro);
		envir.listEnviro.Add (stickEnviro);
		envir.listEnviro.Add (wrapperEnviro);


		han.listHand.Add (acornHand);
		han.listHand.Add (appleHand);
		han.listHand.Add (axHand);
		han.listHand.Add (bottleHand);
		han.listHand.Add (brickHand);
		han.listHand.Add (hornHand);
		han.listHand.Add (leafHand);
		han.listHand.Add (mushroomHand);
		han.listHand.Add (pineconeHand);
		han.listHand.Add (rockHand);
		han.listHand.Add (ropeHand);
		han.listHand.Add (skullHand);
		han.listHand.Add (stickHand);
		han.listHand.Add (wrapperHand);
		appleHand.GetComponent<Renderer> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {

		for (int o = 0; o < envir.listEnviro.Count; o++) {
			print (envir.listEnviro[o]);

			for (int i = 0; i < envir.listEnviro[o].Length; i++) {
				float ook = Vector3.Distance (envir.listEnviro[o][i].transform.position, transform.position);
				if (ook < 1f && Input.GetKeyDown ("space")) {
					print (ook);
					obj = true;
					focus = envir.listEnviro[o][i];
					han.listHand[o].GetComponent<Renderer> ().enabled = true;
					focus.GetComponent<Renderer> ().enabled = false;
				}
				if (obj) {

					if (countDown < 100) {
						countDown += 1;
						print (countDown);
					} else {
						countDown = 0;
						obj = false;
						focus.SetActive (false);
						han.listHand[o].GetComponent<Renderer> ().enabled = false;
					}
				}

			}
		}
	}
}




*/