using UnityEngine;
using System.Collections;

public class TerrainSegmentPrefab : MonoBehaviour {

	public TrackPrefab trackPrefab;

	// Use this for initialization
	void Start () {
	
	}

	public void init(int numTracks) {
		for (int n = 0; n < numTracks; n++) {
			TrackPrefab track = (TrackPrefab) Instantiate(trackPrefab);
			track.transform.parent = transform;
			track.transform.localPosition = new Vector3 (0f, 0f, 5f * n);
			track.createObstacles ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
