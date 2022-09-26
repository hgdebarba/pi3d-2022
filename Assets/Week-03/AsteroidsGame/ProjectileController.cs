using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        float speed = 150f;
        Vector3 direction;
        [SerializeField]
        float size = 2f;
        [SerializeField]
        float lifeTime = 2f;
        float timer;

        void Start()
        {
            var gameInstance = AsteroidsGame.Instance;
            timer = lifeTime;
            direction = gameInstance.spaceship.transform.up;
            transform.position = gameInstance.spaceship.transform.position;
            transform.localScale = new Vector3(1f, 1f, .01f) * size;
        }

        void Update()
        {
            // move projectile, ensure it is locked in z;
            Vector3 displacement = direction * speed * Time.deltaTime;
            displacement.z = 0;
            transform.position += displacement;

            // control lifetime of this object
            timer -= Time.deltaTime;
            if (timer < 0)
                Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Asteroid")
                Destroy(gameObject);
        }
    }
}