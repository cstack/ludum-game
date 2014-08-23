using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;
	private float zPos;

	// Use this for initialization
	void Start () {
		zPos = player.transform.position.z - 10;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float dP = (player.transform.position.z - 10) - transform.position.z;
		zPos += dP * 0.2f;
		transform.position = new Vector3 (player.transform.position.x + 7, 2, zPos);
	}
}
