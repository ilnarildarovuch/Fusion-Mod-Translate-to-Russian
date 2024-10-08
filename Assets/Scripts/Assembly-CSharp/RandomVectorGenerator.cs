using UnityEngine;

public class RandomVectorGenerator : MonoBehaviour
{
	public static Vector2[] GenerateRandomVectors(int numberOfVectorsToGenerate, float minX, float maxX, float minY, float maxY, float minDistance = 1.2f)
	{
		Vector2[] array = new Vector2[numberOfVectorsToGenerate];
		int num = 0;
		int num2 = 10000;
		while (num < numberOfVectorsToGenerate)
		{
			float x = Random.Range(minX, maxX);
			float y = Random.Range(minY, maxY);
			Vector2 vector = new Vector2(x, y);
			bool flag = true;
			if (num2 > 0)
			{
				Vector2[] array2 = array;
				foreach (Vector2 b in array2)
				{
					if (Vector2.Distance(vector, b) < minDistance)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				array[num] = vector;
				num++;
			}
			num2--;
		}
		return array;
	}
}
