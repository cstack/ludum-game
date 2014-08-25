using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public GroundTile blockPrefab;
	public int height;

	// Use this for initialization
	void Start () {
	}

	public void init(int h, bool light) {
		height = h;

		for (int n = 0; n < height; n++) {
			GroundTile block = (GroundTile) Instantiate(blockPrefab);
			block.init(light);
			block.transform.parent = transform;
			block.transform.localPosition = new Vector3 (0f, 1f * n, 0f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
