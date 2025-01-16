using System.Collections.Generic;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class MeshData
    {
        public List<Vector3> Vertices { get; private set; }
        public List<int> Indices { get; private set; }
        public List<Color> Colors { get; private set; }

        public MeshData()
        {
            Vertices = new List<Vector3>();
            Indices = new List<int>();
            Colors = new List<Color>();
        }

        public void AddQuad(in Quad quad)
        {
            foreach (Vector3 vertex in quad.vertices)
                Vertices.Add(vertex);
            
            foreach (Color color in quad.colors)
                Colors.Add(color);

            AddQuadTriangles();
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

    public readonly struct Quad
    {
        public readonly Vector3[] vertices;
        public readonly Color[] colors;

        public Quad(in Vector3[] vertices, in Color[] colors)
        {
            this.vertices = vertices;
            this.colors = colors;
        }

        public Quad(in Vector3[] vertices)
        {
            this.vertices = vertices;
            colors = null;
        }
    }
}