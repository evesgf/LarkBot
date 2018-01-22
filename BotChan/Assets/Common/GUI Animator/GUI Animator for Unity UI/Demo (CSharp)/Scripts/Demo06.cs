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
// Demo06 class
// - Animates all GAui elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA UUI - Demo06 (960x600px)" scene.
// ######################################################################

public class Demo06 : MonoBehaviour
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
	
	// GAui objects of primary buttons
	public GAui m_PrimaryButton1;
	public GAui m_PrimaryButton2;
	public GAui m_PrimaryButton3;
	public GAui m_PrimaryButton4;
	public GAui m_PrimaryButton5;

	// GAui objects of secondary buttons
	public GAui m_SecondaryButton1;
	public GAui m_SecondaryButton2;
	public GAui m_SecondaryButton3;
	public GAui m_SecondaryButton4;
	public GAui m_SecondaryButton5;
	
	// Toggle state of buttons
	bool m_Button1_IsOn = false;
	bool m_Button2_IsOn = false;
	bool m_Button3_IsOn = false;
	bool m_Button4_IsOn = false;
	bool m_Button5_IsOn = false;
	
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

		// Enable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GSui.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}
	
	// MoveIn all primary buttons
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn all primary buttons
		m_PrimaryButton1.MoveIn(GSui.eGUIMove.Self);	
		m_PrimaryButton2.MoveIn(GSui.eGUIMove.Self);	
		m_PrimaryButton3.MoveIn(GSui.eGUIMove.Self);	
		m_PrimaryButton4.MoveIn(GSui.eGUIMove.Self);

		m_PrimaryButton5.MoveIn(GSui.eGUIMove.Self);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	// MoveOut all primary buttons
	public void HideAllGUIs()
	{
		// MoveOut all primary buttons
		m_PrimaryButton1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_PrimaryButton2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_PrimaryButton3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_PrimaryButton4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		m_PrimaryButton5.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
		// MoveOut all secondary buttons
		if(m_Button1_IsOn == true)
			m_SecondaryButton1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Button2_IsOn == true)
			m_SecondaryButton2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Button3_IsOn == true)
			m_SecondaryButton3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Button4_IsOn == true)
			m_SecondaryButton4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		if(m_Button5_IsOn == true)
			m_SecondaryButton5.MoveOut(GSui.eGUIMove.SelfAndChildren);
		
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
	
	public void OnButton_1()
	{
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));

		// Toggle m_Button1
		ToggleButton_1();
		
		// Toggle other buttons
		if(m_Button2_IsOn==true)
		{
			ToggleButton_2();
		}
		if(m_Button3_IsOn==true)
		{
			ToggleButton_3();
		}
		if(m_Button4_IsOn==true)
		{
			ToggleButton_4();
		}
		if(m_Button5_IsOn==true)
		{
			ToggleButton_5();
		}
	}
	
	public void OnButton_2()
	{
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));

		// Toggle m_Button2
		ToggleButton_2();
		
		// Toggle other buttons
		if(m_Button1_IsOn==true)
		{
			ToggleButton_1();
		}
		if(m_Button3_IsOn==true)
		{
			ToggleButton_3();
		}
		if(m_Button4_IsOn==true)
		{
			ToggleButton_4();
		}
		if(m_Button5_IsOn==true)
		{
			ToggleButton_5();
		}
	}
	
	public void OnButton_3()
	{
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));

		// Toggle m_Button3
		ToggleButton_3();
		
		// Toggle other buttons
		if(m_Button1_IsOn==true)
		{
			ToggleButton_1();
		}
		if(m_Button2_IsOn==true)
		{
			ToggleButton_2();
		}
		if(m_Button4_IsOn==true)
		{
			ToggleButton_4();
		}
		if(m_Button5_IsOn==true)
		{
			ToggleButton_5();
		}
	}
	
	public void OnButton_4()
	{
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));

		// Toggle m_Button4
		ToggleButton_4();
		
		// Toggle other buttons
		if(m_Button1_IsOn==true)
		{
			ToggleButton_1();
		}
		if(m_Button2_IsOn==true)
		{
			ToggleButton_2();
		}
		if(m_Button3_IsOn==true)
		{
			ToggleButton_3();
		}
		if(m_Button5_IsOn==true)
		{
			ToggleButton_5();
		}
	}
	
	public void OnButton_5()
	{
		// Disable all buttons for a few seconds
		StartCoroutine(DisableAllButtonsForSeconds(0.6f));

		// Toggle m_Button5
		ToggleButton_5();
		
		// Toggle other buttons
		if(m_Button1_IsOn==true)
		{
			ToggleButton_1();
		}
		if(m_Button2_IsOn==true)
		{
			ToggleButton_2();
		}
		if(m_Button3_IsOn==true)
		{
			ToggleButton_3();
		}
		if(m_Button4_IsOn==true)
		{
			ToggleButton_4();
		}
	}
	
	#endregion // UI Responder
	
	// ########################################
	// Toggle button functions
	// ########################################
	
	#region Toggle Button
	
	// Toggle m_Button1
	void ToggleButton_1()
	{
		m_Button1_IsOn = !m_Button1_IsOn;
		if(m_Button1_IsOn==true)
		{
			// MoveIn m_SecondaryButton1
			m_SecondaryButton1.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// MoveOut m_SecondaryButton1
			m_SecondaryButton1.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Button2
	void ToggleButton_2()
	{
		m_Button2_IsOn = !m_Button2_IsOn;
		if(m_Button2_IsOn==true)
		{
			// MoveIn m_SecondaryButton2
			m_SecondaryButton2.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// MoveOut m_SecondaryButton2
			m_SecondaryButton2.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Button3
	void ToggleButton_3()
	{
		m_Button3_IsOn = !m_Button3_IsOn;
		if(m_Button3_IsOn==true)
		{
			// MoveIn m_SecondaryButton3
			m_SecondaryButton3.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// MoveOut m_SecondaryButton3
			m_SecondaryButton3.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Button4
	void ToggleButton_4()
	{
		m_Button4_IsOn = !m_Button4_IsOn;
		if(m_Button4_IsOn==true)
		{
			// MoveIn m_SecondaryButton4
			m_SecondaryButton4.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// MoveOut m_SecondaryButton4
			m_SecondaryButton4.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	// Toggle m_Button5
	void ToggleButton_5()
	{
		m_Button5_IsOn = !m_Button5_IsOn;
		if(m_Button5_IsOn==true)
		{
			// MoveIn m_SecondaryButton5
			m_SecondaryButton5.MoveIn(GSui.eGUIMove.SelfAndChildren);
		}
		else
		{
			// MoveOut m_SecondaryButton5
			m_SecondaryButton5.MoveOut(GSui.eGUIMove.SelfAndChildren);
		}
	}
	
	#endregion // Toggle Button
}
