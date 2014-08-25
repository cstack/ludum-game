using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool grounded = false;
	public float worldDistance = 5f;
	public bool inForeground = true;
	public bool switchedWorlds;
	public float initialSpeed = 1f;
	public float speed;
	public float jumpSpeed = 30f;
	public float nextTerrainUpdate = 0f;
	public float nextBackgroundUpdate = 10f;
	public bool dead = false;

	public TerrainGenerator generator;
	public BackgroundManager backgroundManagerPrefab;
	BackgroundManager backgroundManager;
	public SpriteRenderer shadow;
	public SpriteRenderer ballSprite;

	public float balanceValue = 0f; // The value of how "light/dark" the ball is
	float maxBalanceValue = 5f;

	public AudioSource deathSound;
	public AudioSource jumpSound;
	public AudioSource toLightSound;
	public AudioSource toDarkSound;

	// Use this for initialization
	void Start () {
		backgroundManager = (BackgroundManager) Instantiate (backgroundManagerPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			return;
		}
		float yVelocity = rigidbody.velocity.y;

		//  Process User inputs
		if (Input.GetButtonDown("Jump") && grounded) {
			yVelocity = jumpSpeed;
			grounded = false;
			jumpSound.Play ();
		}

		if (Input.GetButtonUp("Jump") && yVelocity > 0) {
			yVelocity *= 0.2f;
		}
		if (Input.GetButtonDown("Fire1")) {
			switchWorlds();
		}

		//  Update player properties
		speed += speedDerivative (Time.timeSinceLevelLoad) * Time.deltaTime;
		rigidbody.velocity = new Vector3(speed, yVelocity, 0f);

		if (inForeground) {
			balanceValue += Time.deltaTime;
		} else {
			balanceValue -= Time.deltaTime;
		}
		if (Mathf.Abs (balanceValue) >= maxBalanceValue) {
			die ();
			return;
		}

		//  Update Environment
		if (transform.position.x > nextTerrainUpdate) {
			generator.loadMoreTerrain(nextTerrainUpdate + generator.terrainSegmentSize);
			generator.deleteOldTerrain();
			nextTerrainUpdate = nextTerrainUpdate + generator.terrainSegmentSize;
		}
		/*if (transform.position.x > nextBackgroundUpdate) {
			backgroundManager.shiftPanels();
			nextBackgroundUpdate = nextBackgroundUpdate + backgroundManager.frameWidth;
		}*/

		//  Visual update Player and associated objects
		updateZPosition (inForeground ? 0f : worldDistance);
		updateShadow (inForeground ? worldDistance : -worldDistance);
		updateBalanceShading ();

		switchedWorlds = false; //  Allow hit sound again
	}

	void updateBalanceShading () {
		if (balanceValue >= 0f) {
			float alpha = 1f - (balanceValue / maxBalanceValue); // [0.0, 1.0]
			ballSprite.color = new Color (1f, 1f, 1f, alpha);
		} else {
			float darkness = 1f - (-balanceValue / maxBalanceValue);
			ballSprite.color = new Color (darkness, darkness, darkness, 1f);
		}
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
			if (normal.x < 0f && (Mathf.Abs (normal.x) > Mathf.Abs (normal.y))) {
				die ();
				return; // Don't play landed sound
			} else {
				// Collided on top
				grounded = true;
			}
		}
	}

	void die () {
		dead = true;
		deathSound.Play ();
		StartCoroutine (deathAnimation ());
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
		switchedWorlds = true;
		if (inForeground) {
			toLightSound.Play ();
		} else {
			toDarkSound.Play ();
		}
		gameObject.layer = inForeground ? LayerMask.NameToLayer ("Plane1") : LayerMask.NameToLayer ("Plane2");
		backgroundManager.switchBackgroundImage (inForeground);
	}

	void updateZPosition(float pos) {
		transform.position = new Vector3 (transform.position.x, transform.position.y, pos);
	}
	void updateShadow(float pos) {
		shadow.transform.localPosition = new Vector3 (0, 0, pos);
		if (switchedWorlds) {
			if (inForeground) {
				shadow.color = new Color (0f, 0f, 0f, 0.25f);
			} else {
				shadow.color = new Color (1f, 1f, 1f, 0.25f);
			}
		}
	}
}
