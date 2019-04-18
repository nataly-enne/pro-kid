using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {
    Vector2 completePosition;
    UIController uiControl;

    void Start()
    {
        Transform gold = GameObject.Find("Gold").transform;
        completePosition = new Vector2(Mathf.Round(gold.position.x), Mathf.Round(gold.position.z));
        uiControl = GameObject.Find("UI Manager").GetComponent<UIController>();
    }

    public bool CheckComplete(Transform robot)
    {
        if (robot.position.x == completePosition.x && robot.position.z == completePosition.y)
        {
            ButtonSettings bs = GameObject.Find("Level Lock").GetComponent<ButtonSettings>();

            if (bs.releasedLevel <= SceneManager.GetActiveScene().buildIndex - 1)
                bs.UnlockLevel();                

            uiControl.Congratulations();
            return true;
        } else
        {
            return false;
        }
    }
}
