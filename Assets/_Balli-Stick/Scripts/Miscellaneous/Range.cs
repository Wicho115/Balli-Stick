using System;
using UnityEngine;

namespace _Balli_Stick.Miscellaneous
{
    [Serializable]
    public struct Range
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }

        public Range(float min, float max)
        {
            (Min, Max) = (min, max);
        }
    }

    [Serializable]
    public struct IntRange
    {
        [field: SerializeField] public int Min { get; set; }
        [field: SerializeField] public int Max { get; set; }

        public IntRange(int min, int max)
        {
            (Min, Max) = (min, max);
        }
    }
}