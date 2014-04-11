using UnityEngine;
using System.Collections;

public class ColorCharacterController : MonoBehaviour {

	//public attributes
	public	GameLevel.GameColor	CurrentColor = GameLevel.GameColor.Default;

	//private attributes
	private	CharacterController2D	Controller2D;
	private	LayerMask	defaultPlatformHit;

	//public methods
	public	void	ChangeColor(GameLevel.GameColor newColor) {
		if (this.CurrentColor == newColor) {
			return;
		}
		switch (newColor) {
		case GameLevel.GameColor.Blue:
			this.SetupPhysicsLayers(GameLevel.Instance.BlueLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask);
			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Blue;
			break;
		case GameLevel.GameColor.Red:
			this.SetupPhysicsLayers(GameLevel.Instance.RedLayerMask, GameLevel.Instance.GreenLayerMask, GameLevel.Instance.BlueLayerMask);
			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Red;
			break;
		case GameLevel.GameColor.Green:
			this.SetupPhysicsLayers(GameLevel.Instance.GreenLayerMask, GameLevel.Instance.RedLayerMask, GameLevel.Instance.BlueLayerMask);
			this.GetComponent<SpriteRenderer>().color = GameLevel.Instance.Green;
			break;
		default:
			//physics
			Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.RedLayerMask, true);
			Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.GreenLayerMask, true);
			Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, GameLevel.Instance.RedLayerMask, true);
			this.Controller2D.platformMask = this.defaultPlatformHit;

			this.GetComponent<SpriteRenderer>().color = Color.white;
			break;
		}
		this.CurrentColor = newColor;
		Runity.Messenger<GameLevel.GameColor>.Broadcast("PlayerChangeColor", this.CurrentColor,
														Runity.MessengerMode.DONT_REQUIRE_LISTENER);
	}


	//private Unity methods
	private	void	Awake() {
		this.Controller2D = this.GetComponent<CharacterController2D>();
		this.defaultPlatformHit = this.Controller2D.platformMask;
	}

	//private methods
	private	void	SetupPhysicsLayers(int newCollisionLayer, int layer1, int layer2) {
		Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, newCollisionLayer, false);
		Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer1, true);
		Physics2D.IgnoreLayerCollision(GameLevel.Instance.PlayerLayerMask, layer2, true);

		//special trick for Controller2D
		//if we change controller, remove this line
		this.Controller2D.platformMask = this.defaultPlatformHit | (1 << newCollisionLayer);
	}


}
