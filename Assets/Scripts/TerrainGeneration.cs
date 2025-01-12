using System.Collections.Generic;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class TerrainGeneration
    {
        private readonly int m_chunkSize;
        private readonly ChunkGeneration m_chunkGeneration;
        
        public TerrainGeneration(in int chunkSize, in int maxHeight, in NoiseData noiseData) 
        {
            m_chunkSize = chunkSize;
            m_chunkGeneration = new ChunkGeneration(chunkSize, maxHeight, noiseData);
        }

        public List<ChunkData> GenerateTerrain(in Vector3Int startPosition, in int distanceFromStart)
        {
            List<ChunkData> meshDatas = new List<ChunkData>();

            int xStart = startPosition.x - m_chunkSize * distanceFromStart / 2;
            int xEnd = startPosition.x + m_chunkSize * distanceFromStart / 2;

            int zStart = startPosition.z - m_chunkSize * distanceFromStart / 2;
            int zEnd = startPosition.z + m_chunkSize * distanceFromStart / 2;

            for (int x = xStart; x < xEnd; x += m_chunkSize)
            {
                for (int z = zStart; z < zEnd; z += m_chunkSize)
                {
                    Vector3Int worldPosition = new Vector3Int(x, startPosition.y, z);
                    ChunkData chunkData = m_chunkGeneration.Generate(worldPosition);
                    meshDatas.Add(chunkData);
                }
            }

            return meshDatas;
        }
    }
}
