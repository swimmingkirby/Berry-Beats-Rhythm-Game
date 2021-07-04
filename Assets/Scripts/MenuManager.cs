using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] Image menu_text;
    [SerializeField] Sprite[] texts;

    int menu_indicator = 0;

    void Start()
    {
        menu_indicator = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("up");
            ChangeMenu(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("down");
            ChangeMenu(-1);
        }
    }

    public void ChangeMenu(int input)
    {
        if (menu_indicator + input == -1 || menu_indicator + input == 3)
        {
            if (menu_indicator + input == -1)
                menu_indicator = 2;
            if (menu_indicator + input == 3)
                menu_indicator = 0;
        }
        else
            menu_indicator += input;

        background.sprite = backgrounds[menu_indicator];
        menu_text.sprite = texts[menu_indicator];
            
    }

}
