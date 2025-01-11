using UnityEngine;
using UnityEngine.UI;

public class NoiseVisualization : MonoBehaviour
{
    [SerializeField] private int m_width;
    [SerializeField] private int m_height;
    [SerializeField] private int m_xOffset;
    [SerializeField] private int m_yOffset;
    [SerializeField] private float m_scale;

    private RawImage m_image;
    private Noise m_noise;

    private void Awake() => m_image = GetComponent<RawImage>();

    private void Start() => m_noise = new Noise();
    
    [ContextMenu("CreatePerlinNoiseSample")]
    public void GenerateNoise()
    {
        float[] noise = m_noise.CreateNoiseSample(Mathf.PerlinNoise, m_width, m_height, m_scale, m_xOffset, m_yOffset);
        Visualize(noise);
    }

    [ContextMenu("OctavePerlinNoise")]
    public void GenerateOctavePerlinNoise()
    {
        float OctavePerlinNoise(float x, float y) => m_noise.OctavePerlinNoise(x, y, 6, 0.5f);
        float[] noise = m_noise.CreateNoiseSample(OctavePerlinNoise, m_width, m_height, m_scale, m_xOffset, m_yOffset);
        Visualize(noise);
    }

    private void Visualize(in float[] noise)
    {
        Texture2D noiseTexture = new Texture2D(m_width, m_height);
        Color[] pixels = new Color[m_width * m_height];

        for (int y = 0; y < m_height; ++y)
        {
            for (int x = 0; x < m_width; ++x)
            {
                float noiseValue = noise[y * m_width + x];
                pixels[y * m_width + x] = new Color(noiseValue, noiseValue, noiseValue);
            }
        }

        noiseTexture.SetPixels(pixels);
        noiseTexture.Apply();
        m_image.material.mainTexture = noiseTexture;
        m_image.texture = noiseTexture;
    }
}
