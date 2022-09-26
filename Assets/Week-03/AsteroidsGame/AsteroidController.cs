using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    public class AsteroidController : MonoBehaviour
    {
        [SerializeField]
        float minSize = 10;
        [SerializeField]
        float maxSize = 30;
        [SerializeField]
        float minSpeed = 10;
        [SerializeField]
        float maxSpeed = 50;
        [SerializeField]
        float minRotationSpeed = 20;
        [SerializeField]
        float maxRotationSpeed = 100;

        Vector3 direction;
        float speed;
        float angularSpeed;
        float size;

        void Start()
        {
            // set movement direction, speed, size and initial position        
            direction = Random.insideUnitCircle.normalized;
            speed = Random.Range(minSpeed, maxSpeed);
            angularSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            size = Random.Range(minSize, maxSize);

            float halfVertSize = AsteroidsGame.Instance.camera.orthographicSize;
            float halfHorzSize = halfVertSize * AsteroidsGame.Instance.camera.aspect;
            Vector3 position = new Vector3(Random.Range(-halfHorzSize, halfHorzSize), Random.Range(-halfVertSize, halfVertSize), 0);
            // ensure this is a valid position, repeat until it finds a valid one
            while (Physics.CheckSphere(position, size))
            {
                position = new Vector3(Random.Range(-halfHorzSize, halfHorzSize), Random.Range(-halfVertSize, halfVertSize), 0);
            }
            transform.localScale = new Vector3(1f, 1f, 0.01f) * size;
            transform.position = position;
        }

        void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
            transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

            WrapToScreen();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Projectile")
                AsteroidsGame.Instance.DestroyAsteroid(this);

        }

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
    }

}