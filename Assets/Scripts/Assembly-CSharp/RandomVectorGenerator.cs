using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class RandomVectorGenerator : MonoBehaviour
{
	// Token: 0x0600053C RID: 1340 RVA: 0x0002D500 File Offset: 0x0002B700
	public static Vector2[] GenerateRandomVectors(int numberOfVectorsToGenerate, float minX, float maxX, float minY, float maxY, float minDistance = 1.2f)
	{
		Vector2[] array = new Vector2[numberOfVectorsToGenerate];
		int i = 0;
		int num = 10000;
		while (i < numberOfVectorsToGenerate)
		{
			float x = Random.Range(minX, maxX);
			float y = Random.Range(minY, maxY);
			Vector2 vector = new Vector2(x, y);
			bool flag = true;
			if (num > 0)
			{
				foreach (Vector2 b in array)
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
				array[i] = vector;
				i++;
			}
			num--;
		}
		return array;
	}
}
