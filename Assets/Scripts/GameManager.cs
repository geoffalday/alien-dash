using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : GenericSingleton<GameManager> {

	[HideInInspector] public bool gameOver;
	[HideInInspector] public bool levelComplete;
	[HideInInspector] public int currentLevel;

	void Start () {
		gameOver = true;
		levelComplete = false;

		#if UNITY_EDITOR
		// Play from level started on in editor
		// If started from a scene other than menu...
		if (SceneManager.GetActiveScene().buildIndex > 0) {
			// Set currentLevel to level started from in editor
			currentLevel = SceneManager.GetActiveScene().buildIndex;
			// Save currentLevel in prefs
			SaveGame();
			// Set gameOver to false so camera will follow
			gameOver = false;
		} else {
			// Otherwise, do things the normal way
			if (PlayerPrefs.GetInt ("currentLevel") > 0) {
				currentLevel = PlayerPrefs.GetInt ("currentLevel");
			} else {
				currentLevel = 0;
				SaveGame ();
			}

			GameUI.Instance.DisplayMainMenu ();
		}
		#else
		if (PlayerPrefs.GetInt ("currentLevel") > 0) {
			currentLevel = PlayerPrefs.GetInt ("currentLevel");
		} else {
			currentLevel = 0;
			SaveGame ();
		}

		GameUI.Instance.DisplayMainMenu ();
		#endif
	}

	public void NewGame () {
		GameUI.Instance.HideAllMenus ();
		currentLevel = 1;
		SaveGame ();
		gameOver = false;
		levelComplete = false;
		SceneManager.LoadScene (currentLevel);
	}

	public void RestartLevel () {
		GameUI.Instance.HideAllMenus ();
		gameOver = false;
		levelComplete = false;
		SceneManager.LoadScene (currentLevel);
	}

	public void LoadMenu () {
		GameUI.Instance.HideAllMenus ();
		gameOver = true;
		levelComplete = false;
		SceneManager.LoadScene ("Menu");
		GameUI.Instance.DisplayMainMenu ();
	}

	public void PlayerDied () {
		gameOver = true;
		levelComplete = false;
		GameUI.Instance.DisplayDeathMenu ();
	}

	public void LevelCompleted () {
		gameOver = false;
		levelComplete = true;

		// log level completed in analytics
		string level_id = "Level" + currentLevel.ToString();
		Analytics.CustomEvent("LevelCompleted", new Dictionary<string, object> {
			{ "level_id", level_id }
		});
			
		if (currentLevel < SceneManager.sceneCountInBuildSettings - 2) {
			currentLevel = currentLevel + 1;
			SaveGame ();
			GameUI.Instance.DisplayLevelCompleteMenu ();
		} else {
			currentLevel = 0;
			SaveGame ();
			GameUI.Instance.DisplayGameCompleteMenu ();
		}
	}

	public void LoadNextLevel () {
		GameUI.Instance.HideAllMenus ();
		gameOver = false;
		levelComplete = false;
		SceneManager.LoadScene (currentLevel);
	}

	public void ResetGame () {
		GameUI.Instance.HideAllMenus ();
		gameOver = true;
		levelComplete = false;
		currentLevel = 0;
		SaveGame ();
		SceneManager.LoadScene (currentLevel);
		GameUI.Instance.DisplayMainMenu ();
	}

	void SaveGame () {
		PlayerPrefs.SetInt ("currentLevel", currentLevel);
	}

	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
		Application.Quit ();
		#endif
	}

}
