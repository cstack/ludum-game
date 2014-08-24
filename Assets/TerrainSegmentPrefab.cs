using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainSegmentPrefab : MonoBehaviour {

	public TrackPrefab trackPrefab;

	// Use this for initialization
	void Start () {
	
	}

	public void init(int numTracks) {
		List<List<ObstaclePosition>> obstaclePositions = generateObstaclePositions (numTracks);

		for (int n = 0; n < numTracks; n++) {
			TrackPrefab track = (TrackPrefab) Instantiate(trackPrefab);
			track.transform.parent = transform;
			track.transform.localPosition = new Vector3 (0f, 0f, 5f * n);
			track.createObstacles (obstaclePositions[n]);
		}
	}

	private List<List<ObstaclePosition>> generateObstaclePositions(int numTracks) {
		List<List<ObstaclePosition>> positions = new List<List<ObstaclePosition>> ();
		for (int i = 0; i < numTracks; i++) {
			positions.Add(new List<ObstaclePosition>());
		}

		List<ObstaclePosition> allPositions = new List<ObstaclePosition> ();
		for (int i = 0; i < 10; i++) {
			float position = i * 10f + Random.Range(-3f, 3f);
			bool passable = Random.value < 0.75f;
			allPositions.Add(new ObstaclePosition(position, passable));
		}

		// Divide positions between the tracks
		int track = 0;
		for (int i = 0; i < allPositions.Count; i++) {
			positions[track++ % numTracks].Add(allPositions[i]);
		}

		return positions;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
