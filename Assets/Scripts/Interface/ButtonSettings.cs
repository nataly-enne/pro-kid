using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSettings : MonoBehaviour {
	public int releasedLevel = 1;
	GameObject button;


	void Awake(){
        DontDestroyOnLoad(transform.gameObject);
	}

    public void UnlockLevel ()
    {
        releasedLevel++;
    }

	public void ButtonNextLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Redirec(){
		SceneManager.LoadScene ("LevelSelect");	
	}
}
