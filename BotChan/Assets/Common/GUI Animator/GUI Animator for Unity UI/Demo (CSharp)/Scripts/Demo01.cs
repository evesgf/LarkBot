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
// Demo01 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo01 (960x600px)" scene.
// ######################################################################

public class Demo01 : MonoBehaviour
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
	
	// GAui objects of TopLeft buttons
	public GAui m_TopLeft_A;
	public GAui m_TopLeft_B;
	
	// GAui objects of BottomLeft buttons
	public GAui m_BottomLeft_A;
	public GAui m_BottomLeft_B;
	
	// GAui objects of RightBar buttons
	public GAui m_RightBar_A;
	public GAui m_RightBar_B;
	public GAui m_RightBar_C;

	// Toggle state of TopLeft, BottomLeft and BottomLeft buttons
	bool m_TopLeft_IsOn = false;
	bool m_BottomLeft_IsOn = false;
	bool m_RightBar_IsOn = false;
	
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
		
		// MoveIn all primary buttons
		StartCoroutine(MoveInPrimaryButtons());
	}
	
	// MoveIn all primary buttons
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// MoveIn all primary buttons
		m_TopLeft_A.MoveIn(GSui.eGUIMove.Self);
		m_BottomLeft_A.MoveIn(GSui.eGUIMove.Self);
		m_RightBar_A.MoveIn(GSui.eGUIMove.Self);

		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}

	// MoveOut all primary buttons
	public void HideAllGUIs()
	{
		m_TopLeft_A.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_BottomLeft_A.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_RightBar_A.MoveOut(GSui.eGUIMove.Self);
		
		if(m_TopLeft_IsOn == true)
			m_TopLeft_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_BottomLeft_IsOn == true)
			m_BottomLeft_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_RightBar_IsOn == true)
			m_RightBar_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
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

	public void OnButton_TopLeft()
	{
		// Disable m_TopLeft_A, m_RightBar_A, m_RightBar_C, m_BottomLeft_A for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));

		// Toggle m_TopLeft
		ToggleTopLeft();

		// Toggle other buttons
		if(m_BottomLeft_IsOn==true)
		{
			ToggleBottomLeft();
		}
		if(m_RightBar_IsOn==true)
		{
			ToggleRightBar();
		}
	}

	public void OnButton_BottomLeft()
	{
		// Disable m_TopLeft_A, m_RightBar_A, m_RightBar_C, m_BottomLeft_A for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));

		// Toggle m_BottomLeft
		ToggleBottomLeft();
		
		// Toggle other buttons
		if(m_TopLeft_IsOn==true)
		{
			ToggleTopLeft();
		}
		if(m_RightBar_IsOn==true)
		{
			ToggleRightBar();
		}
		
	}
	
	public void OnButton_RightBar()
	{
		// Disable m_TopLeft_A, m_RightBar_A, m_RightBar_C, m_BottomLeft_A for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_TopLeft_A.gameObject, 0.3f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_A.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_RightBar_C.gameObject, 0.6f));
		StartCoroutine(DisableButtonForSeconds(m_BottomLeft_A.gameObject, 0.3f));

		// Toggle m_RightBar
		ToggleRightBar();
		
		// Toggle other buttons
		if(m_TopLeft_IsOn==true)
		{
			ToggleTopLeft();
		}
		if(m_BottomLeft_IsOn==true)
		{
			ToggleBottomLeft();
		}

	}
	
	#endregion // UI Responder
	
	// ########################################
	// Toggle button functions
	// ########################################
	
	#region Toggle Button

	// Toggle TopLeft buttons
	void ToggleTopLeft()
	{
		m_TopLeft_IsOn = !m_TopLeft_IsOn;
		if(m_TopLeft_IsOn==true)
		{
			// m_TopLeft_B moves in
			m_TopLeft_B.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_TopLeft_B moves out
			m_TopLeft_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle BottomLeft buttons
	void ToggleBottomLeft()
	{
		m_BottomLeft_IsOn = !m_BottomLeft_IsOn;
		if(m_BottomLeft_IsOn==true)
		{
			// m_BottomLeft_B moves in
			m_BottomLeft_B.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_BottomLeft_B moves out
			m_BottomLeft_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle RightBar buttons
	void ToggleRightBar()
	{
		m_RightBar_IsOn = !m_RightBar_IsOn;
		if(m_RightBar_IsOn==true)
		{
			// m_RightBar_A moves out
			m_RightBar_A.MoveOut(GSui.eGUIMove.SelfAndChildren);
			// m_RightBar_B moves in
			m_RightBar_B.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_RightBar_A moves in
			m_RightBar_A.MoveIn(GSui.eGUIMove.SelfAndChildren);
			// m_RightBar_B moves out
			m_RightBar_B.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	#endregion // Toggle Button
}