using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuccesOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponentInChildren<Text>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponentInChildren<Text>().enabled = false;
    }
}
