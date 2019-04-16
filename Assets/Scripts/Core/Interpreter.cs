using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour {
    public Transform mainQ;
    public Transform forQB;
    public Transform forQ;
    public Robot robot;
    Card card;
    List<Card> queue;
    
    public float turnDuration;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            robot.Reset();
            RunQueue();
        }
	}

    void RunQueue()
    {
        queue = new List<Card>();

        for (int i = 0; i < mainQ.transform.childCount; i++)
        {
            if (mainQ.GetChild(i).transform.name.Contains("For"))
            {
                int loopAmount = forQB.GetComponentInChildren<Token>().value;

                for (int j = 0; j < loopAmount; j++)
                {
                    for (int k = 0; k < forQ.childCount; k++)
                    {
                        if (!forQ.GetChild(k).transform.name.Contains("Empty Slot") && !forQ.GetChild(k).transform.name.Contains("Token"))
                            queue.Add(forQ.GetChild(k).GetComponent<Card>());
                    }
                }
            }
            else
            {
                queue.Add(mainQ.GetChild(i).GetComponent<Card>());
            }
        }

        Robot.actions = queue;
        robot.StartCoroutine("DoActions");
    }
}
