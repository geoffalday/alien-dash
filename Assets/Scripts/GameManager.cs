using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager> {

	[HideInInspector] public bool gameOver;
	[HideInInspector] public bool levelComplete;
	[HideInInspector] public int currentLevel;

	void Start () {
		gameOver = true;
		levelComplete = false;

		if (PlayerPrefs.GetInt ("currentLevel") > 0) {
			currentLevel = PlayerPrefs.GetInt ("currentLevel");
		} else {
			currentLevel = 0;
			SaveGame ();
		}

		GameUI.Instance.DisplayMainMenu ();
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
