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
// OpenOtherScene class
// This class handles 8 buttons for changing scene.
// ######################################################################

public class OpenOtherScene : MonoBehaviour
{
	
	// ########################################
	// MonoBehaviour Functions
	// ########################################
	
	#region MonoBehaviour
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start () {		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update () {		
	}
	
	#endregion // MonoBehaviour
	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder
	
	// Open Demo Scene 1
	public void ButtonOpenDemoScene1 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo01 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 2
	public void ButtonOpenDemoScene2 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo02 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 3
	public void ButtonOpenDemoScene3 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo03 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 4
	public void ButtonOpenDemoScene4 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo04 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 5
	public void ButtonOpenDemoScene5 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo05 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 6
	public void ButtonOpenDemoScene6 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo06 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 7
	public void ButtonOpenDemoScene7 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo07 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	// Open Demo Scene 8
	public void ButtonOpenDemoScene8 ()
	{
		// Disable all buttons
		GSui.Instance.EnableAllButtons(false);

		// Waits 1.5 secs for Moving Out animation then load next level
		GSui.Instance.LoadLevel("GA UUI - Demo08 (960x600px)", 1.5f);
		
		gameObject.SendMessage("HideAllGUIs");
	}
	
	#endregion // UI Responder
}
