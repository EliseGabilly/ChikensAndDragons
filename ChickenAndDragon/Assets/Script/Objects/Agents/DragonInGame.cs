using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonInGame : MonoBehaviour {

    ParticleSystem.EmissionModule childPSEmissionModule;
    protected bool isSpitingFire = false;

    public Slider lifeBar;
    private bool imune = false;

    public Slider fireBar;

    private static DragonInGame instance;
    private DragonInGame() { } //block the use of new()

    // Access point
    public static DragonInGame Instance { get => instance; }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        childPSEmissionModule = gameObject.GetComponentsInChildren<ParticleSystem>()[0].emission;
    }

    private void Update() {
        if (Input.GetKeyDown("space")) {
            isSpitingFire = true;
        } else if (Input.GetKeyUp("space")) {
            isSpitingFire = false;
        }
        SpitFire();
    }

    protected void SpitFire() {
        //activate particules that burn the annimals
        childPSEmissionModule.enabled = isSpitingFire;
        if (isSpitingFire) {
            fireBar.value += 2;
            if (fireBar.value == 2000) {
                childPSEmissionModule.enabled = false;
            }
        } else {
            fireBar.value -= 1;
        }
    }

    public void Hurt() {
        if (!imune) {
            lifeBar.value -= 1;
            imune = true;
            StartCoroutine(nameof(SetImune));
            if (lifeBar.value == 0) {
                GameManager.Instance.EndGame();
            }
            AudioManager.Instance.PlayHurtSound();
        }
    }

    private IEnumerator SetImune() {
        yield return new WaitForSeconds(1f);
        imune = false;
    }
}