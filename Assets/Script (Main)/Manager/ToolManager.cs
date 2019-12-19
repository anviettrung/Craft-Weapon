using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
	public List<Tool> tools;
	protected Tool activeTool;

	public void HideActiveTool()
	{
		if (activeTool == null)
			return;

		activeTool.gameObject.SetActive(false);
	}

	public void ShowActiveTool()
	{
		if (activeTool == null)
			return;

		activeTool.gameObject.SetActive(true);
	}

	public Tool GetActiveTool()
	{
		return activeTool;
	}

	public void ChangeTool(string name)
	{
		if (name == "Empty") {
			// turn off
			//activeTool.gameObject.SetActive(false);
			if (activeTool != null)
				activeTool.TriggerGoOutAnimation();
			activeTool = null;

			return;
		}

		foreach (Tool tool in tools) {
			if (tool.toolName == name) {

				if (activeTool != null)
					//activeTool.gameObject.SetActive(false);
					activeTool.TriggerGoOutAnimation();

				activeTool = tool;
				activeTool.gameObject.SetActive(true);
				activeTool.TriggerGoInAnimation();

				return;
			}

		}
	}

	public Tool GetTool(string name)
	{
		foreach (Tool tool in tools) {
			if (tool.toolName == name)
				return tool;
		}

		return null;
	}
}
