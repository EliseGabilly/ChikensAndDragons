using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chiken : an.Annimal {

    protected override bool IsChicken { get => true; }
    protected override void Initialyse() {
        eater = Random.Range(20, 50);
        drinker = Random.Range(10, 40);
        moveSpeed = Random.Range(10, 20);
        hungerLvl = Random.Range(0, 8);
        thirstyLvl = Random.Range(0, 7);
    }


    protected override Priority ChoosePriority() {
        if ((isEating && hungerLvl <= 2) || hungerLvl==10) {
            return Priority.eat;
        } else if ((isDrinking && thirstyLvl <= 2) || thirstyLvl == 10) {
            return Priority.drink;
        } else if (isEating && hungerLvl +1> thirstyLvl) {
            return Priority.eat;
        } else if (isDrinking && thirstyLvl +1>hungerLvl) {
            return Priority.drink;
        } else if ((hungerLvl > thirstyLvl) && hungerLvl>1) {
            return Priority.eat;
        } else if (thirstyLvl>1){
            return Priority.drink;
        }
        return Priority.none;
    }

    protected override void MakeBabies() {
        if (reproductionLvl > 4) {
            if (this.hungerLvl == 0 && this.thirstyLvl == 0) { //if they have nothing to do
                GameObject baby = Instantiate(annimalPrefab, this.transform.position, Quaternion.identity);
                baby.transform.parent = transform.parent;
                this.reproductionLvl = 0;
            }
        }
    }

}
