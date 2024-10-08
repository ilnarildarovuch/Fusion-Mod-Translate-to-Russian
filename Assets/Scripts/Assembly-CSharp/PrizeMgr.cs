using System.Collections;
using TMPro;
using UnityEngine;

public class PrizeMgr : MonoBehaviour
{
	private float horizontalSpeed = 1.5f;

	private float verticalSpeed = 4f;

	private float gravity = 9.8f;

	private Vector2 velocity;

	private Vector2 startPosition;

	private bool isLand;

	private bool isClicked;

	public bool isTrophy;

	private void Start()
	{
		startPosition = base.transform.position;
		velocity = new Vector2(0f - horizontalSpeed, verticalSpeed);
	}

	private void Update()
	{
		if (!isLand)
		{
			velocity.y -= gravity * Time.deltaTime;
			base.transform.Translate(velocity * Time.deltaTime);
			if (base.transform.position.y < startPosition.y - 0.5f)
			{
				isLand = true;
			}
		}
		if (Input.GetMouseButtonDown(0) && !isClicked)
		{
			Click();
		}
	}

	private void Click()
	{
		RaycastHit2D[] array = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit2D raycastHit2D = array[i];
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
				StartCoroutine(MoveAndScaleObject());
				GameAPP.ChangeMusic(14);
				base.transform.GetChild(1).gameObject.SetActive(value: true);
				isLand = true;
				isClicked = true;
			}
		}
		SaveInfo.Instance.SavePlayerData();
	}

	public void GoBack()
	{
		SaveInfo.Instance.SavePlayerData();
		Object.Destroy(GameAPP.board);
		foreach (Transform item in GameAPP.canvasUp.transform)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
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
		if (isTrophy)
		{
			StartCoroutine(LightOutT());
		}
		else
		{
			StartCoroutine(LightOut());
		}
		GameAPP.PlaySound(37);
		Invoke("GoBack", 3f);
	}

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
			GetComponent<SpriteRenderer>().color = col;
			if (base.transform.childCount != 0)
			{
				base.transform.GetChild(0).GetComponent<TextMeshPro>().color = col;
				base.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>()
					.color = col;
			}
			t += Time.deltaTime * speed;
			yield return null;
		}
	}

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
	}
}
