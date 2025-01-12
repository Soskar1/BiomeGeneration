using UnityEngine;

namespace Core.BiomeGeneration
{
    public class TerrainGeneration : MonoBehaviour
    {
        [SerializeField] private int m_chunkSize;
        [SerializeField] private int m_maxHeight;
        [SerializeField] private NoiseData m_noiseData;
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

            GenerateTerrain(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
        }

        public void GenerateTerrain(in Vector3Int position)
        {
            ChunkGeneration chunkGeneration = new ChunkGeneration(m_noiseData);
            MeshData meshData = chunkGeneration.Generate(position, m_chunkSize, m_maxHeight);

            m_mesh.vertices = meshData.Vertices.ToArray();
            m_mesh.triangles = meshData.Indices.ToArray();
            m_mesh.RecalculateNormals();
        }
    }
}
