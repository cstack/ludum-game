using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {
	public Sprite lightSprite;
	public Sprite darkSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(bool light) {
		GetComponent<SpriteRenderer> ().sprite = light ? lightSprite : darkSprite;
	}
}
