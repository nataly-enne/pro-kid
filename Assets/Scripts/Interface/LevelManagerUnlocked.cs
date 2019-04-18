using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerUnlocked : MonoBehaviour {
	public int level;
	public Image image;
	string levelString;

	void Start () {
		if (GameObject.Find("Level Lock").GetComponent<ButtonSettings>().releasedLevel >= level) {
			LevelUnlocked ();
		} else {
			LevelLocked ();
		}
	}

	public void LevelSelect(string _level)
	{
		levelString = _level;
		SceneManager.LoadScene (levelString);
	}



	void LevelLocked()
	{
		GetComponent<Button> ().interactable = false;
		image.enabled = true;
	}

	void LevelUnlocked ()
	{
		GetComponent<Button> ().interactable = true;
		image.enabled = false;
	}

}﻿
