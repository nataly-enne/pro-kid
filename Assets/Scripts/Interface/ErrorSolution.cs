using UnityEngine;

public class ErrorSolution : MonoBehaviour {
    
	void Update () {
        if (transform.childCount >= 13 && !Input.GetKey(KeyCode.Mouse0))
            Destroy(transform.GetChild(12).gameObject);
	}
}
