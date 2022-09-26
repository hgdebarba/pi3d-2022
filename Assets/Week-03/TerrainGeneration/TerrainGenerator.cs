using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]

    public class TerrainGenerator : MonoBehaviour
    {
        public Material material;

        public int side = 10;
        Mesh mesh;

        public float scaleXZ = 1f;
        public float scaleY = 1f;

        public bool updateMesh = false;

        Vector3[] vertices;
        int[] triangles;

        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            if (material)
                GetComponent<MeshRenderer>().material = material;

            CreateShape();
            UpdateMesh();
        }

        private void Update()
        {
            // update mesh only when updateMesh is set to true
            if (!updateMesh)
                return;

            CreateShape();
            UpdateMesh();
            updateMesh = false;
        }

        void CreateShape()
        {
            vertices = new Vector3[side * side];
            triangles = new int[(side - 1) * (side - 1) * 6];

            for (int x = 0; x < side; x++)
            {
                for (int z = 0; z < side; z++)
                {
                    Vector3 vtx = new Vector3(x, 0, z);
                    vtx.y = Mathf.PerlinNoise(vtx.x * scaleXZ, vtx.z * scaleXZ);
                    vtx.y *= scaleY;
                    vertices[x + z * side] = vtx;
                }
            }

            int idx = 0;
            for (int x = 0; x < side - 1; x++)
            {
                for (int z = 0; z < side - 1; z++)
                {
                    int bl = x + z * side;
                    int br = x + 1 + z * side;
                    int tr = x + 1 + (z + 1) * side;
                    int tl = x + (z + 1) * side;

                    triangles[idx] = bl;
                    triangles[idx + 1] = tr;
                    triangles[idx + 2] = br;
                    triangles[idx + 3] = bl;
                    triangles[idx + 4] = tl;
                    triangles[idx + 5] = tr;
                    idx += 6;
                }
            }
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