using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GrassPointCloud : MonoBehaviour {
	
	//public Mesh grassMesh;
	//public Material material;
	private Mesh mesh;
	public MeshFilter filter;
	public int seed;
	public Vector2 size;
	[Range(1,60000)]
	public int grassNumber;
	public float startHeight = 1000;
	public float grassOffset = 0.0f;
	private Vector3 lastPosition;
	// Use this for initialization
	private List<Matrix4x4> matrices;

	// Update is called once per frame
	void Update () {
		if (lastPosition != this.transform.position) {
			Random.InitState (seed);
			List<Vector3> positions = new List<Vector3> (grassNumber);
			int[] indices = new int [grassNumber];
			List<Color> colors = new List<Color> (grassNumber); 
			List<Vector3> normals = new List<Vector3> (grassNumber);
			for (int i = 0; i < grassNumber; ++i) {
				Vector3 origin = transform.position;
				origin.y = startHeight;
				origin.x += size.x * Random.Range (-0.5f, 0.5f);
				origin.z += size.y * Random.Range (-0.5f, 0.5f);
				Ray ray = new Ray (origin, Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					origin = hit.point;
					origin.y += grassOffset;

					positions.Add (origin);
					indices [i] = i;
					colors.Add (new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1));
					normals.Add (hit.normal);

				}
			}
				mesh = new Mesh ();
				mesh.SetVertices (positions);
				mesh.SetIndices (indices,MeshTopology.Points,0);
				mesh.SetColors (colors);
				mesh.SetNormals (normals);
				filter.mesh = mesh;
				lastPosition = this.transform.position;

		}

		//Graphics.DrawMeshInstanced (grassMesh, 0, material, matrices);
	}
}
