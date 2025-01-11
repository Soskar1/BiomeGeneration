using UnityEngine;

public class Noise
{
    public float[] CreatePerlinNoise(in int width, in int height, in float scale = 1.0f, in int xOffset = 0, in int yOffset = 0)
    {
        float[] noise = new float[width * height];

        for (float y = 0; y < height; ++y)
            for (float x = 0; x < width; ++x)
                noise[(int)y * width + (int)x] = Mathf.PerlinNoise(xOffset + x / width * scale, yOffset + y / height * scale);

        return noise;
    }
}
