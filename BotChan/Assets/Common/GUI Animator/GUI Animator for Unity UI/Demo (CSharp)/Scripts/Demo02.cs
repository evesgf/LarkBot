// GUI Animator for Unity UI
// Version: 1.1.1
// Compatilble: Unity 5.4.0 or higher, more info in Readme.txt file.
//
// Developer:				Gold Experience Team (http://www.ge-team.com)
// Unity Asset Store:		https://www.assetstore.unity3d.com/en/#!/content/28709
// Gold Experience Store:	https://www.ge-team.com/en/products/gui-animator-for-unity-ui/
// Support:					geteamdev@gmail.com
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com.

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion // Namespaces

// ######################################################################
// Demo02 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo02 (960x600px)" scene.
// ######################################################################

public class Demo02 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public Canvas m_Canvas;
	
	// GAui objects of Title text
	public GAui m_Title1;
	public GAui m_Title2;
	
	// GAui objects of Top and bottom
	public GAui m_TopBar;
	public GAui m_BottomBar;
	
	// GAui object of Dialog
	public GAui m_Dialog;
	
	#endregion // Variables
	
	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################
	
	#region MonoBehaviour
	
	// Awake is called when the script instance is being loaded.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	void Awake ()
	{
		if(enabled)
		{
			// Set GSui.Instance.m_AutoAnimation to false in Awake(), let you control all GUI Animator elements in the scene via scripts.
			GSui.Instance.m_AutoAnimation = false;
		}
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start ()
	{
		// MoveIn m_TopBar and m_BottomBar
		m_TopBar.MoveIn(GSui.eGUIMove.SelfAndChildren);
		m_BottomBar.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// MoveIn m_Title1 and m_Title2
		StartCoroutine(MoveInTitleGameObjects());

		// Disable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GSui.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update ()
	{		
	}
	
	#endregion // MonoBehaviour
	
	// ########################################
	// MoveIn/MoveOut functions
	// ########################################
	
	#region MoveIn/MoveOut
	
	// MoveIn m_Title1 and m_Title2
	IEnumerator MoveInTitleGameObjects()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn m_Title1 and m_Title2
		m_Title1.MoveIn(GSui.eGUIMove.Self);
		m_Title2.MoveIn(GSui.eGUIMove.Self);
		
		// MoveIn m_Dialog
		StartCoroutine(ShowDialog());
	}
	
	// MoveIn m_Dialog
	IEnumerator ShowDialog()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn m_Dialog
		m_Dialog.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	// MoveOut m_Dialog
	public void HideAllGUIs()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// MoveOut m_Title1 and m_Title2
		StartCoroutine(HideTitleTextMeshes());
	}
	
	// MoveOut m_Title1 and m_Title2
	IEnumerator HideTitleTextMeshes()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveOut m_Title1 and m_Title2
		m_Title1.MoveOut(GSui.eGUIMove.Self);
		m_Title2.MoveOut(GSui.eGUIMove.Self);
		
		// MoveOut m_TopBar and m_BottomBar
		m_TopBar.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_BottomBar.MoveOut(GSui.eGUIMove.SelfAndChildren);
	}
	
	#endregion // MoveIn/MoveOut
	
	// ########################################
	// Enable/Disable button functions
	// ########################################
	
	#region Enable/Disable buttons
	
	// Enable/Disable all scene switch Coroutine
	IEnumerator EnableAllDemoButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// Enable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GSui.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}

	// Disable all buttons for a few seconds
	IEnumerator DisableAllButtonsForSeconds(float DisableTime)
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);
		
		yield return new WaitForSeconds(DisableTime);
		
		// Enable all buttons
		GSui.Instance.EnableAllButtons(true);
	}
	
	#endregion // Enable/Disable buttons
	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder

	public void OnButton_UpperEdge()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);

		// MoveIn m_Dialog from top
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.UpperScreenEdge));
	}
	
	public void OnButton_LeftEdge()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// MoveIn m_Dialog from left
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.LeftScreenEdge));
	}
	
	public void OnButton_RightEdge()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from right
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.RightScreenEdge));
	}
	
	public void OnButton_BottomEdge()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from bottom
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.BottomScreenEdge));
	}
	
	public void OnButton_UpperLeft()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from upper left
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.UpperLeft));
	}
	
	public void OnButton_UpperRight()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from upper right
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.UpperRight));
	}
	
	public void OnButton_BottomLeft()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from bottom left
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.BottomLeft));
	}
	
	public void OnButton_BottomRight()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from bottom right
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.BottomRight));
	}
	
	public void OnButton_Center()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(2.0f));
		
		// MoveIn m_Dialog from center of screen
		StartCoroutine(DialogMoveIn(GUIAnim.ePosMove.MiddleCenter));
	}
	
	#endregion // UI Responder
	
	// ########################################
	// Move dialog functions
	// ########################################
	
	#region Move Dialog

	// MoveIn m_Dialog by position
	IEnumerator DialogMoveIn(GUIAnim.ePosMove PosMoveIn)
	{
		yield return new WaitForSeconds(1.5f);
		
		//Debug.Log("PosMoveIn="+PosMoveIn);
		switch(PosMoveIn)
		{
			// Set m_Dialog to move in from upper
			case GUIAnim.ePosMove.UpperScreenEdge:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.UpperScreenEdge;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from left
			case GUIAnim.ePosMove.LeftScreenEdge:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.LeftScreenEdge;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from right
			case GUIAnim.ePosMove.RightScreenEdge:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.RightScreenEdge;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from bottom
			case GUIAnim.ePosMove.BottomScreenEdge:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.BottomScreenEdge;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from upper left
			case GUIAnim.ePosMove.UpperLeft:	
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.UpperLeft;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from upper right
			case GUIAnim.ePosMove.UpperRight:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.UpperRight;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from bottom left
			case GUIAnim.ePosMove.BottomLeft:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.BottomLeft;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from bottom right
			case GUIAnim.ePosMove.BottomRight:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.BottomRight;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
			// Set m_Dialog to move in from center
			case GUIAnim.ePosMove.MiddleCenter:
			default:
				m_Dialog.m_MoveIn.MoveFrom = GUIAnim.ePosMove.MiddleCenter;
				m_Dialog.m_MoveOut.MoveTo = GUIAnim.ePosMove.MiddleCenter;
				break;
		}

		// Reset m_Dialog
		m_Dialog.Reset();

		// MoveIn m_Dialog by position
		m_Dialog.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	#endregion //  Move Dialog
}