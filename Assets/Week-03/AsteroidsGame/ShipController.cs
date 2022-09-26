using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Week03
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        float linearAcceleration = 60.0f;
        Vector3 velocity = Vector3.zero;

        [SerializeField]
        float rotationAcceleration = 120.0f;
        float accumulatedRotationSpeed = 0.0f;

        [SerializeField]
        float rotationSpeed = 120.0f;

        [SerializeField]
        bool useRotationAcceleration = false;

        void Start()
        {
            Reset();
        }

        void Update()
        {
            // get movement control input
            float pedal = Input.GetAxisRaw("Vertical");
            float wheel = Input.GetAxisRaw("Horizontal");

            if (useRotationAcceleration)
            {
                // rotate with acceleration, much harder to control, but morerealistic in space :)
                accumulatedRotationSpeed += -wheel * rotationAcceleration * Time.deltaTime;
                transform.Rotate(0, 0, accumulatedRotationSpeed * Time.deltaTime);
            }
            else
            {
                // rotate with rotation speed parameter, easier to control
                accumulatedRotationSpeed = -wheel * rotationSpeed;
                transform.Rotate(0, 0, accumulatedRotationSpeed * Time.deltaTime);
            }

            // translation control
            velocity += transform.up * pedal * linearAcceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;

            WrapToScreen();

            // fire projectile
            if (Input.GetButtonDown("Fire1"))
            {
                AsteroidsGame.Instance.SpawnProjectile();
            }
        }

        public void Reset()
        {
            // reset pose and movement parameters
            velocity = Vector3.zero;
            accumulatedRotationSpeed = 0.0f;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }


        // ensure the spaceship is always visible
        void WrapToScreen()
        {
            float halfVertSize = AsteroidsGame.Instance.camera.orthographicSize;
            float halfHorzSize = halfVertSize * AsteroidsGame.Instance.camera.aspect;
            if (transform.position.y > halfVertSize)
            {
                transform.position += (Vector3.down * halfVertSize * 2);
            }
            else if (transform.position.y < -halfVertSize)
            {
                transform.position += (Vector3.up * halfVertSize * 2);
            }
            else if (transform.position.x > halfHorzSize)
            {
                transform.position += (Vector3.left * halfHorzSize * 2);
            }
            else if (transform.position.x < -halfHorzSize)
            {
                transform.position += (Vector3.right * halfHorzSize * 2);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // loss condition, simply reset the game
            if (other.tag == "Asteroid")
                AsteroidsGame.Instance.restartGameFlag = true;
        }
    }
}