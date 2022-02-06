using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace an {
    public abstract class Annimal : MonoBehaviour {

        [Header("State")]
        protected int moveSpeed = 0;
        protected int eater = 0;
        protected int drinker = 0;
        public int hungerLvl = 0;
        public int thirstyLvl = 0;
        public int reproductionLvl = 0;
        private bool isBurn = false;
        [Header("Actions")]
        public bool isEating = false;
        public bool isDrinking = false;
        public bool isAffraid = false;
        [Header("Other")] 
        public GameObject annimalPrefab;
        public Material burnMaterial;
        public Animator anim;

        protected Vector3 randomDestination = Vector3.zero;
        public enum Priority {drink, eat, run, none};
        public static List<Annimal> speciesList = new List<Annimal>();

        //abstarct methods
        protected abstract void Initialyse();
        protected abstract Priority ChoosePriority();
        protected abstract void MakeBabies();
        protected abstract bool IsChicken { get; }

        //other methods
        private IEnumerator Start() {
            speciesList.Add(this);
            yield return new WaitForSeconds(1);
            InvokeRepeating(nameof(ChangeRandomDestination), 0.0f, 2f);
            Initialyse();
            InvokeRepeating(nameof(Action), 0.0f, 0.5f);
            InvokeRepeating(nameof(UpdateStates), 0.0f, 1f);
            InvokeRepeating(nameof(MakeBabies), 1f, 2f);
        }

        private void FixedUpdate() {
            anim.SetBool("isDrinking", isEating || isDrinking);
            // Move our position a step closer to the target.
            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            Vector3 target = ChooseTarget(); //get dirrection
            if (!isBurn) {
                transform.position = Vector3.MoveTowards(transform.position, target, step); //move
                if (target != Vector3.zero) {
                    transform.rotation = Quaternion.LookRotation(new Vector3(-target.x, 0, -target.z)); //rotate to match direction
                }
            }
        }
        private Vector3 ChooseTarget() {
            Priority prio = ChoosePriority();
            Vector3 bestTarget = Vector3.zero;
            float bestDistance = Mathf.Infinity;
            foreach (zone.Zone zone in zone.Zone.zonesList.ToArray()) {
                if (zone.getSupportedPrio().Equals(prio)) {
                    float localDistance = Vector3.Distance(transform.position, zone.transform.position);
                    if (localDistance < bestDistance) {
                        bestTarget = zone.transform.position;
                        bestDistance = localDistance;
                    }
                }

            }
            if (bestDistance.Equals(Mathf.Infinity)) {
                bestTarget = randomDestination;
            }
            return bestTarget;
        }

        private void ChangeRandomDestination() {
            randomDestination = transform.GetRandomDestination();
        }

        private void Action() { //decrease thirth and hunger
            if (isDrinking) {
                Drink();
            }
            if (isEating) {
                Eat();
            }
        }
        public void Drink() {
            thirstyLvl = thirstyLvl > 0 ? thirstyLvl - 1 : 0;
            isDrinking = !thirstyLvl.Equals(0);
        }
        public void Eat() {
            hungerLvl = hungerLvl > 0 ? hungerLvl - 1 : 0;
            isEating = !hungerLvl.Equals(0);
        }

        private void UpdateStates() { // increase thirt and hunger
            if (Random.Range(0, 100) <= eater) {
                hungerLvl = Mathf.Min(10, hungerLvl+1);
            }
            if (Random.Range(0, 100) <= drinker) {
                thirstyLvl = Mathf.Min(10, thirstyLvl + 1);
            }
            if (Random.Range(0, 10) <= 5) {
                reproductionLvl = Mathf.Min(10, reproductionLvl + 1);
            }

        }


        void OnParticleCollision(GameObject other) { 
            CancelInvoke();
            StartCoroutine(nameof(Burn));
        }

        public IEnumerator Burn() {
            //freez the annimal
            isBurn = true;
            Renderer rend = gameObject.GetComponentsInChildren<Renderer>()[0];
            rend.material = burnMaterial;
            yield return new WaitForSeconds(0.3f);
            KillAnnimal(this);
            yield return new WaitForSeconds(0.2f);
            anim.SetTrigger("death");
        }

        public static void KillAnnimal(Annimal annimalToRemove) {
            //destroy to free memory
            speciesList.Remove(annimalToRemove);
            //test if it was the last annimal alive
            if (speciesList.Count == 0) {
                GameManager.Instance.EndGame();
            }
        }

        public void RemoveAnnimal() {
            Destroy(gameObject);
            //test if it's a chicken or cow lover succes 
            if (speciesList.Count >= 10) {
                AreAllAnnimalSame();
            }
        }

        public static void RemoveAnnimal(Annimal annimalToRemove) {
            //destroy to clear view
            Destroy(annimalToRemove.gameObject);
            //test if it's a chicken or cow lover succes 
            if (speciesList.Count >= 10){
                AreAllAnnimalSame();
            }
        }

        private static void AreAllAnnimalSame() {
            bool areChicken = speciesList[0].IsChicken;
            foreach (Annimal annimal in speciesList) {
                if (annimal.IsChicken != areChicken)
                    return;
            }
            if (areChicken) {
                Succes.IsChickenLover = 1;
            } else {
                Succes.IsCowLover = 1;
            }
        }


    }
}
