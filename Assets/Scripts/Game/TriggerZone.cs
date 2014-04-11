using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour {

	//public types
	public	enum Action {
		SpawnPoint
	}

	//private Unity callbacks
	private	void	OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {

		}
	}
}
