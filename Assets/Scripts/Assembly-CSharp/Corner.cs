using UnityEngine;

public class Corner : MonoBehaviour
{
	public enum Direction
	{
		up = -1,
		down = 1
	}

	public Direction direction;

	public int row;

	public int targetRow;

	public bool forHypono;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<Zombie>(out var component) || component.isChangingRow)
		{
			return;
		}
		if (!forHypono)
		{
			if (component.theZombieRow == row && !component.isMindControlled && Mathf.Abs(component.shadow.transform.position.x - base.transform.position.x) < 1.5f)
			{
				component.ChangeRow(targetRow);
			}
		}
		else if (component.theZombieRow == row && component.isMindControlled && Mathf.Abs(component.shadow.transform.position.x - base.transform.position.x) < 1.5f)
		{
			component.ChangeRow(targetRow);
		}
	}
}
