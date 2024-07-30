﻿/* Scripted by Omabu - omabuarts@gmail.com */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Demo : MonoBehaviour
{

	private GameObject[] animals;
	private int animalIndex;
	private List<string> animationList = new List<string>
{
	"Idle_A",
	"Walk",
	"Bounce",
	"Clicked",
	"Eat",
	"Fly",
	"Jump",
	"Run",
	"Sit",
	"Swim"

};
	private List<string> shapekeyList = new List<string>
											{
												"Eyes_Blink",
												"Eyes_Excited",
												"Eyes_Happy",
												"Eyes_Sad",
												"Eyes_Sleep",
												"Eyes_Annoyed",
												"Eyes_Cry",
											};

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
			// animalList.Add(n.Substring(0, n.IndexOf("_")));

			if (i == 0) animals[i].SetActive(true);
			else animals[i].SetActive(false);
		}

		dropdownAnimal.AddOptions(animalList);
		dropdownAnimation.AddOptions(animationList);
		dropdownShapekey.AddOptions(shapekeyList);

		// Set to eyes_blink
		dropdownShapekey.value = 1;
		ChangeShapekey();

		// Bounds b = animals[0].transform.GetChild(0).GetChild(0).GetComponent<Renderer>().bounds;
	}

	void Update()
	{
		if (Input.GetKeyDown("up")) { PrevAnimal(); }
		else if (Input.GetKeyDown("down")) { NextAnimal(); }
		else if (Input.GetKeyDown("right")
			&& (Input.GetKey(KeyCode.LeftControl)
			|| Input.GetKey(KeyCode.RightControl))) { NextShapekey(); }
		else if (Input.GetKeyDown("left")
			&& (Input.GetKey(KeyCode.LeftControl)
			|| Input.GetKey(KeyCode.RightControl))) { PrevShapekey(); }
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
			int index = dropdownAnimation.value;

			// If Spin/Splash animation
			if (index == 15)
			{
				if (animator.HasState(0, Animator.StringToHash("Spin")))
				{
					animator.Play("Spin");
					// dropdownAnimation.options[index] = new Dropdown.OptionData("Spin");
				}
				else if (animator.HasState(0, Animator.StringToHash("Splash")))
				{
					animator.Play("Splash");
					// dropdownAnimation.options[index] = new Dropdown.OptionData("Splash");
				}
			}
			else
			{
				animator.Play(dropdownAnimation.options[index].text);
			}
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
			animator.Play(dropdownShapekey.options[dropdownShapekey.value].text);
		}
	}

	public void GoToWebsite(string URL)
	{
		Application.OpenURL(URL);
	}
}