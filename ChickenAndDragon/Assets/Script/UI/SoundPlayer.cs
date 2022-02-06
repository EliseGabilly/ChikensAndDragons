using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    public void PlayOverSound() {
        AudioManager.Instance.PlayOverSound();
    }
    public void PlayClickSound() {
        AudioManager.Instance.PlayClickSound();
    }
}
