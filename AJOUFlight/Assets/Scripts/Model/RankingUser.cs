using System;

namespace Models
{
	[Serializable]
	public class RankingUser
	{
		public string userId;
		public int score;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}
