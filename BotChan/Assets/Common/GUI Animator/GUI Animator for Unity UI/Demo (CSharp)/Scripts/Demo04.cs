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
// Demo04 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo04 (960x600px)" scene.
// ######################################################################

public class Demo04 : MonoBehaviour
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
	public GAui m_Dialog1;
	public GAui m_Dialog2;
	public GAui m_Dialog3;
	public GAui m_Dialog4;
	
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
		
		// MoveIn m_Title1 m_Title2
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
	
	// Move In m_Title1 and m_Title2
	IEnumerator MoveInTitleGameObjects()
	{
		yield return new WaitForSeconds(1.0f);
		
		// Move In m_Title1 and m_Title2
		m_Title1.MoveIn(GSui.eGUIMove.Self);
		m_Title2.MoveIn(GSui.eGUIMove.Self);
		
		// MoveIn dialogs
		StartCoroutine(MoveInPrimaryButtons());
	}
	
	// MoveIn dialogs
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn dialogs
		m_Dialog1.MoveIn(GSui.eGUIMove.SelfAndChildren);		
		m_Dialog2.MoveIn(GSui.eGUIMove.SelfAndChildren);		
		m_Dialog3.MoveIn(GSui.eGUIMove.SelfAndChildren);		
		m_Dialog4.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	public void HideAllGUIs()
	{
		// MoveOut dialogs
		m_Dialog1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
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

	// Disable a button for a few seconds
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
	
	public void OnButton_Dialog1()
	{
		// MoveOut m_Dialog1
		m_Dialog1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable m_Dialog1 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2.0f));
		
		// Moves m_Dialog1 back to screen
		StartCoroutine(Dialog1_MoveIn());
	}
	
	public void OnButton_Dialog2()
	{
		// MoveOut m_Dialog2
		m_Dialog2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable m_Dialog2 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2.0f));
		
		// Moves m_Dialog2 back to screen
		StartCoroutine(Dialog2_MoveIn());
	}
	
	public void OnButton_Dialog3()
	{
		// MoveOut m_Dialog3
		m_Dialog3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable m_Dialog3 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2.0f));
		
		// Moves m_Dialog3 back to screen
		StartCoroutine(Dialog3_MoveIn());
	}
	
	public void OnButton_Dialog4()
	{
		// MoveOut m_Dialog4
		m_Dialog4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// Disable m_Dialog4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2.0f));
		
		// Moves m_Dialog4 back to screen
		StartCoroutine(Dialog4_MoveIn());
	}
	
	public void OnButton_MoveOutAllDialogs()
	{
		
		// Disable m_Dialog1, m_Dialog2, m_Dialog3, m_Dialog4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Dialog1.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog2.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog3.gameObject, 2.0f));
		StartCoroutine(DisableButtonForSeconds(m_Dialog4.gameObject, 2.0f));

		if(m_Dialog1.m_MoveOut.Began==false && m_Dialog1.m_MoveOut.Done==false)
		{
			// Move m_Dialog1 out
			m_Dialog1.MoveOut(GSui.eGUIMove.SelfAndChildren);
			// Move m_Dialog1 back to screen with Coroutines
			StartCoroutine(Dialog1_MoveIn());
		}
		if(m_Dialog2.m_MoveOut.Began==false && m_Dialog2.m_MoveOut.Done==false)
		{
			// Move m_Dialog2 out
			m_Dialog2.MoveOut(GSui.eGUIMove.SelfAndChildren);
			// Move m_Dialog2 back to screen with Coroutines
			StartCoroutine(Dialog2_MoveIn());
		}
		if(m_Dialog3.m_MoveOut.Began==false && m_Dialog3.m_MoveOut.Done==false)
		{
			// Move m_Dialog3 out
			m_Dialog3.MoveOut(GSui.eGUIMove.SelfAndChildren);
			// Move m_Dialog3 back to screen with Coroutines
			StartCoroutine(Dialog3_MoveIn());
		}
		if(m_Dialog4.m_MoveOut.Began==false && m_Dialog4.m_MoveOut.Done==false)
		{
			// Move m_Dialog4 out
			m_Dialog4.MoveOut(GSui.eGUIMove.SelfAndChildren);
			// Move m_Dialog4 back to screen with Coroutines
			StartCoroutine(Dialog4_MoveIn());
		}

	}
	
	#endregion // UI Responder
	
	// ########################################
	// Move dialog functions
	// ########################################
	
	#region Move Dialog
	
	// MoveIn m_Dialog1
	IEnumerator Dialog1_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		// Reset children of m_Dialog1
		m_Dialog1.ResetAllChildren();
		
		// Moves m_Dialog1 back to screen
		m_Dialog1.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog2
	IEnumerator Dialog2_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		// Reset children of m_Dialog2
		m_Dialog2.ResetAllChildren();
		
		// Moves m_Dialog1 back to screen
		m_Dialog2.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog3
	IEnumerator Dialog3_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		// Reset children of m_Dialog3
		m_Dialog3.ResetAllChildren();
		
		// Moves m_Dialog1 back to screen
		m_Dialog3.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog4
	IEnumerator Dialog4_MoveIn()
	{
		yield return new WaitForSeconds(1.5f);
		
		// Reset children of m_Dialog4
		m_Dialog4.ResetAllChildren();
		
		// Moves m_Dialog1 back to screen to screen
		m_Dialog4.MoveIn(GSui.eGUIMove.SelfAndChildren);
	}
	
	#endregion // Move Dialog
}
