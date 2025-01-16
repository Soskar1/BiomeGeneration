using System.Collections.Generic;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class ChunkGeneration
    {
        private readonly int m_chunkSize;
        private readonly int m_maxHeight;
        private readonly Noise m_noise;
        private readonly List<Region> m_regions;

        public ChunkGeneration(in int chunkSize, in int maxHeight, in NoiseData noiseData, in List<Region> regions)
        {
            m_noise = new Noise(noiseData);
            m_chunkSize = chunkSize;
            m_maxHeight = maxHeight;
            m_regions = regions;
            m_regions.Sort((x, y) => x.noiseValue.CompareTo(y.noiseValue));
        }

        public ChunkData Generate(in Vector3Int position)
        {
            MeshData meshData = new MeshData();
            float[] noise = m_noise.CreateOctavePerlinNoiseSample(m_chunkSize, m_chunkSize, position.x, position.z);

            for (int y = 0; y < m_chunkSize - 1; ++y)
            {
                for (int x = 0; x < m_chunkSize - 1; ++x)
                {
                    Quad quad = CreateQuad(x, y, noise);
                    meshData.AddQuad(quad);
                }
            }

            return new ChunkData(meshData, position, m_chunkSize);
        }

        private Quad CreateQuad(in int x, in int y, float[] noise)
        {
            float GetNoiseValue(in int x, in int y) => noise[y * m_chunkSize + x];

            Vector3 CreateVertex(in int x, in int y)
            {
                float height = GetNoiseValue(x, y) * m_maxHeight;
                return new Vector3(x, height, y);
            }

            Color GetColor(in int x, in int y)
            {
                int index = 0;
                float noiseValue = GetNoiseValue(x, y);

                while (index + 1 < m_regions.Count)
                {
                    if (noiseValue >= m_regions[index].noiseValue && noiseValue < m_regions[index + 1].noiseValue)
                        return m_regions[index].color;

                    ++index;
                }

                return m_regions[index].color;
            }

            Vector3[] vertices = new Vector3[4]
            {
                CreateVertex(x, y),
                CreateVertex(x, y + 1),
                CreateVertex(x + 1, y + 1),
                CreateVertex(x + 1, y)
            };

            Color[] colors = new Color[4]
            {
                GetColor(x, y),
                GetColor(x, y + 1),
                GetColor(x + 1, y + 1),
                GetColor(x + 1, y)
            };

            return new Quad(vertices, colors);
        }
    }
}