using UnityEngine;

namespace Week07
{
    public class RotateAround : MonoBehaviour
    {
        public Vector3 axis = Vector3.up;
        public float rotationRate = 100f;

        void Update()
        {
            // rotate around axis at rotation rate per second
            transform.Rotate(axis, Time.deltaTime * rotationRate);
        }
    }
}