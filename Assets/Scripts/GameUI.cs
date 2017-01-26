using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : GenericSingleton<GameUI> {

	public GameObject mainMenu;
	public GameObject deathMenu;
	public GameObject levelCompleteMenu;
	public GameObject gameCompleteMenu;
	public GameObject continueLevelButton;
	public GameObject quitGameButton;

	void OnGUI () {
		if (GameManager.Instance.currentLevel > 0) {
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
		GameManager.Instance.NewGame ();
	}

	public void RetryLevel () {
		GameManager.Instance.RestartLevel ();
	}

	public void LoadNextLevel () {
		GameManager.Instance.LoadNextLevel ();
	}

	public void QuitLevel () {
		GameManager.Instance.LoadMenu ();
	}

	public void QuitGame () {
		GameManager.Instance.QuitGame ();
	}

	public void ResetGame () {
		GameManager.Instance.ResetGame ();
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
