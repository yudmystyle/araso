using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

	public string[] RandomStrings = new string[0];

	private void OnEnable()
	{
		Text TxtField = gameObject.GetComponent<Text> ();

		if (RandomStrings.Length > 0) {
			//TxtField.text = "Loading...\n" + RandomStrings [Random.Range(0, RandomStrings.Length-1)];
			TxtField.text = RandomStrings [Random.Range(0, RandomStrings.Length-1)];
		}
	}

}