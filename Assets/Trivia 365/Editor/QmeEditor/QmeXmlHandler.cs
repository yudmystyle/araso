using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Assets.QmeXmlEditor.Editor
{
	class QmeXmlHandler
	{
		const string QUESTION_DB = "QuestionDatabase";
		const string QUESTIONS = "Questions";
		const string QUESTION = "Question";
		const string IMAGE = "Image";
		const string ANSWERS = "Answers";
		const string CHOICES = "Choices";
		const string CORRECT = "correct";

		readonly XmlWriterSettings settings;

		public QmeXmlHandler()
		{
			settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
		}

		public QmeXmlData read(string path)
		{
			var doc = XDocument.Load(path);
			var questions = 
				(from q in doc.Descendants(QUESTIONS).ToList()
				     select new Question(
					         q.Element(QUESTION).Value,
					         q.Element(IMAGE).Value,

					         ensureArraySize((from a in q.Element(ANSWERS).Elements(CHOICES)
						                       select new Choice(
							                           a.Attribute(CORRECT).Value == "true",
							                           a.Value
						                           )
						).ToArray(), Choice.setEmpty(), 4)
				         )).ToList();
			return new QmeXmlData(questions);
		}

		A[] ensureArraySize<A>(A[] arr, A defaultValue, int minSize)
		{
			if (arr.Length >= minSize)
				return arr;
			var defaultArr = Enumerable.Repeat(defaultValue, minSize - arr.Length).ToArray();
			return arr.Concat(defaultArr).ToArray();
		}

		public string write(string path, QmeXmlData data)
		{
			var writer = XmlWriter.Create(path, settings);
			writer.WriteStartDocument();
			writer.WriteStartElement(QUESTION_DB);

			foreach (var question in data.questions)
			{
				writer.WriteStartElement(QUESTIONS);
				writer.WriteElementString(QUESTION, question.question);
				writer.WriteElementString(IMAGE, question.imageUrl);
				writer.WriteStartElement(ANSWERS);
				foreach (var answer in question.answers.Where(_ => _.answer.Trim() != string.Empty))
				{
					writer.WriteStartElement(CHOICES);
					writer.WriteAttributeString(CORRECT, answer.correct ? "true" : "false");
					writer.WriteString(answer.answer);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();

			string content = File.ReadAllText(path);
			content = UTF8ByteArrayToString(StringToUTF8ByteArray(content));
			return(content);
		}

		public static string UTF8ByteArrayToString(byte[] characters)
		{
			return System.Text.Encoding.UTF8.GetString(characters, 0, characters.Length);
		}

		public static byte[] StringToUTF8ByteArray(string text)
		{
			return System.Text.Encoding.UTF8.GetBytes(text);
		}
	}
}