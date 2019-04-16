using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour {

	void Update () {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject.Find("Level Lock").GetComponent<ButtonSettings>().releasedLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject.Find("Level Lock").GetComponent<ButtonSettings>().releasedLevel--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
