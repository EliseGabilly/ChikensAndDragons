using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cloud")) {
            Destroy(other.transform.gameObject);
        }
    }
}
