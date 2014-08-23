using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
	public float terrainSegmentSize = 100f;

	public Queue<TerrainSegmentPrefab> terrainSegments;
	public TerrainSegmentPrefab terrainSegmentPrefab;
	
	// Use this for initialization
	void Start ()
	{
		terrainSegments = new Queue<TerrainSegmentPrefab> ();
	}
	
	public void loadMoreTerrain(float lowerLeftCorner) {
		TerrainSegmentPrefab segment = (TerrainSegmentPrefab) Instantiate (terrainSegmentPrefab);
		segment.transform.position = new Vector3 (lowerLeftCorner, 0, 0);

		segment.init (2); // TODO dynamically choose this number
		terrainSegments.Enqueue (segment);
	}
	
	public void deleteOldTerrain() {
		Debug.Log (string.Format ("Queue Size = {0}", terrainSegments.Count));
		if (terrainSegments.Count > 2) {
			TerrainSegmentPrefab segment = terrainSegments.Dequeue ();
			Destroy (segment.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

