using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadScene : MonoBehaviour {
	
	GameManager gm = GameManager.instance;

	public void LoadScene()
	{
        gm.SaveGame();

		Debug.Log ("Reloading Level" + gm.level);
        gm.currScore = gm.levelStartScore;
        gm.currGold = gm.levelStartGold;
		gm.level--;
		SceneManager.LoadScene (gm.level + 1);
	}
}
