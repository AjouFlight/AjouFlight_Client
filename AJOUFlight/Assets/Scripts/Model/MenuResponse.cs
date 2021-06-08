using System;

namespace Models
{
	[Serializable]
	public class MenuResponse
	{
		public int code;
		public Data data;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}