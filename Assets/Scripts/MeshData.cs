using System.Collections.Generic;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class MeshData
    {
        public List<Vector3> Vertices { get; private set; }
        public List<int> Indices { get; private set; }

        public MeshData()
        {
            Vertices = new List<Vector3>();
            Indices = new List<int>();
        }

        public void AddQuad(in Vector3 position)
        {
            AddQuadVertices(position);
            AddQuadTriangles();
        }

        private void AddQuadVertices(in Vector3 position)
        {
            Vertices.Add(position);
            Vertices.Add(new Vector3(position.x, position.y, position.z + 1));
            Vertices.Add(new Vector3(position.x + 1, position.y, position.z + 1));
            Vertices.Add(new Vector3(position.x + 1, position.y, position.z));
        }

        private void AddQuadTriangles()
        {
            Indices.Add(Vertices.Count - 4);
            Indices.Add(Vertices.Count - 3);
            Indices.Add(Vertices.Count - 2);

            Indices.Add(Vertices.Count - 4);
            Indices.Add(Vertices.Count - 2);
            Indices.Add(Vertices.Count - 1);
        }
    }
}