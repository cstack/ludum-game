using UnityEngine;
using System.Collections;

public class InitialObstacles : MonoBehaviour {
	public Wall wallPrefab;

	// Use this for initialization
	void Start () {
		Wall obstacle;;
		for (int i = 0; i < 3; i++) {
			obstacle = (Wall) Instantiate (wallPrefab);
			obstacle.init (i+1);
			obstacle.transform.parent = transform;
			obstacle.transform.localPosition = new Vector3((i+1) * 12f + 4f, 1f, i % 2 == 0 ? 0f : 5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
