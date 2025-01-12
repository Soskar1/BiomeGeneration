using UnityEngine;

namespace Core.BiomeGeneration
{
    public class TerrainGeneration : MonoBehaviour
    {
        [SerializeField] private int m_chunkSize;
        [SerializeField] private int m_maxHeight;
        [SerializeField] private NoiseData m_noiseData;
        [SerializeField] private GameObject m_chunkPrefab;

        private void Start()
        {
            GenerateTerrain(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
        }

        public void GenerateTerrain(in Vector3Int position)
        {
            ChunkGeneration chunkGeneration = new ChunkGeneration(m_noiseData);
            MeshData meshData = chunkGeneration.Generate(position, m_chunkSize, m_maxHeight);
            
            GameObject chunkInstance = Instantiate(m_chunkPrefab, position, Quaternion.identity);
            chunkInstance.transform.parent = transform;

            Mesh mesh = new Mesh();
            mesh.vertices = meshData.Vertices.ToArray();
            mesh.triangles = meshData.Indices.ToArray();
            mesh.RecalculateNormals();

            chunkInstance.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
