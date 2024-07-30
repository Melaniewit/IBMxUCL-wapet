using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Demo : MonoBehaviour
{

	private GameObject[] animals;
	private int animalIndex;
	private List<string> animationList = new List<string> //changed!!
                                            {   "Idle_A", //changed!!
                                                "Walk",   //changed!!
                                                "Bounce", //changed!!
                                                "Clicked",//changed!!
                                                "Eat",    //changed!!
                                                "Fly",    //changed!!
                                                "Jump",   //changed!!
                                                "Run",    //changed!!
                                                "Sit",    //changed!!
                                                "Swim",   //changed!!
                                                "Spin"    //changed!!
                                            }; //changed!!
	private List<string> shapekeyList = new List<string> //changed!!
                                            {   "Eyes_Blink",   //changed!!
                                                "Eyes_Excited", //changed!!
                                                "Eyes_Happy",   //changed!!
                                                "Eyes_Sad",     //changed!!
                                                "Eyes_Sleep",   //changed!!
                                                "Eyes_Annoyed", //changed!!
                                                "Eyes_Cry"      //changed!!
                                            }; //changed!!

	// Dictionary to map original animation names to display names //changed!!
	private Dictionary<string, string> animationNameMap = new Dictionary<string, string> //changed!!
    { //changed!!
        { "Idle_A", "Idle" },       //changed!!
        { "Walk", "Walk" },         //changed!!
        { "Bounce", "Bounce" },     //changed!!
        { "Clicked", "Clicked" },   //changed!!
        { "Eat", "Eat" },           //changed!!
        { "Fly", "Fly" },           //changed!!
        { "Jump", "Jump" },         //changed!!
        { "Run", "Run" },           //changed!!
        { "Sit", "Sit" },           //changed!!
        { "Swim", "Swim" },         //changed!!
        { "Spin", "Spin" }          //changed!!
    }; //changed!!

	// Dictionary to map original shapekey names to display names //changed!!
	private Dictionary<string, string> shapekeyNameMap = new Dictionary<string, string> //changed!!
    { //changed!!
        { "Eyes_Blink", "Blink" },      //changed!!
        { "Eyes_Excited", "Excited" },  //changed!!
        { "Eyes_Happy", "Happy" },      //changed!!
        { "Eyes_Sad", "Sad" },          //changed!!
        { "Eyes_Sleep", "Sleep" },      //changed!!
        { "Eyes_Annoyed", "Annoyed" },  //changed!!
        { "Eyes_Cry", "Cry" }           //changed!!
    }; //changed!!

	[Space(10)]
	Transform animal_parent;
	Dropdown dropdownAnimal;
	Dropdown dropdownAnimation;
	Dropdown dropdownShapekey;

	void Start()
	{
		animal_parent = GameObject.Find("Animals").transform;
		Transform canvas = GameObject.Find("Canvas").transform;
		dropdownAnimal = canvas.Find("Animal").Find("Dropdown").GetComponent<Dropdown>();
		dropdownAnimation = canvas.Find("Animation").Find("Dropdown").GetComponent<Dropdown>();
		dropdownShapekey = canvas.Find("Shapekey").Find("Dropdown").GetComponent<Dropdown>();

		int count = animal_parent.childCount;
		animals = new GameObject[count];
		List<string> animalList = new List<string>();

		for (int i = 0; i < count; i++)
		{
			animals[i] = animal_parent.GetChild(i).gameObject;
			string n = animal_parent.GetChild(i).name;
			animalList.Add(n);
			if (i == 0) animals[i].SetActive(true);
			else animals[i].SetActive(false);
		}

		dropdownAnimal.AddOptions(animalList);

		// Use the mapped display names instead of the original names //changed!!
		List<string> displayAnimationList = new List<string>(); //changed!!
		foreach (string anim in animationList)
		{ //changed!!
			string displayName = animationNameMap.ContainsKey(anim) ? animationNameMap[anim] : anim; //changed!!
			displayAnimationList.Add(displayName); //changed!!
		} //changed!!
		dropdownAnimation.ClearOptions(); //changed!!
		dropdownAnimation.AddOptions(displayAnimationList); //changed!!

		// Use the mapped display names for shape keys //changed!!
		List<string> displayShapekeyList = new List<string>(); //changed!!
		foreach (string shapekey in shapekeyList)
		{ //changed!!
			string displayName = shapekeyNameMap.ContainsKey(shapekey) ? shapekeyNameMap[shapekey] : shapekey; //changed!!
			displayShapekeyList.Add(displayName); //changed!!
		} //changed!!
		dropdownShapekey.ClearOptions(); //changed!!
		dropdownShapekey.AddOptions(displayShapekeyList); //changed!!

		dropdownShapekey.value = 1;
		ChangeShapekey();
	}

	void Update()
	{
		if (Input.GetKeyDown("up")) { PrevAnimal(); }
		else if (Input.GetKeyDown("down")) { NextAnimal(); }
		else if (Input.GetKeyDown("right") && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) { NextShapekey(); }
		else if (Input.GetKeyDown("left") && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) { PrevShapekey(); }
		else if (Input.GetKeyDown("right")) { NextAnimation(); }
		else if (Input.GetKeyDown("left")) { PrevAnimation(); }
	}

	public void NextAnimal()
	{
		if (dropdownAnimal.value >= dropdownAnimal.options.Count - 1)
			dropdownAnimal.value = 0;
		else
			dropdownAnimal.value++;

		ChangeAnimal();
	}

	public void PrevAnimal()
	{
		if (dropdownAnimal.value <= 0)
			dropdownAnimal.value = dropdownAnimal.options.Count - 1;
		else
			dropdownAnimal.value--;

		ChangeAnimal();
	}

	public void ChangeAnimal()
	{
		animals[animalIndex].SetActive(false);
		animals[dropdownAnimal.value].SetActive(true);
		animalIndex = dropdownAnimal.value;

		ChangeAnimation();
		ChangeShapekey();
	}

	public void NextAnimation()
	{
		if (dropdownAnimation.value >= dropdownAnimation.options.Count - 1)
			dropdownAnimation.value = 0;
		else
			dropdownAnimation.value++;

		ChangeAnimation();
	}

	public void PrevAnimation()
	{
		if (dropdownAnimation.value <= 0)
			dropdownAnimation.value = dropdownAnimation.options.Count - 1;
		else
			dropdownAnimation.value--;

		ChangeAnimation();
	}

	public void ChangeAnimation()
	{
		Animator animator = animals[dropdownAnimal.value].GetComponent<Animator>();
		if (animator != null)
		{
			string animName = animationList[dropdownAnimation.value]; //changed!!
			string mappedName = animationNameMap.ContainsKey(animName) ? animationNameMap[animName] : animName; //changed!!

			animator.Play(mappedName); //changed!!
		}
	}

	public void NextShapekey()
	{
		if (dropdownShapekey.value >= dropdownShapekey.options.Count - 1)
			dropdownShapekey.value = 0;
		else
			dropdownShapekey.value++;

		ChangeShapekey();
	}

	public void PrevShapekey()
	{
		if (dropdownShapekey.value <= 0)
			dropdownShapekey.value = dropdownShapekey.options.Count - 1;
		else
			dropdownShapekey.value--;

		ChangeShapekey();
	}

	public void ChangeShapekey()
	{
		Animator animator = animals[dropdownAnimal.value].GetComponent<Animator>();
		if (animator != null)
		{
			string shapeKeyName = shapekeyList[dropdownShapekey.value]; //changed!!
			string mappedName = shapekeyNameMap.ContainsKey(shapeKeyName) ? shapekeyNameMap[shapeKeyName] : shapeKeyName; //changed!!

			animator.Play(mappedName); //changed!!
		}
	}

	public void GoToWebsite(string URL)
	{
		Application.OpenURL(URL);
	}
}
