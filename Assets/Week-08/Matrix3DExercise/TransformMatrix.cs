using UnityEngine;
using UnityEditor;

namespace Week08
{

    // In this activity we will see how the input that we provide in the Unity transform
    // can be used to create a transformation matrix from scratch

    [ExecuteInEditMode] // this attribute makes the script run without playing the scene
    public class TransformMatrix : MonoBehaviour
    {
        Matrix4x4 TRSMatrix = Matrix4x4.identity;

        // Called when script is loaded or a value is changed in the inspector 
        private void Update()
        {
            Vector3 position = transform.position;
            Vector3 rotation = transform.eulerAngles;
            Vector3 scale = transform.lossyScale;

            // Unity already include a function to create a transformation matrix
            // using Position, Rotation and Scale:
            //TRSMatrix = Matrix4x4.TRS(position, Quaternion.Euler(rotation), scale);
            // but we want to create our own!
            TRSMatrix = OurTRSMatrix(position, rotation, scale);
        }

        Matrix4x4 OurTRSMatrix(Vector3 position, Vector3 rotation, Vector3 scale) {

            // TODO create the translation matrix
            Matrix4x4 T = Matrix4x4.identity;
            T.m03 = position.x;
            T.m13 = position.y;
            T.m23 = position.z;

            // TODO create the scale matrix
            Matrix4x4 S = Matrix4x4.identity;
            S.m00 = scale.x;
            S.m11 = scale.y;
            S.m22 = scale.z;


            // TODO create three rotation matrices, one per rotation axis/euler angle
            Matrix4x4 RX = Matrix4x4.identity;
            float angleRadX = Mathf.Deg2Rad * rotation.x;
            RX.m11 = Mathf.Cos(angleRadX);
            RX.m21 = Mathf.Sin(angleRadX);
            RX.m12 = -Mathf.Sin(angleRadX);
            RX.m22 = Mathf.Cos(angleRadX);
            Matrix4x4 RY = Matrix4x4.identity;
            float angleRadY = Mathf.Deg2Rad * rotation.y;
            RY.m00 = Mathf.Cos(angleRadY);
            RY.m02 = Mathf.Sin(angleRadY);
            RY.m20 = -Mathf.Sin(angleRadY);
            RY.m22 = Mathf.Cos(angleRadY);
            Matrix4x4 RZ = Matrix4x4.identity;
            // rotation.z/RZ is given as an example
            float angleRad = Mathf.Deg2Rad * rotation.z;
            RZ.m00 =Mathf.Cos(angleRad);
            RZ.m10 = Mathf.Sin(angleRad);
            RZ.m01 = -Mathf.Sin(angleRad);
            RZ.m11 = Mathf.Cos(angleRad);

            // TODO concatenate the three matrices together
            // remember that, when using euler angles, rotation order matters!
            // by default, Unity implements the order Rotation Z -> then X -> then Y
            // notice that, since we use column vectors, transformations are applied from RIGHT TO LEFT
            Matrix4x4 R = RY * RX* RZ; // R = multiply RX, RY and RZ in the CORRECT ORDER

            // TODO concatenate the transformations into a single matrix
            // we first Scale -> then Rotate -> and then Translate
            // notice that, since we use column vectors, transformations are applied from RIGHT TO LEFT
            TRSMatrix = T * R * S; // TRSMatrix = multiply translation, scale and rotation in the CORRECT ORDER


            return TRSMatrix;
        }


        // draw debug objects
        void OnDrawGizmos()
        {
            // we apply the transformation, now all our Gizmos.Draw... calls will use this matrix
            Gizmos.matrix = TRSMatrix;

            // Draw a cube to show the effect of the transformation matrix
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(0, 0, 0), Vector3.one * 1.005f);
            Gizmos.color = Color.yellow * 0.25f;
            Gizmos.DrawCube(new Vector3(0, 0, 0), Vector3.one * 1.005f);
        }
    }
}