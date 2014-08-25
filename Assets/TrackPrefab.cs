using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackPrefab : MonoBehaviour {
	public Wall wallPrefab;

	// Use this for initialization
	void Start () {
	
	}

	public void createObstacles (List<ObstaclePosition> positions) {
		foreach (ObstaclePosition position in positions) {
			generateObstacle(position);
		}
	}

	private void generateObstacle(ObstaclePosition position) {
		Wall obstacle = (Wall)Instantiate (wallPrefab);
		int height = position.passable ? Random.Range (1, 3) : Random.Range (4, 10);
		obstacle.init (height);
		obstacle.transform.parent = transform;
		obstacle.transform.position = transform.position + new Vector3 (position.xPos + Random.Range(-3f, 3f), 1f, 0f);
	}
		
		// Update is called once per frame
	void Update () {
	
	}
}
