using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackPrefab : MonoBehaviour {
	public Wall wallPrefab;

	// Use this for initialization
	void Start () {
	
	}

	public void createObstacles (List<float> positions) {
		foreach (float position in positions) {
			Wall obstacle = (Wall) Instantiate(wallPrefab);
			obstacle.init(Random.Range(0, 5));
			obstacle.transform.parent = transform;
			obstacle.transform.position = transform.position + new Vector3(position, 1f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
