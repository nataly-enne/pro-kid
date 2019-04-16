using UnityEngine;

public class Placeholder : MonoBehaviour {
    public void UpdateStatus () {
        DropZone drop = transform.GetComponentInParent<DropZone>();

        if (drop != null)
        {
            if (drop.full || drop.transform.name == "For Queue") { 
                gameObject.SetActive(false);
            } else
                gameObject.SetActive(true);
        }
    }
}
