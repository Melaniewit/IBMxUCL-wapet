using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Demo : MonoBehaviour
{
    private GameObject[] animals;
    private int animalIndex;
    private List<string> animationList = new List<string>
                                            {   "Idle_A",
                                                "Walk",
                                                "Bounce",
                                                "Clicked",
                                                "Eat",
                                                "Fly",
                                                "Jump",
                                                "Run",
                                                "Sit",
                                                "Swim",
                                                "Spin"
                                            };
    private List<string> shapekeyList = new List<string>
                                            {   "Eyes_Blink",
                                                "Eyes_Excited",
                                                "Eyes_Happy",
                                                "Eyes_Sad",
                                                "Eyes_Sleep",
                                                "Eyes_Annoyed",
                                                "Eyes_Cry"
                                            };

    // Dictionary to map original animation names to display names
    private Dictionary<string, string> animationNameMap = new Dictionary<string, string>
    {
        { "Idle_A", "Idle" },
        { "Walk", "Walk" },
        { "Bounce", "Bounce" },
        { "Clicked", "Clicked" },
        { "Eat", "Eat" },
        { "Fly", "Fly" },
        { "Jump", "Jump" },
        { "Run", "Run" },
        { "Sit", "Sit" },
        { "Swim", "Swim" },
        { "Spin", "Spin" }
    };

    // Dictionary to map original shapekey names to display names
    private Dictionary<string, string> shapekeyNameMap = new Dictionary<string, string>
    {
        { "Eyes_Blink", "Blink" },
        { "Eyes_Excited", "Excited" },
        { "Eyes_Happy", "Happy" },
        { "Eyes_Sad", "Sad" },
        { "Eyes_Sleep", "Sleep" },
        { "Eyes_Annoyed", "Annoyed" },
        { "Eyes_Cry", "Cry" }
    };

    // Dictionary to store animation speed multipliers  
    private Dictionary<string, float> animationSpeedMap = new Dictionary<string, float>
    {
        { "Idle_A", 0.8f },
        { "Walk", 1.0f },
        { "Bounce", 0.7f },
        { "Clicked", 0.8f },
        { "Eat", 0.8f },
        { "Fly", 1.0f },
        { "Jump", 0.8f },
        { "Run", 1.0f },
        { "Sit", 0.9f },
        { "Swim", 1.0f },
        { "Spin", 0.3f }
    };

    // Dictionary to store allowed animations for each animal //changed!!
    private Dictionary<string, List<string>> animalAllowedAnimations = new Dictionary<string, List<string>> //changed!!
    { //changed!!
        { "Cat", new List<string> { "Idle_A", "Walk", "Eat", "Jump", "Run", "Sit", "Spin" } }, //changed!!
        { "Dog", new List<string> { "Idle_A", "Walk", "Eat", "Jump", "Run", "Sit", "Spin" } }, //changed!!
        { "Goldfish", new List<string> { "Idle_A", "Swim" } }, //changed!!
        { "Mouse", new List<string> { "Idle_A", "Eat", "Jump", "Run", "Spin" } }, //changed!!
        { "Rabbit", new List<string> { "Idle_A", "Eat", "Jump", "Run", "Sit", "Spin" } }, //changed!!
        { "Tortoise", new List<string> { "Idle_A", "Walk", "Eat", "Swim" } } //changed!!
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

        dropdownAnimal.onValueChanged.AddListener(delegate { ChangeAnimal(); }); //changed!!

        UpdateAnimationDropdown(); //changed!!

        // Use the mapped display names for shape keys
        List<string> displayShapekeyList = new List<string>();
        foreach (string shapekey in shapekeyList)
        {
            string displayName = shapekeyNameMap.ContainsKey(shapekey) ? shapekeyNameMap[shapekey] : shapekey;
            displayShapekeyList.Add(displayName);
        }
        dropdownShapekey.ClearOptions();
        dropdownShapekey.AddOptions(displayShapekeyList);

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

        UpdateAnimationDropdown(); //changed!!

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
            string animName = animationList[dropdownAnimation.value]; // Use original name

            // Set the animation speed
            if (animationSpeedMap.ContainsKey(animName))
            {
                animator.speed = animationSpeedMap[animName];
            }
            else
            {
                animator.speed = 1.0f; // Default speed  
            }

            animator.Play(animName); // Play using original name
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
            string shapeKeyName = shapekeyList[dropdownShapekey.value]; // Use original name

            animator.Play(shapeKeyName); // Play using original name
        }
    }

    // Update the animations in the dropdown based on the selected animal //changed!!
    private void UpdateAnimationDropdown() //changed!!
    { //changed!!
        string currentAnimal = animals[dropdownAnimal.value].name; //changed!!
        List<string> allowedAnimations = animalAllowedAnimations[currentAnimal];
