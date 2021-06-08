using System;

namespace Models
{
	[Serializable]
	public class StageUser
	{
		public int score;
		public double money;
		public int stage;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}
