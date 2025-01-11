using UnityEngine;

public class Noise
{
    public float[] CreatePerlinNoise(in int width, in int height)
    {
        float[] noise = new float[width * height];

        for (float y = 0; y < height; ++y)
            for (float x = 0; x < width; ++x)
                noise[(int)y * width + (int)x] = Mathf.PerlinNoise(x / width, y / height);

        return noise;
    }
}
