using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Assets.QmeXmlEditor.Editor
{
	public class QmeXmlEditor : EditorWindow
	{
		public static QmeXmlEditor window;

		private string LastImportPath = string.Empty;

		private string LastExportPath = string.Empty;

		//Change this when changing the name of the class
		const string nameof = "QmeEditor";

		const string dataFileExt = "xml";

		static readonly Vector2
			sizeMax = new Vector2(3840, 650),
			sizeMin = new Vector2(1200, 650);
		static readonly QmeXmlHandler xmlHandler = new QmeXmlHandler();

		static QmeHistory history;
		static Question selectedQuestion;


		[MenuItem("Tools/XML Editor", false, -16)]
		static void Init()
		{
			window = GetWindow<QmeXmlEditor>(true, "QuizMe XML Editor");
			window.minSize = sizeMin;
			window.maxSize = sizeMax;
			window.Show();
		}

		Vector2 scrollPos;

		static void selectFirstOrCreate()
		{
			var data = history.data.questions;
			selectedQuestion = (data.Count > 0) ? data.First() : Question.empty("");
			GUIUtility.keyboardControl = 0;
		}

		static void selectLastOrCreate()
		{
			var data = history.data.questions;
			selectedQuestion = (data.Count > 0) ? data.Last() : Question.empty("");
			GUIUtility.keyboardControl = 0;
		}

		public void OnGUI()
		{
			window = this;

			var scWidth = position.width;
			var scHeight = position.height;
			QmeLayout.refresh(scWidth, scHeight);

			if (history == null)
			{
				history = QmeXmlData.createAssetAsSibling(nameof);
				selectFirstOrCreate();
				EditorUtility.SetDirty(history);
			}

			using (var h = new EditorGUILayout.HorizontalScope())
			{
				EditorGUI.BeginDisabledGroup(history.data.questions.Count == 0);
				using (var v = new EditorGUILayout.VerticalScope())
				{
					QmeLayout.title();
					createScrollView(history.data);
				}

				using (var v = new EditorGUILayout.VerticalScope())
				{
					QmeLayout.emptyBox();


					using (var h2 = new EditorGUILayout.HorizontalScope())
					{
						QmeLayout.label("Question", QmeLayout.HeighSelect.Big);
						selectedQuestion.question = QmeLayout.largerText(selectedQuestion.question);
					}

					GUILayout.Space(10);

					using (var h2 = new EditorGUILayout.HorizontalScope())
					{
						QmeLayout.label("Image", QmeLayout.HeighSelect.Small);
						selectedQuestion.imageUrl = QmeLayout.text(selectedQuestion.imageUrl);

					}
			
					QmeLayout.emptyBox();
					QmeLayout.generateAnswers(selectedQuestion.answers);
					QmeLayout.emptyBox();
					GUILayout.FlexibleSpace();

					EditorGUI.EndDisabledGroup();

					using (var h2 = new EditorGUILayout.HorizontalScope())
					{
						GUI.color = Color.green;
						if (QmeLayout.button("New"))
							newData();
						if (QmeLayout.button("Import"))
							import();
						EditorGUI.BeginDisabledGroup(history.data.questions.Count == 0);
						{
							if (QmeLayout.button("Save"))
								export();
							GUI.color = Color.red;
							if (QmeLayout.button("Delete"))
								delete();
							if (QmeLayout.button("Clear"))
								clear();
							GUI.color = Color.white;
						}
						EditorGUI.EndDisabledGroup();
					}
				}
			}
		}

		void newData()
		{
			var q = Question.empty("Question #" + (history.data.questions.Count + 1));
			history.data.questions.Add(q);
			selectedQuestion = q;
		}

		void import()
		{
			selectFirstOrCreate();

			string path;

			if(LastImportPath.Length>1)
				path = EditorUtility.OpenFilePanel("Open file", LastImportPath, dataFileExt);
			else
				path = EditorUtility.OpenFilePanel("Open file", "Assets/", dataFileExt);
			
			if (path == string.Empty)
				return;

			history.data.questions.Clear();
			history.data = QmeXmlData.empty();

			history.data = xmlHandler.read(path);
			if (history.data.questions.Count > 0)
				loadData(history.data.questions[0]);

			LastImportPath = path;
		}

		void export()
		{
			QmeXmlData data = history.data;

			for (int x = 0; x < data.questions.Count; x++)
			{
				if (data.questions[x].question == string.Empty)
				{
					EditorUtility.DisplayDialog("Error", "Question " + (x + 1).ToString() + " is blank.", "Ok");
					loadData(data.questions[x]);
					return;
				}

				int blankCount = 0;

				for (int y = 0; y < data.questions[x].answers.Length; y++)
				{
					if (data.questions[x].answers[y].correct == true && data.questions[x].answers[y].answer == string.Empty)
					{
						EditorUtility.DisplayDialog("Error", "You cannot set a blank field as the correct answer.", "Ok");
						loadData(data.questions[x]);
						return;
					}
					
					if (data.questions[x].answers[y].answer == string.Empty)
						blankCount++;
				}

				if (blankCount == 1 || blankCount == 3)
				{
					EditorUtility.DisplayDialog("Error", "Question " + (x + 1).ToString() + " has an invalid number of answers.", "Ok");
					Debug.LogError("QuizMe supports 2 or 4 answer questions.\n\nQuestion " + (x + 1).ToString() + " has " + (4 - blankCount).ToString() + " answer(s).\n\n");
					loadData(data.questions[x]);
					return;
				}

				Question que = data.questions[x];

				if (que.answers[0].correct == false && que.answers[1].correct == false && que.answers[2].correct == false && que.answers[3].correct == false)
				{
					EditorUtility.DisplayDialog("Error", "Question " + (x + 1).ToString() + " has all answers set to false.\n\n Select the correct answer.", "Ok");
					loadData(data.questions[x]);
					return;
				}

			}

			string path;

			if(LastExportPath.Length>1)
				path = EditorUtility.SaveFilePanel("Save file", LastExportPath, "", dataFileExt);
			else
				path = EditorUtility.SaveFilePanel("Save file", "Assets/", "", dataFileExt);
			
			if (path == string.Empty)
				return;

			string content = xmlHandler.write(path, history.data);

			File.WriteAllText(path, content);

			AssetDatabase.Refresh();

			LastExportPath = path;
		}

		void delete()
		{
			if (EditorUtility.DisplayDialog("Delete question", "Are you sure you want to delete this question?\n\nYou can't undo this action.", "Yes", "No"))
			{
				history.data.questions.Remove(selectedQuestion);
				selectLastOrCreate();
			}
		}

		void clear()
		{
			if (EditorUtility.DisplayDialog("Delete ALL questions", "Are you sure you want to delete ALL the questions?\n\nYou can't undo this action.", "Yes", "No"))
			{
				history.data = QmeXmlData.empty();
				selectFirstOrCreate();
			}
		}

		void loadData(Question question)
		{
			selectedQuestion = question;
			GUIUtility.keyboardControl = 0;
		}

		void createScrollView(QmeXmlData data)
		{
			if (EditorGUIUtility.isProSkin)
			{
				QmeStyles.scrollViewText.normal.textColor = Color.white;
				QmeStyles.scrollViewTextSelected.normal.textColor = Color.cyan;
			}
			else
			{
				QmeStyles.scrollViewText.normal.textColor = Color.black;
				QmeStyles.scrollViewTextSelected.normal.textColor = Color.blue;
			}
			
			int cur = 1;

			using (var scrollView = QmeLayout.scrollView(scrollPos))
			{
				scrollPos = scrollView.scrollPosition;
				scrollView.handleScrollWheel = true;
				foreach (var question in data.questions)
				{
					var buttonStyle = question == selectedQuestion ? QmeStyles.scrollViewTextSelected : QmeStyles.scrollViewText;
					var buttonText = new GUIContent(cur + ". " + question.question);
					var rect = GUILayoutUtility.GetRect(buttonText, buttonStyle);

					if (GUI.Button(rect, buttonText, buttonStyle))
					{
						loadData(question);
					}
					cur++;
				}
			}
		}
	}
}
