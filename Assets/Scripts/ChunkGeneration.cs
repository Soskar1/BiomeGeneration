using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkGeneration
    {
        private readonly int m_chunkSize;
        private readonly int m_maxHeight;
        private readonly Noise m_noise;

        public ChunkGeneration(in int chunkSize, in int maxHeight, in NoiseData noiseData)
        {
            m_noise = new Noise(noiseData);
            m_chunkSize = chunkSize;
            m_maxHeight = maxHeight;
        }

        public ChunkData Generate(in Vector3Int position)
        {
            MeshData meshData = new MeshData();
            float[] noise = m_noise.CreateOctavePerlinNoiseSample(m_chunkSize, m_chunkSize, position.x, position.z);

            Vector3 CreateVertex(in int x, in int y)
            {
                float height = noise[x * m_chunkSize + y] * m_maxHeight;
                return new Vector3(x, height, y);
            }

            for (int x = 0; x < m_chunkSize - 1; ++x)
            {
                for (int y = 0; y < m_chunkSize - 1; ++y)
                {
                    Vector3[] vertices = new Vector3[4]
                    {
                        CreateVertex(x, y),
                        CreateVertex(x, y + 1),
                        CreateVertex(x + 1, y + 1),
                        CreateVertex(x + 1, y)
                    };

                    meshData.AddQuad(vertices);
                }
            }

            return new ChunkData(meshData, position);
        }
    }
}