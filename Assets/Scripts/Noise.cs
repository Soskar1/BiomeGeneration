using System;
using UnityEngine;

namespace Core.BiomeGeneration
{
    public class Noise
    {
        public float OctavePerlinNoise(float x, float y, int octaves, float persistance)
        {
            float CalculateNoise(in int octave = 0, in float frequency = 1, in float amplitude = 1, in float maxValue = 0, float total = 0)
            {
                total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;

                if (octave < octaves)
                    return CalculateNoise(octave + 1, frequency * 2, amplitude * persistance, maxValue + amplitude, total);

                return total / maxValue;
            }

            return CalculateNoise();
        }

        public float[] CreateOctavePerlinNoiseSample(in int width, in int height, in int octaves, in float persistance, in float scale = 1.0f, in int xOffset = 0, in int yOffset = 0)
        {
            float[] noise = new float[width * height];

            for (float y = 0; y < height; ++y)
                for (float x = 0; x < width; ++x)
                    noise[(int)y * width + (int)x] = OctavePerlinNoise(xOffset + x / width * scale, yOffset + y / height * scale, octaves, persistance);

            return noise;
        }
    }
}