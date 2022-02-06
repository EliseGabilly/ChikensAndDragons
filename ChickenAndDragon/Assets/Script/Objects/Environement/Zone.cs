using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zone {
    public abstract class Zone : MonoBehaviour {

        public static List<Zone> zonesList = new List<Zone>();

        protected Vector3 position;

        private void Start() {
            zonesList.Add(this);
            position = gameObject.transform.position;
        }

        public Vector3 GetPosition() {
            return position;
        }
        public abstract an.Annimal.Priority getSupportedPrio();

        protected abstract void OnTriggerEnter(Collider other);
        protected abstract void OnTriggerExit(Collider other);

        public static void ResetList() {
            zonesList = new List<Zone>();
        }
    }
}
