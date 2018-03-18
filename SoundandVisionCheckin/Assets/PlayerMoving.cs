using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMoving : MonoBehaviour {
	Animator m_animator;


	public bool obj = false;
	private int countDown = 0;
	public GameObject focus;
	public GameObject focus1;
	Dictionary<int,GameObject[]> dictionaryEnviro=  new Dictionary<int,GameObject[]>();
	Dictionary<int,List<float>> dictionaryEnviroDist = new Dictionary<int,List<float>>();
	Dictionary<int,GameObject> dictionaryHand=  new Dictionary<int,GameObject>();
	public List<float>fList = new List<float>();
	void Start(){
		//Objects in environment
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
		m_animator = GetComponent<Animator> ();
	}
	void Update(){
		print (countDown);
		for (int i = 0; i<dictionaryEnviro.Count;i++){
			List<float>fList = new List<float>();
			for (int h = 0; h < dictionaryEnviro [i].Length; h++) {
				fList.Add (Vector3.Distance (dictionaryEnviro [i] [h].transform.position, transform.position));
			}
			dictionaryEnviroDist.Add (i,fList);
		}
		for (int k = 0; k < dictionaryEnviroDist.Count; k++) {
			for (int l = 0; l<dictionaryEnviroDist[k].Count;l++){
				if (dictionaryEnviroDist [k] [l]<1f && Input.GetKeyDown("space")) {
					obj = true;
					focus = dictionaryEnviro [k][l];
					focus1 = dictionaryHand [k];



				}
				if (obj) {
					if (countDown >= 250) {
						focus1.GetComponent<MeshRenderer> ().enabled = true;
						focus.GetComponent<Renderer> ().enabled = false;
					}
					else if (countDown < 320) {
						countDown += 1;
						m_animator.SetBool ("isEating", true);


					} 
					else if (countDown >= 320) {
						countDown = 0;
						focus1.GetComponent<MeshRenderer> ().enabled = false;
						focus.SetActive (false);
						m_animator.SetBool ("isEating", false);
						obj = false;
					}
				}
			}
		}
		dictionaryEnviroDist.Clear ();
		/*
		 * 
		 * if (ook < 1f && Input.GetKeyDown ("space")) {
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

		 * 
		 * 
		 * 
		 */
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