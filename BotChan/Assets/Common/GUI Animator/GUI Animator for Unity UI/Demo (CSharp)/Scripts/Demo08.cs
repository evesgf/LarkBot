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
// Demo08 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo08 (960x600px)" scene.
// ######################################################################

public class Demo08 : MonoBehaviour
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
	
	// GAui objects of 4 primary buttons
	public GAui m_CenterButtons;
	
	// GAui objects of buttons
	public GAui m_Button1;
	public GAui m_Button2;
	public GAui m_Button3;
	public GAui m_Button4;

	// GAui objects of top, left, right and bottom bars
	public GAui m_Bar1;
	public GAui m_Bar2;
	public GAui m_Bar3;
	public GAui m_Bar4;
	
	// Toggle state of top, left, right and bottom bars
	bool m_Bar1_IsOn = false;
	bool m_Bar2_IsOn = false;
	bool m_Bar3_IsOn = false;
	bool m_Bar4_IsOn = false;
	
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
		m_CenterButtons.MoveIn(GSui.eGUIMove.SelfAndChildren);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	// MoveOut all primary buttons
	public void HideAllGUIs()
	{
		// MoveOut all primary buttons
		m_CenterButtons.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// MoveOut all side bars
		if(m_Bar1_IsOn==true)
			m_Bar1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Bar2_IsOn==true)
			m_Bar2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Bar3_IsOn==true)
			m_Bar3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Bar4_IsOn==true)
			m_Bar4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
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
		// Toggle m_Bar1
		ToggleBar1();
		
		// Toggle other bars
		if(m_Bar2_IsOn==true)
		{
			ToggleBar2();
		}
		if(m_Bar3_IsOn==true)
		{
			ToggleBar3();
		}
		if(m_Bar4_IsOn==true)
		{
			ToggleBar4();
		}
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}
	
	public void OnButton_2()
	{
		// Toggle m_Bar2
		ToggleBar2();
		
		// Toggle other bars
		if(m_Bar1_IsOn==true)
		{
			ToggleBar1();
		}
		if(m_Bar3_IsOn==true)
		{
			ToggleBar3();
		}
		if(m_Bar4_IsOn==true)
		{
			ToggleBar4();
		}
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}
	
	public void OnButton_3()
	{
		// Toggle m_Bar3
		ToggleBar3();
		
		// Toggle other bars
		if(m_Bar1_IsOn==true)
		{
			ToggleBar1();
		}
		if(m_Bar2_IsOn==true)
		{
			ToggleBar2();
		}
		if(m_Bar4_IsOn==true)
		{
			ToggleBar4();
		}
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}
	
	public void OnButton_4()
	{
		// Toggle m_Bar4
		ToggleBar4();
		
		// Toggle other bars
		if(m_Bar1_IsOn==true)
		{
			ToggleBar1();
		}
		if(m_Bar2_IsOn==true)
		{
			ToggleBar2();
		}
		if(m_Bar3_IsOn==true)
		{
			ToggleBar3();
		}
		
		// Disable m_Button1, m_Button2, m_Button3, m_Button4 for a few seconds
		StartCoroutine(DisableButtonForSeconds(m_Button1.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button2.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button3.gameObject, 0.75f));
		StartCoroutine(DisableButtonForSeconds(m_Button4.gameObject, 0.75f));
	}
	
	#endregion // UI Responder
	
	// ########################################
	// Toggle button functions
	// ########################################
	
	#region Toggle Button
	
	// Toggle m_Bar1
	void ToggleBar1()
	{
		m_Bar1_IsOn = !m_Bar1_IsOn;
		if(m_Bar1_IsOn==true)
		{
			// m_Bar1 moves in
			m_Bar1.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_Bar1 moves out
			m_Bar1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Bar2
	void ToggleBar2()
	{
		m_Bar2_IsOn = !m_Bar2_IsOn;
		if(m_Bar2_IsOn==true)
		{
			// m_Bar2 moves in
			m_Bar2.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_Bar2 moves out
			m_Bar2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Bar3
	void ToggleBar3()
	{
		m_Bar3_IsOn = !m_Bar3_IsOn;
		if(m_Bar3_IsOn==true)
		{
			// m_Bar3 moves in
			m_Bar3.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_Bar3 moves out
			m_Bar3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Bar4
	void ToggleBar4()
	{
		m_Bar4_IsOn = !m_Bar4_IsOn;
		if(m_Bar4_IsOn==true)
		{
			// m_Bar4 moves in
			m_Bar4.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// m_Bar4 moves out
			m_Bar4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	#endregion // Toggle Button
}
