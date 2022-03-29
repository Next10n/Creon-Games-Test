using System.Collections.Generic;
using Code.StaticData;
using UnityEngine;

namespace Code.Core
{
    public class MeshCutter : IMeshCutter
    {
        private readonly IStaticDataService _staticDataService;

        public MeshCutter(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Cut(RectMesh rectMesh, out Mesh leftMesh, out Mesh rightMesh, out BrokenLine brokenLine)
        {
            brokenLine = new BrokenLine();
            int pointX = Random.Range(1, rectMesh.Width - 1);
            List<Vector3> leftVertices = new List<Vector3>();
            List<Vector3> rightVertices = new List<Vector3>();
            
            
            List<int> leftTriangles = new List<int>();
            List<int> rightTriangles = new List<int>();

            for(int i = 0; i <= rectMesh.Height; i++)
            {
                for(int j = 0; j <= rectMesh.Width; j++)
                {
                    if(j <= pointX)
                        leftVertices.Add(rectMesh.Vertices[j, i]);

                    if(j >= pointX)
                        rightVertices.Add(rectMesh.Vertices[j, i]);

                    if(j == pointX)
                        brokenLine.AddPoint(rectMesh.Vertices[j, i]);
                }

                pointX = NextXRectPoint(pointX, rectMesh);

            }

            for(int i = 0; i < rectMesh.Vertices.GetLength(0) - 1; i++)
            {
                for(int j = 0; j < rectMesh.Vertices.GetLength(1) - 1; j++)
                {
                    AddTriangles(rightVertices, rightTriangles, i, j);
                    AddTriangles(leftVertices, leftTriangles, i, j);
                }
            }
            
            
            leftMesh = new Mesh() {
                vertices = leftVertices.ToArray(),
                triangles = leftTriangles.ToArray()
            };
            
            leftMesh.RecalculateNormals();

            rightMesh = new Mesh() {
                vertices = rightVertices.ToArray(),
                triangles = rightTriangles.ToArray()
            };
            
            rightMesh.RecalculateNormals();

            void AddTriangles(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                if(ContainsSquare(vertices, i, j))
                    AddSquareTriangles(vertices, triangles, i, j);
                else if(ContainsDownLeftTriangle(vertices, i, j))
                    AddDownLeftTriangle(vertices, triangles, i, j);
                else if(ContainsUpperRightTriangle(vertices, i, j))
                    AddUpperRightTriangle(vertices, triangles, i, j);
                else if(ContainsDownRightTriangle(vertices, i, j))
                    AddDownRightTriangle(vertices, triangles, i, j);
                else if(ContainsUpperLeftTriangle(vertices, i, j))
                    AddUpperLeftTriangle(vertices, triangles, i, j);
            }

            void AddSquareTriangles(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                AddDownLeftTriangle(vertices, triangles, i, j);
                AddUpperRightTriangle(vertices, triangles, i, j);
            }

            void AddDownLeftTriangle(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j + 1]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j]));
            }
            
            void AddDownRightTriangle(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j + 1]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j]));
            }


            void AddUpperRightTriangle(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j + 1]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j + 1]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j]));
            }
            
            void AddUpperLeftTriangle(List<Vector3> vertices, List<int> triangles, int i, int j)
            {
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i, j + 1]));
                triangles.Add(vertices.IndexOf(rectMesh.Vertices[i + 1, j + 1]));
            }
            
            
            bool ContainsSquare(List<Vector3> vertices, int i, int j)
            {
                return vertices.Contains(rectMesh.Vertices[i, j]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j]) &&
                    vertices.Contains(rectMesh.Vertices[i, j + 1]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j + 1]);
            }

            bool ContainsDownLeftTriangle(List<Vector3> vertices, int i, int j)
            {
                return vertices.Contains(rectMesh.Vertices[i, j]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j]) &&
                    vertices.Contains(rectMesh.Vertices[i, j + 1]);
            }
            
            bool ContainsDownRightTriangle(List<Vector3> vertices, int i, int j)
            {
                return vertices.Contains(rectMesh.Vertices[i, j]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j + 1]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j]);
            }


            bool ContainsUpperRightTriangle(List<Vector3> vertices, int i, int j)
            {
                return vertices.Contains(rectMesh.Vertices[i, j + 1]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j + 1]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j]);
            }
            
            bool ContainsUpperLeftTriangle(List<Vector3> vertices, int i, int j)
            {
                return vertices.Contains(rectMesh.Vertices[i, j]) &&
                    vertices.Contains(rectMesh.Vertices[i, j + 1]) &&
                    vertices.Contains(rectMesh.Vertices[i + 1, j +1]);
            }

        }
        
        private int NextXRectPoint(int currentX, RectMesh rectMesh)
        {
            if(currentX < _staticDataService.Data.CutBorderDistance)
                return ++currentX;

            if(currentX >= rectMesh.Width - _staticDataService.Data.CutBorderDistance)
                return --currentX;

            return currentX + (Random.Range(0, 2) == 0 ? -1 : 1);
        }

        
    }
}