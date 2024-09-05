using System.Collections;
using UnityEngine;

namespace _Project.Utilities
{
	public static class StringUtility
	{
		public static string RemoveBetweenDash(string input)
		{
			int firstDashIndex = input.IndexOf("-");
			int secondDashIndex = input.IndexOf("-", firstDashIndex + 1);

			if (firstDashIndex >= 0 && secondDashIndex > firstDashIndex)
			{
				return input.Remove(firstDashIndex, secondDashIndex - firstDashIndex + 1);
			}

			return input;
		}
	}
}