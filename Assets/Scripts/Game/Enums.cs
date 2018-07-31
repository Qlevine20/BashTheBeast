using UnityEngine;
using System.Collections;

public enum ActionType
{
	None,
	Action,
	//DiceAgain,
	//MissATurn,
	GoAhead,
	//BackToStart,
	GoBack,
	//SendPlayerToStart,
	GoToField,
	//GoToJail,
	Custom,
	Question,
	MultiQues,
}

public enum FieldType
{
	Start,
	Finish,
	Normal,
	Action,
}