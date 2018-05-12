//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIAnchor))]
public class UIAnchorEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();
		EditorGUILayout.HelpBox("All NGUI widgets have anchoring functionality built-in.", MessageType.Info);
	}
}
