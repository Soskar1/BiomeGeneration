using UnityEngine;

namespace Core.BiomeGeneration
{
    public class Noise
    {
        private NoiseData m_noiseData;

        public Noise(in NoiseData data) => m_noiseData = data;

        private float OctavePerlinNoise(float x, float y)
        {
            float CalculateNoise(in int octave = 0, in float frequency = 1, in float amplitude = 1, in float maxValue = 0, float total = 0)
            {
                total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;

                if (octave < m_noiseData.octaves)
                    return CalculateNoise(octave + 1, frequency * 2, amplitude * m_noiseData.persistance, maxValue + amplitude, total);

                return total / maxValue;
            }

            return CalculateNoise(frequency: m_noiseData.startFrequency, amplitude: m_noiseData.startAmplitude);
        }

        public float[] CreateOctavePerlinNoiseSample(in int width, in int height, in int xOffset = 0, in int yOffset = 0)
        {
            float[] noise = new float[width * height];

            for (float y = 0; y < height; ++y)
                for (float x = 0; x < width; ++x)
                    noise[(int)y * width + (int)x] = OctavePerlinNoise(xOffset + x / width * m_noiseData.scale, yOffset + y / height * m_noiseData.scale);

            return noise;
        }
    }
}