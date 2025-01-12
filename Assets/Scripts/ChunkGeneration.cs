using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkGeneration
    {
        private Noise m_noise;

        public ChunkGeneration(NoiseData noiseData) => m_noise = new Noise(noiseData);

        public MeshData Generate(in Vector3Int position, int chunkSize, int maxHeight)
        {
            MeshData meshData = new MeshData();
            float[] noise = m_noise.CreateOctavePerlinNoiseSample(chunkSize, chunkSize, position.x, position.z);

            Vector3 CreateVertex(in int x, in int y)
            {
                float height = noise[x * chunkSize + y] * maxHeight;
                return new Vector3(x, height, y);
            }

            for (int x = 0; x < chunkSize - 1; ++x)
            {
                for (int y = 0; y < chunkSize - 1; ++y)
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

            return meshData;
        }
    }
}