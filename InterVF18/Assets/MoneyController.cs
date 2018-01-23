using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyController : MonoBehaviour {

	private static MoneyController _instance;
	public static MoneyController Instance{
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<MoneyController> ();
			}
			return _instance;
		}
	}

	[SerializeField]
	Text txtMoneyLeft;

	void Start(){
		UpdateMoneyOnHUD ();
	}

	public void UpdateMoneyOnHUD(){
		txtMoneyLeft.text = Money.money.ToString ();
	}

}
