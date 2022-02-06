using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCloudOpaque : MonoBehaviour {

    public Material opaque;
    //public Material transparant;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cloud")) {
            StartCoroutine(GradualyIncreaseAlpha(other.transform));
        }
    }

    private IEnumerator GradualyIncreaseAlpha(Transform obj) {
        Color color = obj.GetComponent<MeshRenderer>().material.color;
        for(int i=0; i<30; i++) {
            yield return new WaitForSeconds(0.01f);
            color.a += 0.025f;
            obj.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Cloud")) {
            StartCoroutine(GradualyDecreaseAlpha(other.transform));
        }
    }

    private IEnumerator GradualyDecreaseAlpha(Transform obj) {
        Color color = obj.GetComponent<MeshRenderer>().material.color;
        for (int i = 0; i < 30; i++) {
            yield return new WaitForSeconds(0.01f);
            color.a -= 0.025f;
            obj.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
