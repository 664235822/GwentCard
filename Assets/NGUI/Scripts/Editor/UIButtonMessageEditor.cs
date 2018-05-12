//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIButtonMessage))]
public class UIButtonMessageEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		EditorGUILayout.HelpBox("This is a legacy component. Consider using the Event Trigger instead.", MessageType.Warning);
		base.OnInspectorGUI();
	}
}
