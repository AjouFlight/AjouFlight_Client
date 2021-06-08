using System;

namespace Models
{
	[Serializable]
	public class StoreFlight
	{
		public int flightId;
		public double money;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}
