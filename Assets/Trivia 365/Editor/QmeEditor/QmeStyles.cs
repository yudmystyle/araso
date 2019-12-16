using UnityEngine;
using UnityEditor;

namespace Assets.QmeXmlEditor.Editor
{
	public class QmeStyles
	{
		public static GUIStyle titleStyle = 
			new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleCenter,
				fontSize = 20,
				fontStyle = FontStyle.Normal
			};


		public static GUIStyle labelStyle =
			new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.UpperLeft,
				fontSize = 14,
				fontStyle = FontStyle.Bold
			};

		public static GUIStyle editTextStyle =
			new GUIStyle(GUI.skin.box)
			{
				alignment = TextAnchor.UpperLeft,
				fontSize = 12
			};

		public static GUIStyle scrollViewText =
			new GUIStyle(GUI.skin.label)
			{
				fontSize = 13,
				fontStyle = FontStyle.Normal
			};


		public static GUIStyle scrollViewTextSelected =
			new GUIStyle(GUI.skin.label)
			{
				fontSize = 13,
				fontStyle = FontStyle.BoldAndItalic,
			};

		public static GUIStyle button =
			new GUIStyle(GUI.skin.button)
			{
				margin = new RectOffset(20, 20, 0, 50),
				fontStyle = FontStyle.Bold
			};
	}
}