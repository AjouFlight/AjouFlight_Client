using System;

namespace Models
{
	[Serializable]
	public class RankingResponse
	{
		public int code;
		public string message;
		public RankingUser[] data;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}