using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkZone : zone.Zone {

    public override an.Annimal.Priority getSupportedPrio() {
        return an.Annimal.Priority.drink;
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Annimal")) {
            an.Annimal annimal = (an.Annimal)other.gameObject.GetComponent<an.Annimal>();
            annimal.isDrinking = true;
        }
    }

    protected override void OnTriggerExit(Collider other) {
        if (other.CompareTag("Annimal")) {
            an.Annimal annimal = (an.Annimal)other.gameObject.GetComponent<an.Annimal>();
            annimal.isDrinking = false;
        }
    }

}
