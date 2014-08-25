using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool grounded = false;
	public float worldDistance = 5f;
	public bool inForeground = true;
	public float initialSpeed = 1f;
	public float speed;
	public float jumpSpeed = 30f;
	public float nextTerrainUpdate = 0f;
	public float nextBackgroundUpdate = 10f;
	public bool dead = false;

	public TerrainGenerator generator;
	public BackgroundManager backgroundManagerPrefab;
	BackgroundManager backgroundManager;

	// Use this for initialization
	void Start () {
		backgroundManager = (BackgroundManager) Instantiate (backgroundManagerPrefab);
		speed = initialSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			return;
		}
		float yVelocity = rigidbody.velocity.y;

		if (Input.GetButtonDown("Jump") && grounded) {
			yVelocity = jumpSpeed;
			grounded = false;
		}

		if (Input.GetButtonUp("Jump")) {
			yVelocity = 0f;
		}

		if (Input.GetButtonDown("Fire1")) {
			switchWorlds();
		}

		speed += speedDerivative (Time.timeSinceLevelLoad) * Time.deltaTime;

		rigidbody.velocity = new Vector3(speed, yVelocity, 0f);

		if (transform.position.x > nextTerrainUpdate) {
			generator.loadMoreTerrain(nextTerrainUpdate + generator.terrainSegmentSize);
			generator.deleteOldTerrain();
			nextTerrainUpdate = nextTerrainUpdate + generator.terrainSegmentSize;
		}

		if (transform.position.x > nextBackgroundUpdate) {
			backgroundManager.shiftPanels();
			nextBackgroundUpdate = nextBackgroundUpdate + backgroundManager.frameWidth;
		}

		updateZPosition (inForeground ? 0f : worldDistance);
	}

	float speedDerivative (float t) {
		//return 0.1f * t * Mathf.Exp (-t / 10f);
		return 0.2f;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "ground") {
			grounded = true;
		} else if (collision.gameObject.tag == "obstacle") {
			Vector3 normal = collision.contacts[0].normal;
			if (normal.x < 0f) {
				// Collided on side
				dead = true;
				StartCoroutine(deathAnimation());
			} else {
				// Collided on top
				grounded = true;
			}

		}
	}

	IEnumerator deathAnimation() {
		GameObject sprite = transform.FindChild ("Sprite").gameObject;
		Animator animator = sprite.GetComponent<Animator> ();
		animator.SetTrigger ("die");
		gameObject.layer = LayerMask.NameToLayer ("NoInteraction");
		rigidbody.velocity = Vector3.zero;
		rigidbody.useGravity = false;
		yield return new WaitForSeconds(0.5f);
		rigidbody.useGravity = true;
		rigidbody.velocity = new Vector3 (0f, jumpSpeed, 0f);
		yield return new WaitForSeconds (3f);
		Application.LoadLevel(Application.loadedLevel);
	}

	void switchWorlds() {
		inForeground = !inForeground;
		gameObject.layer = inForeground ? LayerMask.NameToLayer ("Plane1") : LayerMask.NameToLayer ("Plane2");
		backgroundManager.switchBackgroundImage (inForeground);
	}

	void updateZPosition(float pos) {
		transform.position = new Vector3 (transform.position.x, transform.position.y, pos);
	}
}
