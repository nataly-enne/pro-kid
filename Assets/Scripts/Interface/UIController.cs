using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    UISettings settings;
    List<GameObject> queues = new List<GameObject>();
    List<GameObject> backgrounds = new List<GameObject>();
    int tabIndex = 0;

    [HideInInspector]
    public GameObject congrats;

    GameObject returnButton;

    void Start()
    {
        settings = GetComponent<UISettings>();
        returnButton = GameObject.Find("Return");

        queues.Add(settings.mainQueue);
        backgrounds.Add(settings.mainQueueBG);

        if (settings.hasFor)
        {
            queues.Add(settings.forQueue);
            backgrounds.Add(settings.forQueueBG);
        }

        if (settings.hasIf) {
            GameObject ifPlaceholder = new GameObject();
            ifPlaceholder.name = "IFTAB";
            queues.Add(ifPlaceholder);
            backgrounds.Add(ifPlaceholder);
        }

    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (tabIndex + 2 <= queues.Count)
            {
                if (queues[tabIndex].name.Contains("IFTAB"))
                {
                    settings.greenQueue.SetActive(false);
                    settings.greenQueueBG.SetActive(false);
                    settings.redQueue.SetActive(false);
                    settings.redQueueBG.SetActive(false);
                }
                else
                {
                    queues[tabIndex].SetActive(false);
                    backgrounds[tabIndex].SetActive(false);
                }

                tabIndex++;

                if(queues[tabIndex].name.Contains("IFTAB"))
                {
                    settings.greenQueue.SetActive(true);
                    settings.greenQueueBG.SetActive(true);
                    settings.redQueue.SetActive(true);
                    settings.redQueueBG.SetActive(true);
                }
                else
                {
                    queues[tabIndex].SetActive(true);
                    backgrounds[tabIndex].SetActive(true);
                }
            } else
            {
                if (queues[tabIndex].name.Contains("IFTAB"))
                {
                    settings.greenQueue.SetActive(false);
                    settings.greenQueueBG.SetActive(false);
                    settings.redQueue.SetActive(false);
                    settings.redQueueBG.SetActive(false);
                }
                else
                {
                    queues[tabIndex].SetActive(false);
                    backgrounds[tabIndex].SetActive(false);
                }

                tabIndex = 0;

                queues[tabIndex].SetActive(true);
                backgrounds[tabIndex].SetActive(true);
            }
        }
	}

    public void Congratulations ()
    {
        GameObject pool = GameObject.Find("Pool");
        GameObject trash = GameObject.Find("Trash");

        pool.SetActive(false);
        settings.mainQueue.SetActive(false);
        settings.mainQueueBG.SetActive(false);
        settings.forQueue.SetActive(false);
        settings.forQueueBG.SetActive(false);
        settings.greenQueue.SetActive(false);
        settings.greenQueueBG.SetActive(false);
        settings.redQueue.SetActive(false);
        settings.redQueueBG.SetActive(false);
        trash.SetActive(false);
        returnButton.SetActive(false);

        congrats.SetActive(true);
    }
}
