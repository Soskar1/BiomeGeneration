using UnityEngine;

namespace Core.BiomeGeneration
{
    [CreateAssetMenu(fileName = "NoiseData", menuName = "Scriptable Objects/NoiseData")]
    public class NoiseData : ScriptableObject
    {
        public float scale;
        public int octaves;
        public float persistance;
        public float startFrequency;
        public float startAmplitude;
        public float xOffset;
        public float yOffset;
    }
}