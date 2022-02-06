using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : an.Annimal {

    protected override bool IsChicken { get => false; }
    protected override void Initialyse() {
        eater = Random.Range(5, 20);
        drinker = Random.Range(10, 30);
        moveSpeed = Random.Range(5, 15);
        hungerLvl = Random.Range(0, 7);
        thirstyLvl = Random.Range(0, 8);
    }


    protected override Priority ChoosePriority() {
        if ((isDrinking && thirstyLvl <= 2) || thirstyLvl == 10) {
            return Priority.drink;
        } else if ((isEating && hungerLvl <= 2) || hungerLvl == 10) {
            return Priority.eat;
        } else if (isDrinking && thirstyLvl + 1 > hungerLvl) {
            return Priority.drink;
        } else if (isEating && hungerLvl + 1 > thirstyLvl) {
            return Priority.eat;
        } else if ((hungerLvl > thirstyLvl) && hungerLvl > 1) {
            return Priority.eat;
        } else if (thirstyLvl > 1) {
            return Priority.drink;
        }
        return Priority.none;
    }

    protected override void MakeBabies() {
        foreach (an.Annimal annimal in speciesList) {
            if (annimal.GetType().Equals(this.GetType()) && !annimal.Equals(this)) { //if they are form the same species
                if (annimal.reproductionLvl > 4 && this.reproductionLvl > 4) { //if they are ready to mate
                    if (annimal.hungerLvl + annimal.thirstyLvl + this.hungerLvl + this.thirstyLvl <= 2) { //if they have nothing to do
                        if (Vector3.Distance(annimal.transform.position, this.transform.position) < 6) {
                            GameObject baby = Instantiate(annimalPrefab, Vector3.Lerp(annimal.transform.position, this.transform.position, 0.5f), Quaternion.identity);
                            baby.transform.parent = transform.parent;
                            annimal.reproductionLvl = 0;
                            this.reproductionLvl = 0;
                        }
                    }
                }
            }
        }
    }
}
