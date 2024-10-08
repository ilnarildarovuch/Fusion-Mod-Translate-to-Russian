using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class PrizeMgr : MonoBehaviour
{
	// Token: 0x060003AA RID: 938 RVA: 0x0001C52F File Offset: 0x0001A72F
	private void Start()
	{
		this.startPosition = base.transform.position;
		this.velocity = new Vector2(-this.horizontalSpeed, this.verticalSpeed);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001C560 File Offset: 0x0001A760
	private void Update()
	{
		if (!this.isLand)
		{
			this.velocity.y = this.velocity.y - this.gravity * Time.deltaTime;
			base.transform.Translate(this.velocity * Time.deltaTime);
			if (base.transform.position.y < this.startPosition.y - 0.5f)
			{
				this.isLand = true;
			}
		}
		if (Input.GetMouseButtonDown(0) && !this.isClicked)
		{
			this.Click();
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
	private void Click()
	{
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
		{
			if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject == base.gameObject)
			{
				GameObject theItemOnMouse = Mouse.Instance.theItemOnMouse;
				if (theItemOnMouse != null)
				{
					Object.Destroy(theItemOnMouse);
				}
				switch (GameAPP.theBoardType)
				{
				case 0:
					GameAPP.advLevelCompleted[GameAPP.theBoardLevel] = true;
					break;
				case 1:
					GameAPP.clgLevelCompleted[GameAPP.theBoardLevel] = true;
					break;
				case 2:
					GameAPP.gameLevelCompleted[GameAPP.theBoardLevel] = true;
					break;
				case 3:
					GameAPP.survivalLevelCompleted[GameAPP.theBoardLevel] = true;
					break;
				}
				base.StartCoroutine(this.MoveAndScaleObject());
				GameAPP.ChangeMusic(14);
				base.transform.GetChild(1).gameObject.SetActive(true);
				this.isLand = true;
				this.isClicked = true;
			}
		}
		SaveInfo.Instance.SavePlayerData();
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001C71C File Offset: 0x0001A91C
	public void GoBack()
	{
		SaveInfo.Instance.SavePlayerData();
		Object.Destroy(GameAPP.board);
		foreach (object obj in GameAPP.canvasUp.transform)
		{
			Transform transform = (Transform)obj;
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		if (GameAPP.board.GetComponent<Board>().isIZ)
		{
			Object.Destroy(GameObject.Find("InGameUIIZE"));
		}
		else
		{
			Object.Destroy(GameAPP.board.GetComponent<InitBoard>().theInGameUI);
		}
		UIMgr.EnterAdvantureMenu();
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
	private IEnumerator MoveAndScaleObject()
	{
		Vector3 initialPosition = base.transform.localPosition;
		Vector3 initialScale = base.transform.localScale;
		Vector3 targetPosition = new Vector3(10f, -3f, base.transform.position.z);
		Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
		float moveSpeed = 0.8f;
		float t = 0f;
		while (t < 1f)
		{
			base.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
			base.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
			t += Time.deltaTime * moveSpeed;
			yield return null;
		}
		if (this.isTrophy)
		{
			base.StartCoroutine(this.LightOutT());
		}
		else
		{
			base.StartCoroutine(this.LightOut());
		}
		GameAPP.PlaySound(37, 0.5f);
		base.Invoke("GoBack", 3f);
		yield break;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001C7E7 File Offset: 0x0001A9E7
	private IEnumerator LightOut()
	{
		Vector3 initialScale = base.transform.localScale;
		Vector3 targetScale = new Vector3(400f, 400f, 400f);
		float speed = 0.25f;
		Color col = base.transform.gameObject.GetComponent<SpriteRenderer>().color;
		float t = 0f;
		while (t < 1f)
		{
			base.transform.GetChild(2).transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
			col.a -= Time.deltaTime * speed;
			base.GetComponent<SpriteRenderer>().color = col;
			if (base.transform.childCount != 0)
			{
				base.transform.GetChild(0).GetComponent<TextMeshPro>().color = col;
				base.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = col;
			}
			t += Time.deltaTime * speed;
			yield return null;
		}
		yield break;
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0001C7F6 File Offset: 0x0001A9F6
	private IEnumerator LightOutT()
	{
		Vector3 initialScale = base.transform.localScale;
		Vector3 targetScale = new Vector3(400f, 400f, 400f);
		float speed = 0.25f;
		Color col = base.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
		float t = 0f;
		while (t < 1f)
		{
			base.transform.GetChild(2).transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
			col.a -= Time.deltaTime * speed;
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().color = col;
			t += Time.deltaTime * speed;
			yield return null;
		}
		yield break;
	}

	// Token: 0x040001CF RID: 463
	private float horizontalSpeed = 1.5f;

	// Token: 0x040001D0 RID: 464
	private float verticalSpeed = 4f;

	// Token: 0x040001D1 RID: 465
	private float gravity = 9.8f;

	// Token: 0x040001D2 RID: 466
	private Vector2 velocity;

	// Token: 0x040001D3 RID: 467
	private Vector2 startPosition;

	// Token: 0x040001D4 RID: 468
	private bool isLand;

	// Token: 0x040001D5 RID: 469
	private bool isClicked;

	// Token: 0x040001D6 RID: 470
	public bool isTrophy;
}
