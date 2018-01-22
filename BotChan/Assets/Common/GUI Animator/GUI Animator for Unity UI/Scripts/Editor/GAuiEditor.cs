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
using UnityEditor;
using System.Collections;

#endregion // Namespaces

// ######################################################################
// GAuiEditor class
// Custom editor for GAui component
// ######################################################################

// http://docs.unity3d.com/ScriptReference/CustomEditor.html
// http://docs.unity3d.com/ScriptReference/CustomEditor-ctor.html
// http://unity3d.com/learn/tutorials/modules/intermediate/editor/adding-buttons-to-inspector
[CustomEditor(typeof(GAui))]
// http://docs.unity3d.com/ScriptReference/Editor.html
public class GAuiEditor : GUIAnimEditor
{
	#region Variables
	
	#endregion // Variables
	
	// ########################################
	// Editor Functions
	// http://docs.unity3d.com/ScriptReference/Editor.html
	// ########################################
	
	#region Editor functions

	// This function is called when the object is loaded
	// http://docs.unity3d.com/ScriptReference/ScriptableObject.OnEnable.html
	//public override void OnEnable()
	//{
	//	base.OnEnable();

		// ########################################
		//*** PERFORM YOUR EDITOR SCRIPTS HERE ***//


		// ########################################
	//}
	
	// Implement this function to make a custom inspector
	// http://docs.unity3d.com/ScriptReference/Editor.OnInspectorGUI.html
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		// ########################################
		//*** PERFORM YOUR EDITOR SCRIPTS HERE ***//


		// ########################################
	}

	#endregion // Editor functions
}
