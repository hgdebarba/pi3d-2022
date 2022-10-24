using UnityEngine;

namespace Week08
{
    public class QuaternionVsEuler : MonoBehaviour
    {
        public Transform startRot, endRot;
        public bool useQuaternion = false;
        [Range(1, 30)]
        public float duration = 3.0f;

        void Update()
        {

        }
    }
}