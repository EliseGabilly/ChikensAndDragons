using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace dr {
    public class Dragon : MonoBehaviour {

        public Rigidbody rb;
        protected float hDirection;
        private float hMovement;
        protected float vDirection;
        private float vMovement;
        private readonly float speed = 400;
        private Vector3 velocity = Vector3.zero;


        private static Dragon instance;
        private Dragon() { } //block the use of new()

        // Access point
        public static Dragon Instance { get => instance; }

        void Awake() {
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }

        private void Update() {
            //get direction based on input
            hDirection = Input.GetAxis("Horizontal");
            vDirection = Input.GetAxis("Vertical");
        }

        private void FixedUpdate() {
            hMovement = hDirection * speed * Time.deltaTime;
            vMovement = vDirection * speed * Time.deltaTime;
            Vector3 targetVelocity = new Vector3(hMovement, 0, vMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if (targetVelocity != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(targetVelocity);
            }
        }

    }
}
