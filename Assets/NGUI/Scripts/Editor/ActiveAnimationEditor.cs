//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2018 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActiveAnimation))]
public class ActiveAnimationEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		NGUIEditorTools.SetLabelWidth(80f);
		ActiveAnimation aa = target as ActiveAnimation;
		GUILayout.Space(3f);
		NGUIEditorTools.DrawEvents("On Finished", aa, aa.onFinished);
	}
}
