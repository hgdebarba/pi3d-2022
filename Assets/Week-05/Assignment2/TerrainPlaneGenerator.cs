using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week03
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]

    public class TerrainPlaneGenerator : MonoBehaviour
    {
        public Material material;

        public int side = 100;
        Mesh mesh;

        Vector3[] vertices;
        Vector2[] uvCoords;
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

        void CreateShape()
        {
            vertices = new Vector3[side * side];
            uvCoords = new Vector2[side * side];
            triangles = new int[(side - 1) * (side - 1) * 6];

            for (int x = 0; x < side; x++)
            {
                for (int z = 0; z < side; z++)
                {
                    Vector3 vtx = new Vector3(x, 0, z);
                    vertices[x + z * side] = vtx;
                    Vector2 uv = new Vector2(x, z) / (float)(side - 1);
                    uvCoords[x + z * side] = uv;
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
            mesh.uv = uvCoords;
            mesh.RecalculateNormals();
        }
    }
}