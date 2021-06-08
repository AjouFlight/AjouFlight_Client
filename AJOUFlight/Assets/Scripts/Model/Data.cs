using UnityEditor;
using UnityEngine;

namespace Models
{
    [SerializeField]
    public class Data
    {
        public string token;
        public int stage;
        public int score;
        public double money;
        public int[] flights;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}