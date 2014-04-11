using UnityEngine;
using System.Collections;

public class GameLevel : MonoBehaviour {

	//static attributes
	private	static	GameLevel	_instance = null;
	public	static	GameLevel	Instance {
		get {
			if (GameLevel._instance == null) {
				GameLevel._instance = new GameLevel();
			}
			return GameLevel._instance;
		}
	}
	//public types
	public	enum GameColor {
		Red,
		Green,
		Blue,
		Default
	}

	//public attributes
	public	Color	Red;
	public	Color	Green;
	public	Color	Blue;

	
	public	string	RedLayerName = "Physics_Red";
	public	string	GreenLayerName = "Physics_Green";
	public	string	BlueLayerName = "Physics_Blue";
	public	string	PlayerLayerName = "Player";

	//private attributes
	public	int		RedLayerMask {get; private set;}
	public	int		GreenLayerMask {get; private set;}
	public	int		BlueLayerMask {get; private set;}
	public	int		PlayerLayerMask {get; private set;}

	//private Unity methods 
	private	void	Awake() {
		this.RedLayerMask = LayerMask.NameToLayer(this.RedLayerName);
		this.GreenLayerMask = LayerMask.NameToLayer(this.GreenLayerName);
		this.BlueLayerMask = LayerMask.NameToLayer(this.BlueLayerName);
		this.PlayerLayerMask = LayerMask.NameToLayer(this.PlayerLayerName);

		GameLevel._instance = this;
		Runity.Messenger.Reset();
		Runity.Messenger<GameColor>.Reset();
		Runity.Messenger<string>.Reset();
	}
}
