using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

	public static GameUI instance;

	public GameObject mainMenu;
	public GameObject deathMenu;
	public GameObject levelCompleteMenu;
	public GameObject gameCompleteMenu;
	public GameObject continueLevelButton;
	public GameObject quitGameButton;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void OnGUI () {
		if (GameManager.instance.currentLevel > 0) {
			continueLevelButton.SetActive (true);
		} else {
			continueLevelButton.SetActive (false);
		}

		#if UNITY_EDITOR || UNITY_STANDALONE
		quitGameButton.SetActive (true);
		#else
		quitGameButton.SetActive (false);
		#endif
	}

	// Things buttons can do
	public void NewGame () {
		GameManager.instance.NewGame ();
	}

	public void RetryLevel () {
		GameManager.instance.RestartLevel ();
	}

	public void LoadNextLevel () {
		GameManager.instance.LoadNextLevel ();
	}

	public void QuitLevel () {
		GameManager.instance.LoadMenu ();
	}

	public void QuitGame () {
		GameManager.instance.QuitGame ();
	}

	public void ResetGame () {
		GameManager.instance.ResetGame ();
	}

	// Menu handlers
	public void DisplayMainMenu () {
		HideAllMenus ();
		mainMenu.SetActive (true);
	}

	public void DisplayDeathMenu () {
		HideAllMenus ();
		deathMenu.SetActive (true);
	}

	public void DisplayLevelCompleteMenu () {
		HideAllMenus ();
		levelCompleteMenu.SetActive (true);
	}

	public void DisplayGameCompleteMenu () {
		HideAllMenus ();
		gameCompleteMenu.SetActive (true);
	}

	public void HideAllMenus () {
		mainMenu.SetActive (false);
		deathMenu.SetActive (false);
		levelCompleteMenu.SetActive (false);
		gameCompleteMenu.SetActive (false);
	}

}
