using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool grounded = false;
	public float worldDistance = 5f;
	public bool inForeground = true;
	public float speed = 1f;
	public float jumpSpeed = 30f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float yVelocity = rigidbody2D.velocity.y;

		if (Input.GetButtonDown("Jump") && grounded) {
			yVelocity = jumpSpeed;
			grounded = false;
		}

		if (Input.GetButtonDown("Fire1")) {
			switchWorlds();
		}

		rigidbody2D.velocity = new Vector3(speed, yVelocity, 0f);
		updateZPosition (inForeground ? 0f : worldDistance);
	}

	void OnCollisionEnter2D(Collision2D collision) {
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
