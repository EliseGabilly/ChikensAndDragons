using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatZone : zone.Zone {

    public override an.Annimal.Priority getSupportedPrio() {
        return an.Annimal.Priority.eat;
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Annimal")) {
            an.Annimal annimal = (an.Annimal)other.gameObject.GetComponent<an.Annimal>();
            annimal.isEating = true;
        }
    }

    protected override void OnTriggerExit(Collider other) {
        if (other.CompareTag("Annimal")) {
            an.Annimal annimal = (an.Annimal)other.gameObject.GetComponent<an.Annimal>();
            annimal.isEating = false;
        }
    }

}
