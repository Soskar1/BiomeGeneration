using UnityEngine;
using System.Collections.Generic;

namespace Core.BiomeGeneration
{
    public class TerrainRenderer : MonoBehaviour
    {
        [SerializeField] private ChunkRenderer m_chunkPrefab;
        [SerializeField] private int m_chunkSize;
        [SerializeField] private int m_chunkHeight;
        [SerializeField] private NoiseData m_noiseData;
        [SerializeField] private int m_renderDistance;
        [SerializeField] private List<Region> m_regions;

        private TerrainGeneration m_terrainGeneration;

        private void Awake() => m_terrainGeneration = new TerrainGeneration(m_chunkSize, m_chunkHeight, m_noiseData, m_regions);

        [ContextMenu("Create Terrain")]
        public void CreateTerrain()
        {
            Vector3Int worldPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            List<ChunkData> chunkDatas = m_terrainGeneration.GenerateTerrain(worldPosition, m_renderDistance);

            foreach (ChunkData chunkData in chunkDatas)
            {
                ChunkRenderer chunkInstance = Instantiate(m_chunkPrefab, chunkData.WorldPosition, Quaternion.identity);
                chunkInstance.transform.parent = transform;
                chunkInstance.Render(chunkData);
            }
        }
    }
}