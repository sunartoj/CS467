using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadScene : MonoBehaviour {
	
	GameManager gm = GameManager.instance;

	public void LoadScene()
	{
		Debug.Log ("Reloading Level" + gm.level);
        gm.currScore = gm.levelStartScore;
		gm.level--;
		SceneManager.LoadScene (1);
	}
}
