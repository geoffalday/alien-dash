using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[HideInInspector] public bool gameOver;
	[HideInInspector] public int currentLevel;
	[HideInInspector] public bool levelComplete;

	void Awake () {
		if (instance == null) {
			instance = this;
		}  else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		//gameOver = true;
		levelComplete = false;
		currentLevel = SceneManager.GetActiveScene ().buildIndex;
	}

	void Update () {
		if (currentLevel == 0) {
			GameUI.instance.DisplayMainMenu ();
		}
	}

	public void NewGame () {
		GameUI.instance.HideAllMenus ();
		currentLevel = 1;
		gameOver = false;
		SceneManager.LoadScene (currentLevel);
	}

	public void RestartLevel () {
		gameOver = false;
		GameUI.instance.HideAllMenus ();
		SceneManager.LoadScene (currentLevel);
	}

	public void LoadMenu () {
		SceneManager.LoadScene ("Menu");
		GameUI.instance.DisplayMainMenu ();
	}

	public void PlayerDied () {
		gameOver = true;
		GameUI.instance.DisplayDeathMenu ();
	}

	public void LevelCompleted () {
		levelComplete = true;

		if (currentLevel < SceneManager.sceneCountInBuildSettings - 1) {
			GameUI.instance.DisplayLevelCompleteMenu ();
		} else {
			GameUI.instance.DisplayGameCompleteMenu ();
		}
	}

	public void LoadNextLevel () {
		levelComplete = false;
		GameUI.instance.HideAllMenus ();

		int nextLevel = currentLevel + 1;
		SceneManager.LoadScene (nextLevel);
		currentLevel = nextLevel;
	}

	public void ResetGame () {
		gameOver = true;
		levelComplete = false;
		currentLevel = 0;
		SceneManager.LoadScene (currentLevel);
		GameUI.instance.DisplayMainMenu ();
	}

	public void QuitGame () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
		Application.Quit ();
		#endif
	}
}
