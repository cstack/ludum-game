using UnityEngine;
using System.Collections;

public class TrackPrefab : MonoBehaviour {
	public GameObject[] obstaclePrefabs;

	// Use this for initialization
	void Start () {
	
	}

	public void createObstacles () {
		foreach (GameObject obstaclePrefab in obstaclePrefabs) {
			for (int i = 0; i < 5; i++) {
				int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
				GameObject prefab = obstaclePrefabs[prefabIndex];
				GameObject obstacle = (GameObject) Instantiate(obstaclePrefab);
				obstacle.transform.parent = transform;
				obstacle.transform.position = new Vector3(transform.position.x + i * 10 + Random.Range(-5f, 5f), transform.position.y + 1f, transform.position.z);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
