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
// Demo07 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo07 (960x600px)" scene.
// ######################################################################

public class Demo07 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public Canvas m_Canvas;
	
	// GAui objects of title text
	public GAui m_Title1;
	public GAui m_Title2;
	
	// GAui objects of top and bottom bars
	public GAui m_TopBar;
	public GAui m_BottomBar;
	
	// GAui object of dialogs
	public GAui m_Dialog;
	public GAui m_DialogButtons;
	
	// GAui objects of buttons
	public GAui m_Button1;
	public GAui m_Button2;
	public GAui m_Button3;
	public GAui m_Button4;
	
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
		
		// MoveIn all dialogs and buttons
		StartCoroutine(MoveInPrimaryButtons());
	}
	
	// MoveIn all dialogs and buttons
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn all dialogs
		m_Dialog.MoveIn(GSui.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// MoveIn all buttons
		m_Button1.MoveIn(GSui.eGUIMove.SelfAndChildren);	
		m_Button2.MoveIn(GSui.eGUIMove.SelfAndChildren);	
		m_Button3.MoveIn(GSui.eGUIMove.SelfAndChildren);	
		m_Button4.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	// MoveOut all dialogs and buttons
	public void HideAllGUIs()
	{
		// MoveOut all dialogs
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// MoveOut all buttons
		m_Button1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
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
	IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		// Disable all buttons
		GSui.Instance.EnableButton(GO.transform, false);
		
		yield return new WaitForSeconds(DisableTime);
		
		// Enable all buttons
		GSui.Instance.EnableButton(GO.transform, true);
	}
	
	#endregion // Enable/Disable buttons
	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder

	public void OnButton_1()
	{
		// MoveOut m_Button1
		MoveButtonsOut();
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2.0f));

		// Set next move in of m_Button1 to new position
		StartCoroutine(SetButtonMove(GUIAnim.ePosMove.UpperScreenEdge, GUIAnim.ePosMove.UpperScreenEdge));
	}
	
	public void OnButton_2()
	{
		// MoveOut m_Button2
		MoveButtonsOut();
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2.0f));
		
		// Set next move in of m_Button2 to new position
		StartCoroutine(SetButtonMove(GUIAnim.ePosMove.LeftScreenEdge, GUIAnim.ePosMove.LeftScreenEdge));
	}
	
	public void OnButton_3()
	{
		// MoveOut m_Button3
		MoveButtonsOut();
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2.0f));

		// Set next move in of m_Button3 to new position
		StartCoroutine(SetButtonMove(GUIAnim.ePosMove.RightScreenEdge, GUIAnim.ePosMove.RightScreenEdge));
	}
	
	public void OnButton_4()
	{
		// MoveOut m_Button4
		MoveButtonsOut();
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 2.0f));
		
		// Set next move in of m_Button3 to new position
		StartCoroutine(SetButtonMove(GUIAnim.ePosMove.BottomScreenEdge, GUIAnim.ePosMove.BottomScreenEdge));
	}
	
	public void OnDialogButton()
	{
		// MoveOut m_Dialog
		m_Dialog.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable m_DialogButtons for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_DialogButtons.gameObject, 2.0f));

		// Moves m_Dialog back to screen
		StartCoroutine(DialogMoveIn());
	}
	
	#endregion // UI Responder
	
	// ########################################
	// Move Dialog/Button functions
	// ########################################
	
	#region Move Dialog/Button
	
	// MoveOut all buttons
	void MoveButtonsOut()
	{
		m_Button1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Button4.MoveOut(GSui.eGUIMove.SelfAndChildren);
	}
	
	// Set next move in of all buttons to new position
	IEnumerator SetButtonMove(GUIAnim.ePosMove PosMoveIn, GUIAnim.ePosMove PosMoveOut)
	{
		yield return new WaitForSeconds(2.0f);
		
		// Set next MoveIn position of m_Button1 to PosMoveIn
		m_Button1.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button1
		m_Button1.Reset();
		// MoveIn m_Button1
		m_Button1.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Set next MoveIn position of m_Button2 to PosMoveIn
		m_Button2.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button2
		m_Button2.Reset();
		// MoveIn m_Button2
		m_Button2.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Set next MoveIn position of m_Button3 to PosMoveIn
		m_Button3.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button3
		m_Button3.Reset();
		// MoveIn m_Button3
		m_Button3.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Set next MoveIn position of m_Button4 to PosMoveIn
		m_Button4.m_MoveIn.MoveFrom = PosMoveIn;
		// Reset m_Button4
		m_Button4.Reset();
		// MoveIn m_Button4
		m_Button4.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	// Moves m_Dialog back to screen
	IEnumerator DialogMoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		m_Dialog.MoveIn(GSui.eGUIMove.SelfAndChildren);
		m_DialogButtons.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	#endregion // Move Dialog/Button
}
