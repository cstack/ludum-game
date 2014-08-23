using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool grounded = false;
	public float worldDistance = 5f;
	public bool inForeground = true;
	public float speed = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float yVelocity = rigidbody.velocity.y;

		if (Input.GetButtonDown("Jump") && grounded) {
			yVelocity = 10f;
			grounded = false;
		}

		if (Input.GetButtonDown("Fire1")) {
			switchWorlds();
		}

		rigidbody.velocity = new Vector3(speed, yVelocity, 0f);
		updateZPosition (inForeground ? 0f : worldDistance);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "ground") {
			grounded = true;
		}
	}

	void switchWorlds() {
		inForeground = !inForeground;
	}

	void updateZPosition(float pos) {
		transform.position = new Vector3 (transform.position.x, transform.position.y, pos);
	}
}
