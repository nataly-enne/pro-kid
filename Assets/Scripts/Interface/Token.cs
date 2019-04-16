using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Token : MonoBehaviour, IPointerClickHandler {
    public int value = 1;
    public Sprite[] sprites = new Sprite[5];

	public void OnPointerClick(PointerEventData eventData)
    {
        if (value < 5)
            value++;
        else
            value = 1;

        GetComponent<Image>().sprite = sprites[value - 1];
    }
}
