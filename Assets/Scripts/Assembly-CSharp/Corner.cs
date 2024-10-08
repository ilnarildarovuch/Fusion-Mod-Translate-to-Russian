using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class Corner : MonoBehaviour
{
	// Token: 0x060003B9 RID: 953 RVA: 0x0001CA54 File Offset: 0x0001AC54
	private void OnTriggerStay2D(Collider2D collision)
	{
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && !zombie.isChangingRow)
		{
			if (!this.forHypono)
			{
				if (zombie.theZombieRow == this.row && !zombie.isMindControlled && Mathf.Abs(zombie.shadow.transform.position.x - base.transform.position.x) < 1.5f)
				{
					zombie.ChangeRow(this.targetRow);
					return;
				}
			}
			else if (zombie.theZombieRow == this.row && zombie.isMindControlled && Mathf.Abs(zombie.shadow.transform.position.x - base.transform.position.x) < 1.5f)
			{
				zombie.ChangeRow(this.targetRow);
			}
		}
	}

	// Token: 0x040001DB RID: 475
	public Corner.Direction direction;

	// Token: 0x040001DC RID: 476
	public int row;

	// Token: 0x040001DD RID: 477
	public int targetRow;

	// Token: 0x040001DE RID: 478
	public bool forHypono;

	// Token: 0x02000142 RID: 322
	public enum Direction
	{
		// Token: 0x04000464 RID: 1124
		up = -1,
		// Token: 0x04000465 RID: 1125
		down = 1
	}
}
