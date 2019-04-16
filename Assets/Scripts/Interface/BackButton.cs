using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

	public void Back () {
		SceneManager.LoadScene("LevelSelect");
	}

	public void NextDemo0 (){
		SceneManager.LoadScene("Demo");
	}

	public void NextDemo (){
		SceneManager.LoadScene("Demo2");
	}

	public void NextDemo2 (){
		SceneManager.LoadScene("Demo3");
	}

	public void NextDemo3 (){
		SceneManager.LoadScene("Demo4");
	}

	public void Menu (){
		SceneManager.LoadScene ("MainMenu");
	
	}
		
}
