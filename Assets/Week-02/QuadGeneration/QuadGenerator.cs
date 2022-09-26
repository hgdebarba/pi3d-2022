using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week02
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class QuadGenerator : MonoBehaviour
    {
        public Material material;
        Mesh mesh;
        Vector3[] vertices;
        int[] triangles; // also known as indices (notice that these are integer values)

        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            if(material)
                GetComponent<MeshRenderer>().material = material;

            CreateShape();
            UpdateMesh();
        }

        void CreateShape()
        {
            // allocate the arrays
            vertices = new Vector3[4];
            triangles = new int[6];

            // create the vertices in the "vertices" array
            vertices[0] = new Vector3(-1, 0,-1);
            vertices[1] = new Vector3( 1, 0,-1);
            vertices[2] = new Vector3( 1, 0, 1);
            vertices[3] = new Vector3(-1, 0, 1);
            // connect the vertices into triangles using their index in the "vertices" array
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;
            triangles[3] = 0;
            triangles[4] = 3;
            triangles[5] = 2;
        }

        void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }

}