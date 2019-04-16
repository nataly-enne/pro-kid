using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    WALK,
    ROTATECW,
    ROTATECC,
    TELEPORT,
    FOR,
    IF
}

public class Card : MonoBehaviour {
    public Type type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Morph (Type t)
    {
        type = t;
    }
}
