using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            DragonInGame.Instance.Hurt();
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            DragonInGame.Instance.Hurt();
        }
    }

}
