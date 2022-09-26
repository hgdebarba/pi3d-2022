using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    public class Minimal1PPController : MonoBehaviour
    {
        public float speedMult = 1f;
        public float rotSpeedMult = 1f;

        void Update()
        {
            Vector3 dir = new Vector3(
            Input.GetAxis("Horizontal"), // [-1,1] -> left, right
            0,
            Input.GetAxis("Vertical")); // [-1,1] -> backward, forward

            float factor = 0;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
                factor = 1.0f / dir.x;
            else
                factor = 1.0f / dir.z;

            // the bug I had in class was bacause I forgot to make this a Vector3 :) (was originally a Vector2)
            Vector3 max = dir * factor; 
            float magnitude = max.magnitude;
            // ensure input vector is not 0 before we divide 
            dir = magnitude > 1e-5 ? dir / magnitude : dir;

            Vector3 displacement = speedMult * dir * Time.deltaTime;
            transform.Translate(displacement);

            float rot = Input.GetAxis("Mouse X"); // mouse delta -> rotation
            transform.Rotate(0, rotSpeedMult * rot * Time.deltaTime, 0);
        }
    }
}