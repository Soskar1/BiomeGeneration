using UnityEngine;

namespace Core.BiomeGeneration
{
    [RequireComponent(typeof(MeshFilter))]
    public class ChunkRenderer : MonoBehaviour
    {
        private MeshFilter m_meshFilter;

        private void Awake() => m_meshFilter = GetComponent<MeshFilter>();

        public void Render(in ChunkData chunkData)
        {
            Mesh mesh = new Mesh();
            mesh.vertices = chunkData.MeshData.Vertices.ToArray();
            mesh.triangles = chunkData.MeshData.Indices.ToArray();
            mesh.RecalculateNormals();

            m_meshFilter.mesh = mesh;
        }
    }
}