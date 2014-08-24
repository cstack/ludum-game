using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public Material fireBackgroundMaterial;
	public Material iceBackgroundMaterial;

	public float frameWidth = 10f;

	// Use this for initialization
	void Start () {
		switchBackgroundImage (true);
	}

	public void switchBackgroundImage (bool inForeground) {
		foreach (Transform child in transform) {
			child.GetComponent<MeshRenderer>().material = inForeground ? fireBackgroundMaterial : iceBackgroundMaterial;
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
