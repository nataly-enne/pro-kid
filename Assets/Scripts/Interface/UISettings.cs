using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour {
    public GameObject uiPrefab;
    public GameObject slotPrefab;

    public bool hasWalk;
    public bool hasRotateCW;
    public bool hasRotateCC;
    public bool hasTeleport;
    public bool hasFor;
    public bool hasIf;

    public int mainQueueSize;
    public int forQueueSize;
    public int greenQueueSize;
    public int redQueueSize;

    [HideInInspector]
    public GameObject mainQueue;
    [HideInInspector]
    public GameObject mainQueueBG;
    [HideInInspector]
    public GameObject forQueue;
    [HideInInspector]
    public GameObject forQueueBG;
    [HideInInspector]
    public GameObject greenQueue;
    [HideInInspector]
    public GameObject greenQueueBG;
    [HideInInspector]
    public GameObject redQueue;
    [HideInInspector]
    public GameObject redQueueBG;

    void Start()
    {
        GameObject pool = GameObject.Find("Pool");
        mainQueueBG = GameObject.Find("Main Queue Background");
        mainQueue = GameObject.Find("Main Queue");

        #region Card Instantiation
        int cardCounter = 0;
        if (hasWalk)
        {
            pool.transform.Find("Walk").gameObject.SetActive(true);
            cardCounter++;
        }
        if (hasRotateCW) {
            pool.transform.Find("RotateCW").gameObject.SetActive(true);
            cardCounter++;
        }
        if (hasRotateCC)
        {
            pool.transform.Find("RotateCC").gameObject.SetActive(true);
            cardCounter++;
        }
        if (hasTeleport)
        {
            pool.transform.Find("Teleport").gameObject.SetActive(true);
            cardCounter++;
        }
        if (hasIf)
        {
            pool.transform.Find("If").gameObject.SetActive(true);
            cardCounter++;
        }
        if (hasFor)
        {
            pool.transform.Find("For").gameObject.SetActive(true);
            cardCounter++;
        }
        #endregion
        
        RectTransform poolRect = pool.GetComponent<RectTransform>();
        HorizontalLayoutGroup poolLG = pool.GetComponent<HorizontalLayoutGroup>();
        poolRect.sizeDelta = new Vector2(poolLG.padding.right + poolLG.padding.left + cardCounter * 128 + (cardCounter - 1) * poolLG.spacing, 140);

        mainQueue.GetComponent<DropZone>().maxChildren = mainQueueSize;
        RectTransform mainQueueRect = mainQueue.GetComponent<RectTransform>();
        HorizontalLayoutGroup mainQueueLG = mainQueue.GetComponent<HorizontalLayoutGroup>();
        Vector2 sD = new Vector2(mainQueueLG.padding.right + mainQueueLG.padding.left + mainQueueSize * 128 + (mainQueueSize - 1) * mainQueueLG.spacing, 140);
        mainQueueRect.sizeDelta = sD;
        mainQueueBG.GetComponent<RectTransform>().sizeDelta = sD;

        for (int i = 1; i <= mainQueueSize; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.name = "Empty Slot";
            slot.transform.SetParent(mainQueueBG.transform);
        }

        forQueue = GameObject.Find("For Queue");
        forQueue.SetActive(false);
        forQueueBG = GameObject.Find("For Queue Background");
        forQueueBG.SetActive(false);
        if (hasFor)
        {            
            RectTransform forQueueRect = forQueue.GetComponent<RectTransform>();
            HorizontalLayoutGroup forQueueLG = forQueue.GetComponent<HorizontalLayoutGroup>();
            sD = new Vector2(forQueueLG.padding.right + forQueueLG.padding.left + (forQueueSize + 1) * 128 + (forQueueSize + 1) * forQueueLG.spacing, 140);
            forQueueRect.sizeDelta = sD;
            forQueueBG.GetComponent<RectTransform>().sizeDelta = sD;
            forQueue.GetComponent<DropZone>().maxChildren = forQueueSize;

            for (int i = 1; i <= forQueueSize; i++)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "Empty Slot";
                slot.transform.SetParent(forQueueBG.transform);
            }
        }

        greenQueue = GameObject.Find("Green Queue");
        greenQueue.SetActive(false);
        greenQueueBG = GameObject.Find("Green Queue Background");
        greenQueueBG.SetActive(false);
        if (hasIf)
        {
            RectTransform greenQueueRect = greenQueue.GetComponent<RectTransform>();
            HorizontalLayoutGroup greenQueueLG = greenQueue.GetComponent<HorizontalLayoutGroup>();
            sD = new Vector2(greenQueueLG.padding.right + greenQueueLG.padding.left + greenQueueSize * 128 + (greenQueueSize - 1) * greenQueueLG.spacing, 140);
            greenQueueRect.sizeDelta = sD;
            greenQueueBG.GetComponent<RectTransform>().sizeDelta = sD;
            Vector3 lP = new Vector3(-greenQueueRect.sizeDelta.x / 2, greenQueueRect.localPosition.y, greenQueueRect.localPosition.z);
            greenQueueRect.localPosition = lP;
            greenQueueBG.GetComponent<RectTransform>().localPosition = lP;
            greenQueue.GetComponent<DropZone>().maxChildren = greenQueueSize;

            for (int i = 1; i <= greenQueueSize; i++)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "Empty Slot";
                slot.transform.SetParent(greenQueueBG.transform);
            }
        }

        redQueue = GameObject.Find("Red Queue");
        redQueue.SetActive(false);
        redQueueBG = GameObject.Find("Red Queue Background");
        redQueueBG.SetActive(false);
        if (hasIf)
        {
            RectTransform redQueueRect = redQueue.GetComponent<RectTransform>();
            HorizontalLayoutGroup redQueueLG = redQueue.GetComponent<HorizontalLayoutGroup>();
            sD = new Vector2(redQueueLG.padding.right + redQueueLG.padding.left + redQueueSize * 128 + (greenQueueSize - 1) * redQueueLG.spacing, 140);
            redQueueRect.sizeDelta = sD;
            redQueueBG.GetComponent<RectTransform>().sizeDelta = sD;
            Vector3 lP = new Vector3(redQueueRect.sizeDelta.x / 2, redQueueRect.localPosition.y, redQueueRect.localPosition.z);
            redQueueRect.localPosition = lP;
            redQueueBG.GetComponent<RectTransform>().localPosition = lP;
            redQueue.GetComponent<DropZone>().maxChildren = redQueueSize;

            for (int i = 1; i <= redQueueSize; i++)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "Empty Slot";
                slot.transform.SetParent(redQueueBG.transform);
            }
        }

        GameObject congrats = GameObject.Find("Congrats");
        GetComponent<UIController>().congrats = congrats;
        congrats.SetActive(false);
    }
}
