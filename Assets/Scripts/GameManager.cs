using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager> {

	[HideInInspector] public bool gameOver;
	[HideInInspector] public int currentLevel;
	[HideInInspector] public bool levelComplete;

	void Start () {
		levelComplete = false;
		currentLevel = SceneManager.GetActiveScene ().buildIndex;
	}

	void Update () {
		if (currentLevel == 0) {
			GameUI.Instance.DisplayMainMenu ();
		}
	}

	public void NewGame () {
		GameUI.Instance.HideAllMenus ();
		currentLevel = 1;
		gameOver = false;
		SceneManager.LoadScene (currentLevel);
	}

	public void RestartLevel () {
		gameOver = false;
		GameUI.Instance.HideAllMenus ();
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
		GameUI.Instance.DisplayDeathMenu ();
	}

	public void LevelCompleted () {
		levelComplete = true;

		if (currentLevel < SceneManager.sceneCountInBuildSettings - 1) {
			GameUI.Instance.DisplayLevelCompleteMenu ();
		} else {
			GameUI.Instance.DisplayGameCompleteMenu ();
		}
	}

	public void LoadNextLevel () {
		levelComplete = false;
		GameUI.Instance.HideAllMenus ();

		int nextLevel = currentLevel + 1;
		SceneManager.LoadScene (nextLevel);
		currentLevel = nextLevel;
	}

	public void ResetGame () {
		gameOver = true;
		levelComplete = false;
		currentLevel = 0;
		SceneManager.LoadScene (currentLevel);
		GameUI.Instance.DisplayMainMenu ();
	}

	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
		Application.Quit ();
		#endif
	}
}
