using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    public class CorrectAxisInput : MonoBehaviour
    {

        public bool isCorrectionOn = false;

        void Start()
        {
            // small sphere used to represent the analog joystick input
            Transform sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            sphere.localScale = Vector3.one * .3f;
            sphere.SetParent(transform);

            Input.GetAxis("Horizontal"); // [-1,1] -> left, right
            Input.GetAxis("Vertical"); // [-1,1] -> backward, forward
            Input.GetAxis("Mouse X"); // mouse delta -> rotation
        }

        void Update()
        {
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (!isCorrectionOn) // skip correction
            {
                transform.localPosition = dir;
                return;
            }

            float factor = 0;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                factor = 1.0f / dir.x;
            else
                factor = 1.0f / dir.y;

            Vector2 max = dir * factor;
            float magnitude = max.magnitude;
            // ensure input vector is not 0 before we divide 
            dir = magnitude > 1e-5 ? dir / magnitude : dir;

            transform.localPosition = dir;
        }
    }
}