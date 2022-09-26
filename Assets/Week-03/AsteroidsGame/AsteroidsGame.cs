using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Week03 {
    public class AsteroidsGame : MonoBehaviour
    {
        public ShipController spaceship;

        [Tooltip("projectile prefab")]
        [SerializeField]
        ProjectileController projectile;

        [Tooltip("asteroid prefab")]
        [SerializeField]
        AsteroidController asteroid;

        [SerializeField]
        int asteroidsInScene = 10;
        List<AsteroidController> asteroids;


        [HideInInspector]
        public Camera camera;
        public Color backgroundColor = Color.black;
        public float orthoVertSize = 100f;

        [Tooltip("game control, reset if true")]
        public bool restartGameFlag;


        // AsteroidsGame is a singleton
        private static AsteroidsGame _instance;
        public static AsteroidsGame Instance { get { return _instance; } }



        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }


        void Start()
        {
            camera = Camera.main;
            Assert.IsNotNull(camera, "could not find the main camera!");
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = backgroundColor;
            camera.orthographic = true;
            camera.orthographicSize = orthoVertSize; // this makes the screen size by size * aspectRatio

            asteroids = new List<AsteroidController>();

        }

        // Update is called once per frame
        void Update()
        {
            if (asteroids.Count == 0 || restartGameFlag) // win condition
            {
                Reset();
                restartGameFlag = false;
            }
        }


        public void SpawnAsteroid()
        {
            asteroids.Add(Instantiate<AsteroidController>(asteroid));
        }

        public void SpawnProjectile()
        {
            Instantiate<ProjectileController>(projectile);
        }

        public void DestroyAsteroid(AsteroidController asteroid)
        {
            asteroids.Remove(asteroid);
            Destroy(asteroid.gameObject);
        }


        public void Reset()
        {
            while (asteroids.Count > 0)
                DestroyAsteroid(asteroids[0]);

            for (int i = 0; i < asteroidsInScene; i++)
            {
                SpawnAsteroid();
            }

            spaceship.Reset();
        }

    }
}