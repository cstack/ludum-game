using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public GUIElement gui;
	public Player player;
	int score = 0;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		gui.guiText.text = "Score: " + score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.dead) {
			score = (int) Time.timeSinceLevelLoad;
		}
	}
}
