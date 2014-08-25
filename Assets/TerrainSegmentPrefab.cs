using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainSegmentPrefab : MonoBehaviour {

	public TrackPrefab trackPrefab;

	// Use this for initialization
	void Start () {
	}

	public void init(int numTracks, float portionFilled) {
		List<List<ObstaclePosition>> obstaclePositions = generateObstaclePositions (numTracks, portionFilled);

		for (int n = 0; n < numTracks; n++) {
			TrackPrefab track = (TrackPrefab) Instantiate(trackPrefab);
			bool light = (n == 1);
			track.init(light);
			track.transform.parent = transform;
			track.transform.localPosition = new Vector3 (0f, 0f, 5f * n);
			track.createObstacles (obstaclePositions[n]);
		}
	}

	private List<List<ObstaclePosition>> generateObstaclePositions(int numTracks, float portionFilled) {
		List<List<ObstaclePosition>> tracks = new List<List<ObstaclePosition>> ();
		for (int i = 0; i < numTracks; i++) {
			tracks.Add(new List<ObstaclePosition>());
		}

		List<ObstaclePosition> allPositions = new List<ObstaclePosition> ();
		for (int i = 0; i < 10; i++) {
			float position = i * 10f;
			bool passable = Random.value < 0.75f;
			allPositions.Add(new ObstaclePosition(position, passable));
		}

		// Divide positions between the tracks
		foreach (ObstaclePosition position in allPositions) {
			if (Random.value > portionFilled) continue;
			if (position.passable) {
				foreach (List<ObstaclePosition> track in tracks) {
					track.Add(position);
				}
			} else {
				List<ObstaclePosition> track = tracks[(int) Random.Range(0,numTracks-1)];
				track.Add(position);
			}
		}

		return tracks;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
