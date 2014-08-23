using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	public float frameWidth = 10f;

	// Use this for initialization
	void Start () {
	
	}

	public void switchBackgroundImage () {
		foreach (Transform child in transform) {
			//  change the background image in each panel
		}
	}

	public void shiftPanels () {
		//  Find the farthest back panel
		Transform lastChild = transform.GetChild (0);
		float maxXPosition = lastChild.transform.position.x;
		foreach (Transform child in transform) {
			if (child.position.x < lastChild.position.x) {
				lastChild = child;
			}
			if (child.position.x > maxXPosition) {
				maxXPosition = child.position.x;
			}
		}
		Debug.Log (string.Format ("last child is at {0}", lastChild.transform.position.x));
		Debug.Log (string.Format ("leading child is at {0}", maxXPosition));

		//  Move the last panel
		Transform panelToMove = lastChild;
		float y = panelToMove.transform.position.y;
		float z = panelToMove.transform.position.z;
		panelToMove.transform.position = new Vector3 (maxXPosition + frameWidth, y, z);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
