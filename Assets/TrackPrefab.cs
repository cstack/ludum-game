using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackPrefab : MonoBehaviour {
	public GameObject[] obstaclePrefabs;

	// Use this for initialization
	void Start () {
	
	}

	public void createObstacles (List<float> positions) {
		foreach (float position in positions) {
			int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
			GameObject prefab = obstaclePrefabs[prefabIndex];
			GameObject obstacle = (GameObject) Instantiate(prefab);
			obstacle.transform.parent = transform;
			obstacle.transform.position = transform.position + new Vector3(position, 1f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
