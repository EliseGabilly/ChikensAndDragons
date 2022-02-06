using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Annimal")) {
            an.Annimal annimal = (an.Annimal)other.gameObject.GetComponent<an.Annimal>();
            an.Annimal.KillAnnimal(annimal);
            an.Annimal.RemoveAnnimal(annimal);
        }
    }
}
