using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {
	GameManager gm = GameManager.instance;

	public void LoadScene()
	{
        save();
		SceneManager.LoadScene (0);
	}


    void save()
    {
        gm.SaveGame();
    }
}
