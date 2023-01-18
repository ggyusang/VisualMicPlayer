using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
	public List<GameObject> panelObj;

	private float timeLeft;

	private float visibleCursorTimer = 5f;

	private float cursorPosition;

	private bool catchCursor = true;

	private bool IsOnPanels = false;
	

	public void ActivePanels()
	{
	   if(IsOnPanels)
	   {
			for (int i = 0; i < panelObj.Count; i++)
			{
				panelObj[i].SetActive(false);			
			}
			IsOnPanels = false;
		}
		else
		{
			for (int i = 0; i < panelObj.Count; i++)
			{
				panelObj[i].SetActive(true);
			}
			IsOnPanels = true;
		}
	}

	private void Update()
	{
	if(Input.GetKeyDown(KeyCode.B))
	{
			this.ActivePanels();
	}
	/*	if (catchCursor)
		{
			catchCursor = false;
			cursorPosition = Input.GetAxis("Mouse X");
		}
		if (Input.GetAxis("Mouse X") == cursorPosition)
		{
			MonoBehaviour.print("Mouse stop");
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0f)
			{
				timeLeft = visibleCursorTimer;
				Cursor.visible = false;
				catchCursor = true;
				for (int i = 0; i < panelObj.Count; i++)
				{
					panelObj[i].SetActive(false);
				}
			}
		}
		else
		{
			timeLeft = visibleCursorTimer;
			Cursor.visible = true;
			for (int j = 0; j < panelObj.Count; j++)
			{
				panelObj[j].SetActive(true);
			}
		}*/
	}
}
