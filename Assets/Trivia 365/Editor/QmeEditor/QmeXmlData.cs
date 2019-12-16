using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.QmeXmlEditor.Editor
{
	[Serializable]
	public class QmeXmlData
	{
		public List<Question> questions = new List<Question>();

		public QmeXmlData(List<Question> questions)
		{
			this.questions = questions;
		}

		public static QmeXmlData empty()
		{
			return new QmeXmlData(new List<Question>());
		}

		public static QmeHistory createAssetAsSibling(string fileName)
		{
			var guid = AssetDatabase.FindAssets(fileName);
			var dirPath = AssetDatabase.GUIDToAssetPath(guid[0]);
			var filePath = dirPath + "/Qmehistory.asset";

			var history = AssetDatabase.LoadAssetAtPath<QmeHistory>(filePath);
			if (history != null)
				return history;
			//Debug.Log("creating history file at " + filePath);
			var so = ScriptableObject.CreateInstance<QmeHistory>();
			so.data = empty();
			AssetDatabase.CreateAsset(so, filePath);
			return so;
		}
	}

	[Serializable]
	public class Question
	{
		public string question, imageUrl;
		public Choice[] answers = new Choice[4];

		public Question(string question, string imageUrl, Choice[] answers)
		{
			this.question = question;
			this.imageUrl = imageUrl;
			this.answers = answers;
		}

		public static Question empty(string title)
		{
			return new Question(title, "", new[]
				{
					Choice.firstChoice(),
					Choice.setEmpty(),
					Choice.setEmpty(),
					Choice.setEmpty()
				});
		}
	}

	[Serializable]
	public class Choice
	{
		public bool correct;
		public string answer = "";

		public Choice(bool correct, string answer)
		{
			this.correct = correct;
			this.answer = answer;
		}

		public static Choice setEmpty()
		{
			return new Choice(false, "");
		}

		public static Choice firstChoice()
		{
			return new Choice(true, "");
		}
	}
}