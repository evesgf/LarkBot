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
using UnityEngine.UI;

#endregion // Namespaces

// ######################################################################
// GSui class
// Handles animation speed and auto animation of all GAui elements in the scene.
// ######################################################################

#region GSui

public class GSui : GUIAnimSystem
{

	#region Variables
	
	#endregion // Variables
	
	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################
	
	#region MonoBehaviour
		
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	//void Start ()
	public override void GUIAnimSystemStart()
	{
		// ########################################
		// PERFORM YOUR SCRIPTS


		// ########################################
	}
		
	// Update is called every frame, if the MonoBehaviour is enabled.
	//void Update ()
	public override void GUIAnimSystemUpdate()
	{
		// ########################################
		// PERFORM YOUR SCRIPTS


		// ########################################
	}
	
	#endregion // MonoBehaviour

}

#endregion
