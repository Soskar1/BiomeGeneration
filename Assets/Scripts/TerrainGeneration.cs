using System.Collections.Generic;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class TerrainGeneration
    {
        private readonly int m_chunkSize;
        private readonly ChunkGeneration m_chunkGeneration;
        private readonly Dictionary<Vector3Int, ChunkData> m_cachedChunks;
        
        public TerrainGeneration(in int chunkSize, in int maxHeight, in NoiseData noiseData, in List<Region> regions) 
        {
            m_chunkSize = chunkSize;
            m_chunkGeneration = new ChunkGeneration(chunkSize, maxHeight, noiseData, regions);
        }

        public List<ChunkData> GenerateTerrain(in Vector3Int startPosition, in int distanceFromStart)
        {
            List<ChunkData> meshDatas = new List<ChunkData>();

            int xStart = startPosition.x - m_chunkSize * distanceFromStart;
            int xEnd = startPosition.x + m_chunkSize * distanceFromStart;

            int zStart = startPosition.z - m_chunkSize * distanceFromStart;
            int zEnd = startPosition.z + m_chunkSize * distanceFromStart;

            for (int x = xStart; x < xEnd; x += m_chunkSize - 1)
            {
                for (int z = zStart; z < zEnd; z += m_chunkSize - 1)
                {
                    Vector3Int worldPosition = new Vector3Int(x, startPosition.y, z);
                    ChunkData chunkData;
                    
                    if (m_cachedChunks.ContainsKey(worldPosition))
                    {
                        chunkData = m_cachedChunks[worldPosition];
                    }
                    else
                    {
                        chunkData = m_chunkGeneration.Generate(worldPosition);
                        m_cachedChunks.Add(worldPosition, chunkData);
                    }
                    
                    meshDatas.Add(chunkData);
                }
            }

            return meshDatas;
        }
    }

    [System.Serializable]
    public struct Region
    {
        public string name;
        [Range(0, 1)] public float noiseValue;
        public Color color;
    }
}
