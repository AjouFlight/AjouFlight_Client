using System;

namespace Models
{
	[Serializable]
	public class ServerResponse
    {
		public int code;
		public string message;

        public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}