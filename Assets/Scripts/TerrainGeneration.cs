using UnityEngine;

namespace Core.BiomeGeneration
{
    public class TerrainGeneration : MonoBehaviour
    {
        [SerializeField] private int m_chunkSize;
        [SerializeField] private int m_maxHeight;
        private Mesh m_mesh;
        private MeshFilter m_filter;

        private void Awake()
        {
            m_filter = GetComponent<MeshFilter>();
            m_mesh = new Mesh();
        }

        private void Start()
        {
            m_filter.mesh = m_mesh;

            GenerateTerrain(transform.position);
        }

        public void GenerateTerrain(in Vector3 position)
        {
            ChunkGeneration chunkGeneration = new ChunkGeneration();
            MeshData meshData = chunkGeneration.Generate(position, m_chunkSize);

            m_mesh.vertices = meshData.Vertices.ToArray();
            m_mesh.triangles = meshData.Indices.ToArray();
            m_mesh.RecalculateNormals();
        }
    }
}
