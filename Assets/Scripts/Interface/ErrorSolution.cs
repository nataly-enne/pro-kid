using UnityEngine;

public class ErrorSolution : MonoBehaviour {
    
	void Update () {
        Debug.Log(transform.childCount);
        if (transform.childCount >= 12 && !Input.GetKey(KeyCode.Mouse0))
        {
            Destroy(transform.GetChild(11).gameObject);
        }
	}
}
