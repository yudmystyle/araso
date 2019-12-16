using System;
using UnityEditor;
using UnityEngine;

namespace Assets.QmeXmlEditor.Editor
{
	public static class QmeLayout
	{
		const float 
			TITLE_HEIGHT = 50, 
			LABEL_HEIGHT = 50,
			LARGE_LABEL_HEIGHT = 110,
			BUTTON_HEIGHT = 30,
			ANSWER_HEIGHT = 100,
			SCROLL_WIDTH_FACTOR = 0.35f, 
			LABEL_WIDTH_FACTOR = 0.1f,
			TEXT_WIDTH_FACTOR = 0.5f,
			ANSWER_WIDTH_FACTOR = (1 - SCROLL_WIDTH_FACTOR - 0.1f) / 2;

		static GUILayoutOption
			scrollWidth, titleHeight, scrollHeight, labelWidth, labelHeight, textWidth, textHeight, largeTextHeight,
			answerTextWidth, answerTextHeight, buttonHeight;

		static float? w, h;

		public static void refresh(float width, float height)
		{
			// To avoid unnecessary calculations
			if (w.HasValue && h.HasValue && Math.Abs(w.Value - width) < 1 && Math.Abs(h.Value - height) < 1)
			{
				return;
			}
			w = width;
			h = height;
			scrollWidth = GUILayout.Width(width * SCROLL_WIDTH_FACTOR);
			titleHeight = GUILayout.Height(TITLE_HEIGHT);
			scrollHeight = GUILayout.Height(height - TITLE_HEIGHT - 5);
			labelWidth = GUILayout.Width(width * LABEL_WIDTH_FACTOR);
			//labelHeight = GUILayout.Height(LABEL_HEIGHT);
			textWidth = GUILayout.Width(width * TEXT_WIDTH_FACTOR);
			textHeight = GUILayout.Height(LABEL_HEIGHT);
			largeTextHeight = GUILayout.Height(LARGE_LABEL_HEIGHT);

			answerTextWidth = GUILayout.Width(width * ANSWER_WIDTH_FACTOR);
			answerTextHeight = GUILayout.Height(ANSWER_HEIGHT);
			buttonHeight = GUILayout.Height(BUTTON_HEIGHT);
		}

		public static EditorGUILayout.ScrollViewScope scrollView(Vector2 scrollPos)
		{
			return new EditorGUILayout.ScrollViewScope(
				scrollPos, false, true, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, scrollWidth, scrollHeight
			);
		}

		public enum HeighSelect
		{
			Small,
			Big
		}

		public static void title()
		{
			if (EditorGUIUtility.isProSkin)
				QmeStyles.titleStyle.normal.textColor = Color.white;
			else
				QmeStyles.titleStyle.normal.textColor = Color.black;
			
			GUILayout.Label("Questions", QmeStyles.titleStyle, scrollWidth, titleHeight);
		}

		public static void label(string labelText, HeighSelect x = HeighSelect.Small)
		{
			if (EditorGUIUtility.isProSkin)
				QmeStyles.labelStyle.normal.textColor = Color.white;
			else
				QmeStyles.labelStyle.normal.textColor = Color.black;
			
			if(x == HeighSelect.Small)
				GUILayout.Label(labelText, QmeStyles.labelStyle, labelWidth, textHeight);
			else
				GUILayout.Label(labelText, QmeStyles.labelStyle, labelWidth, largeTextHeight);
		}

		public static string text(string labelText)
		{
			if (EditorGUIUtility.isProSkin)
				QmeStyles.editTextStyle.normal.textColor = Color.white;
			else
				QmeStyles.editTextStyle.normal.textColor = Color.black;
			
			return EditorGUILayout.TextField(labelText, QmeStyles.editTextStyle, textWidth, textHeight);
		}

		public static string largerText(string labelText)
		{
			if (EditorGUIUtility.isProSkin)
				QmeStyles.editTextStyle.normal.textColor = Color.white;
			else
				QmeStyles.editTextStyle.normal.textColor = Color.black;
			
			return EditorGUILayout.TextField(labelText, QmeStyles.editTextStyle, textWidth, largeTextHeight);
		}

		public static void emptyBox()
		{
			GUILayout.Box("", GUIStyle.none, titleHeight);
		}

		public static bool button(string text)
		{
			if (EditorGUIUtility.isProSkin)
				QmeStyles.button.normal.textColor = Color.white;
			else
				QmeStyles.button.normal.textColor = Color.black;
			
			return GUILayout.Button(text, QmeStyles.button, buttonHeight);
		}

		public static void generateAnswers(Choice[] fields)
		{
			using (var h = new EditorGUILayout.HorizontalScope())
			{
				generateAndUpdate(fields, 0);
				generateAndUpdate(fields, 1);
			}
			using (var h = new EditorGUILayout.HorizontalScope())
			{
				generateAndUpdate(fields, 2);
				generateAndUpdate(fields, 3);
			}
		}

		static void generateAndUpdate(Choice[] fields, int idx)
		{
			generateAnswer(fields[idx], correct =>
				{
					foreach (var field in fields)
					{
						if (field != correct)
							field.correct = false;
					}
				});
		}

		static void generateAnswer(Choice data, Action<Choice> correctChanged)
		{
			using (var v = new EditorGUILayout.VerticalScope())
			{
				var correct = toggle(data.correct, " Correct Answer");
				var text = EditorGUILayout.TextField(data.answer, QmeStyles.editTextStyle, answerTextWidth, answerTextHeight);
				if (correct != data.correct && correct)
				{
					if (data.answer == string.Empty)
					{
						EditorUtility.DisplayDialog("Error", "You cannot set a blank field as the correct answer.", "Ok");
						return;
					}
					correctChanged(data);
				}
				data.correct = correct;
				data.answer = text;
			}
		}

		static bool toggle(bool value, string text)
		{
			return GUILayout.Toggle(value, text);
		}
	}
}