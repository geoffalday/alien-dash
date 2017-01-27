using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProductionSceneLoader : MonoBehaviour {

	public static bool productionSceneIsLoaded;

	void Awake () {
		if (!productionSceneIsLoaded) {
			SceneManager.LoadScene ("ProductionScene", LoadSceneMode.Additive);
			productionSceneIsLoaded = true;
		}

		Destroy (gameObject);
	}

}
