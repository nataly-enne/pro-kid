using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class Robot : MonoBehaviour {    
    Direction dir;

    float turnDuration;
    float startRotation;
    Direction startDirection;

    LevelComplete comp;

    // Variáveis usadas para andar
    Vector3 initPos;
    Vector3 targetPos;

    // Variáveis usadas para rotacionar
    float initRot;

    public static List<Card> actions;
    
    public float gravity;

    void Awake(){
		transform.tag = "Player";
	}

	void Start () {
        int yRot = (int)transform.rotation.eulerAngles.y;
        switch (yRot)
        {
            case 0:
                dir = Direction.NORTH;
                break;

            case 90:
                dir = Direction.EAST;
                break;

            case 180:
                dir = Direction.SOUTH;
                break;

            case 270:
                dir = Direction.WEST;
                break;            
        }

        turnDuration = GameObject.Find("Game Manager").GetComponent<Interpreter>().turnDuration;
        startRotation = transform.rotation.eulerAngles.y;
        startDirection = dir;
        comp = GameObject.Find("Game Manager").GetComponent<LevelComplete>();
	}

    void Update()
    {
        if (!IsGrounded())
            transform.Translate(-Vector2.up * gravity * Time.deltaTime);

        if (transform.position.y < 0)
            Reset();
    }

    public IEnumerator DoActions ()
    {
        while(actions.Count > 0)
        {
            if (actions[0] != null)
            {
                StartCoroutine("RunAction", actions[0].type);
                actions.RemoveAt(0);
            }

            yield return new WaitForSeconds(1.5f * (turnDuration / 1000));
        }
    }

    IEnumerator RunAction (Type action)
    {
        switch (action)
        {
            #region WALK
            case Type.WALK:
                switch (dir)
                {
                    case Direction.NORTH:
                        targetPos = transform.position + Vector3.forward;
                        break;

                    case Direction.EAST:
                        targetPos = transform.position + Vector3.right;
                        break;

                    case Direction.SOUTH:
                        targetPos = transform.position + Vector3.back;
                        break;

                    case Direction.WEST:
                        targetPos = transform.position + Vector3.left;
                        break;
                }

                initPos = transform.position;

                float timer = 0;    // Vai de 0 até 1 em cada turno
                while (timer < 1)
                {
                    timer += (1 / (turnDuration / 1000)) * Time.deltaTime;
                    Vector3 pos = transform.position;

                    if (dir == Direction.NORTH || dir == Direction.SOUTH)
                        transform.position = new Vector3(pos.x, pos.y, Mathf.Lerp(initPos.z, targetPos.z, timer));
                    else if (dir == Direction.EAST || dir == Direction.WEST)
                        transform.position = new Vector3(Mathf.Lerp(initPos.x, targetPos.x, timer), pos.y, pos.z);
                    
                    yield return null;
                }
                break;
            #endregion

            #region ROTATECW
            case Type.ROTATECW:
                initRot = transform.rotation.eulerAngles.y;

                timer = 0;    // Vai de 0 até 1 em cada turno
                while (timer < 1)
                {
                    timer += (1 / (turnDuration / 1000)) * Time.deltaTime;
                    Quaternion rot = transform.rotation;
                    transform.rotation = Quaternion.Euler(0, Mathf.Lerp(initRot, initRot + 90, timer), 0);

                    yield return null;
                }

                int dirIndex = (int) dir;

                if (dirIndex == 3)
                    dirIndex = 0;
                else
                    dirIndex++;

                dir = (Direction) dirIndex;                
                break;
            #endregion

            #region ROTATECC
            case Type.ROTATECC:
                initRot = transform.rotation.eulerAngles.y;

                timer = 0;    // Vai de 0 até 1 em cada turno
                while (timer < 1)
                {
                    timer += (1 / (turnDuration / 1000)) * Time.deltaTime;
                    Quaternion rot = transform.rotation;
                    transform.rotation = Quaternion.Euler(0, Mathf.Lerp(initRot, initRot - 90, timer), 0);

                    yield return null;
                }

                dirIndex = (int)dir;

                if (dirIndex == 0)
                    dirIndex = 3;
                else
                    dirIndex--;

                dir = (Direction)dirIndex;
                break;
            #endregion

            #region TELEPORT
            case Type.TELEPORT:
                Teleport();
                break;
            #endregion

            #region IF
            case Type.IF:
                
                CheckBlock();
                break;
            #endregion
        }

        if (comp.CheckComplete(transform))
            StopAllCoroutines();
    }

    public void Reset()
    {
        StopAllCoroutines();
        transform.position = new Vector3(0, 0.9f, 0);
        transform.rotation = Quaternion.Euler(0, startRotation, 0);
        dir = startDirection;
    }

    void Teleport()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit);
        GameObject block = hit.transform.gameObject;
        Teleporter teleporter = block.GetComponent<Teleporter>();

        if (teleporter != null)
            transform.position = new Vector3(teleporter.connection.position.x, transform.position.y, teleporter.connection.position.z);
    }

    void CheckBlock ()
    {
        Transform colorQ;
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit);
        GameObject block = hit.transform.gameObject;

        if (block.name.Contains("Green") && !block.name.Contains("Teleport"))
            colorQ = GameObject.Find("UI Manager").GetComponent<UISettings>().greenQueue.transform;
        else if (block.name.Contains("Red"))
            colorQ = GameObject.Find("UI Manager").GetComponent<UISettings>().redQueue.transform;
        else
            colorQ = null;

        if (colorQ != null)
        {
            List<Card> queue = new List<Card>();
            queue.Add(null);
            actions.RemoveAt(0);

            for (int i = 0; i < colorQ.transform.childCount; i++)
            {
                if (!colorQ.GetChild(i).transform.name.Contains("Empty Slot"))
                {
                    if (colorQ.GetChild(i).transform.name.Contains("For"))
                    {
                        Transform forQ = GameObject.Find("UI Settings").GetComponent<UISettings>().greenQueue.transform;
                        int loopAmount = forQ.GetComponentInChildren<Token>().value;

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
                        queue.Add(colorQ.GetChild(i).GetComponent<Card>());
                    }
                }
            }

            queue.AddRange(actions);
            actions = queue;
        
        }
    }
    
    bool IsGrounded ()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.45f);
    }
}
