using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuButtons : MonoBehaviour
{
    Button myButton;

    void Awake()
    {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here

        myButton.onClick.AddListener(() => { LoadScene(1); });  // <-- you assign a method to the button OnClick event here
        
    }

    void myFunctionForOnClickEvent(string argument1, float argument2)
    {
        // your code goes here
        print(argument1 + ", " + argument2.ToString());
    }


    public void LoadScene(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
		
}
