using UnityEngine;
using UnityEngine.UI;

namespace Core.BiomeGeneration
{
    public class NoiseVisualization : MonoBehaviour
    {
        [SerializeField] private int m_width;
        [SerializeField] private int m_height;
        [SerializeField] private int m_xOffset;
        [SerializeField] private int m_yOffset;
        [SerializeField] private NoiseData m_noiseData;

        private RawImage m_image;
        private Noise m_noise;

        private void Awake() => m_image = GetComponent<RawImage>();

        private void Start() => m_noise = new Noise(m_noiseData);

        [ContextMenu("OctavePerlinNoise")]
        public void GenerateOctavePerlinNoise()
        {
            float[] noise = m_noise.CreateOctavePerlinNoiseSample(m_width, m_height, m_xOffset, m_yOffset);
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
}
