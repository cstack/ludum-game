using UnityEngine;
using System.Collections;

public class InitialObstacles : MonoBehaviour {
	public Wall wallPrefab;

	// Use this for initialization
	void Start () {
		Wall obstacle;;
		for (int i = 0; i < 4; i++) {
			obstacle = (Wall) Instantiate (wallPrefab);
			obstacle.init (i+1);
			obstacle.transform.parent = transform;
			obstacle.transform.localPosition = new Vector3((i+1) * 15f, 1f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
