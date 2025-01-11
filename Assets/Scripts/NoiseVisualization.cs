using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class NoiseVisualization : MonoBehaviour
{
    [SerializeField] private int m_width;
    [SerializeField] private int m_height;

    private RawImage m_image;

    private void Awake() => m_image = GetComponent<RawImage>();
    
    [ContextMenu("GenerateNoise")]
    public void GenerateNoise()
    {
        Stopwatch watch = Stopwatch.StartNew();
        float[] noise = new Noise().CreatePerlinNoise(m_width, m_height);
        watch.Stop();
        Debug.Log($"{watch.ElapsedMilliseconds}ms");

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
