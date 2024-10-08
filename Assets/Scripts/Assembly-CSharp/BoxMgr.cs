using UnityEngine;

public class BoxMgr : MonoBehaviour
{
	public static void SetBox()
	{
		Board instance = Board.Instance;
		instance.boxType[0, 1] = 2;
		instance.boxType[1, 1] = 2;
		instance.boxType[2, 0] = 2;
		instance.boxType[2, 1] = 2;
		instance.boxType[2, 2] = 2;
		instance.boxType[2, 3] = 2;
		instance.boxType[2, 4] = 2;
		instance.boxType[3, 0] = 2;
		instance.boxType[3, 4] = 2;
		instance.boxType[4, 0] = 2;
		instance.boxType[4, 4] = 2;
		instance.boxType[5, 0] = 2;
		instance.boxType[5, 4] = 2;
		instance.boxType[6, 0] = 2;
		instance.boxType[6, 1] = 2;
		instance.boxType[6, 3] = 2;
		instance.boxType[6, 4] = 2;
		instance.boxType[7, 1] = 2;
		instance.boxType[7, 3] = 2;
		instance.boxType[8, 1] = 2;
		instance.boxType[8, 3] = 2;
		instance.boxType[8, 4] = 2;
		instance.boxType[9, 1] = 2;
		instance.boxType[9, 4] = 2;
	}
}
