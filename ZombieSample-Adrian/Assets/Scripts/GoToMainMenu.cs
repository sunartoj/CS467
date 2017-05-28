using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {
	GameManager gm = GameManager.instance;

	public void LoadScene()
	{
		gm.level = 0;
		SceneManager.LoadScene ("MainMenu");
	}
}
