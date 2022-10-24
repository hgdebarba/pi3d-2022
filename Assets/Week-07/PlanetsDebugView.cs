using UnityEngine;

namespace Week07
{
    public class PlanetsDebugView : MonoBehaviour
    {
        public bool showDebugObjects = true;

        public Transform sun;
        public Transform earth;
        public Transform moon;

        public Transform earthOrbit;
        public Transform moonOrbit;


        // OnDrawGizmos let you draw your own custom gizmos
        // gizmos are shown only in the editor screen
        void OnDrawGizmos()
        {
            if (!showDebugObjects)
                return;


            // we normally don't manipulate matrices directly nowadays
            // but they are used to define the world pose when drawing gizmos
            if (sun == null || earth == null || moon == null)
                return;

            Gizmos.color = Color.green;

            Gizmos.matrix = sun.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = earth.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = moon.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);

            // extra:
            // for completness, here is how we can obtain the three matrices above (local to world)
            // by using only local transformation matrices and matrix algebra
            if (earthOrbit == null || moonOrbit == null)
                return;

            // local transformation matrices:
            // we create these since Unity don't give it to us
            Matrix4x4 sunToWorld = Matrix4x4.TRS(sun.localPosition, sun.localRotation, sun.localScale);
            Matrix4x4 earthToEarthOrbit = Matrix4x4.TRS(earth.localPosition, earth.localRotation, earth.localScale);
            Matrix4x4 moonToMoonOrbit = Matrix4x4.TRS(moon.localPosition, moon.localRotation, moon.localScale);
            Matrix4x4 earthOrbitToSun = Matrix4x4.TRS(earthOrbit.localPosition, earthOrbit.localRotation, earthOrbit.localScale);
            Matrix4x4 moonOrbitToEarth = Matrix4x4.TRS(moonOrbit.localPosition, moonOrbit.localRotation, moonOrbit.localScale);

            // matrix algebra: 
            // here we compose the matrices, transformations are applied from right to left:
            // earthToEarthOrbit -> earthOrbitToSun -> sunToWorld
            Matrix4x4 earthToWorld = sunToWorld * earthOrbitToSun * earthToEarthOrbit;
            // moonToMoonOrbit -> moonOrbitToEarth -> earthToWorld
            Matrix4x4 moonToWorld = earthToWorld * moonOrbitToEarth * moonToMoonOrbit;
            // (unity traverse the hierarchy and does this every time an object moves for rendering)

            Gizmos.color = Color.red;
            // this is just for visualization, as I render the same sphere wireframe multiple times
            Matrix4x4 rotOffset = Matrix4x4.Rotate(Quaternion.Euler(0, 30, 0));
            Gizmos.matrix = sunToWorld * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = earthToWorld * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = moonToWorld * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);


            // another extra:
            // a local transformation matrix is just a the rotation scale and rotation of a transform
            // knowing that the transform has a .parent property, we could implement the code above
            // as a recursive function, that repeats the matrix multiplication until it reaches the 
            // root of the scene.
            // That is more or less how Unity handles building the matrices for rendering
            Gizmos.color = Color.magenta;

            rotOffset = Matrix4x4.Rotate(Quaternion.Euler(0, 60, 0));
            Gizmos.matrix = RecursiveLocalToWorld(sun) * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = RecursiveLocalToWorld(earth) * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);
            Gizmos.matrix = RecursiveLocalToWorld(moon) * rotOffset;
            Gizmos.DrawWireSphere(Vector3.zero, .51f);

        }

        Matrix4x4 RecursiveLocalToWorld(Transform t)
        {
            Matrix4x4 localToParent = Matrix4x4.TRS(t.localPosition, t.localRotation, t.localScale);

            // if t.parent == null, it means the object is at the root of the hierarchy
            if (t.parent == null)
                return localToParent;

            // if not, we need to integrate the parent transformation matrix
            return RecursiveLocalToWorld(t.parent) * localToParent;
        }
    }
}