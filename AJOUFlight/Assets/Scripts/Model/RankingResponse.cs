using System;
using System.Collections.Generic;

namespace Models
{
	[Serializable]
	public class RankingResponse
	{
		public int code;
		public string message;
		public RankingData data;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}