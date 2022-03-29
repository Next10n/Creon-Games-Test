using UnityEngine;

namespace Code.Core
{
    public class RectMesh
    {
        public readonly Mesh Mesh;
        public readonly int Width;
        public readonly int Height;

        public Vector3[,] Vertices { get; private set; }

        public RectMesh(int width, int height)
        {
            Width = width;
            Height = height;
            Mesh = new Mesh {
                vertices = CreateVertices(),
                triangles = CreateTriangles()
            };
            Mesh.RecalculateNormals();
        }

        private Vector3[] CreateVertices()
        {
            Vector3[] vertices = new Vector3[(Width + 1) * (Height + 1)];
            Vertices = new Vector3[Width + 1, Height + 1];
            for(int i = 0, k = 0; i <= Width; i++)
            {
                for(int j = 0; j <= Height; j++)
                {
                    Vector3 vector3 = new Vector3(i, 0, j);
                    vertices[k++] = vector3;
                    Vertices[i, j] = vector3;
                }
            }
            
            return vertices;
        }
        
        private int[] CreateTriangles()
        {
            int[] triangles = new int[Width * Height * 6];
            int vert = 0;
            int tris = 0;

            for(int j = 0; j < Width; j++)
            {
                for(int i = 0; i < Height; i++)
                {
                    triangles[tris] = vert + 0;
                    triangles[tris + 1] = vert + 1;
                    triangles[tris + 2] = vert + Height + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + Height + 2;
                    triangles[tris + 5] = vert + Height + 1;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            return triangles;
        }
    }
}