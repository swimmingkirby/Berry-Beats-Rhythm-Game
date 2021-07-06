using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Image menu_text;
    [SerializeField] Sprite[] texts;
    [SerializeField] RectTransform size_example;
    [SerializeField] float transition_speed = 3000f;

    int menu_indicator = 0;
    float image_height;
    float standard_x_pos;

    bool has_to_move = false;
    Vector3 destination = new Vector3();

    void Start()
    {
        standard_x_pos = background.transform.position.x;
        image_height = size_example.rect.height;
        menu_indicator = 0;
    }

    void Update()
    {
        if (!has_to_move)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeMenu(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeMenu(-1);
            }
        }
        
        if (has_to_move)
        {
            if (background.transform.position == destination)
                has_to_move = false;

            background.transform.position = Vector3.MoveTowards(background.transform.position, destination, Time.deltaTime * transition_speed);
        }

        print(background.transform.position.y);
        if(background.transform.position.y == image_height + (image_height / 2) || background.transform.position.y == (-image_height / 2) - 2 * image_height)
        {
            has_to_move = false;
            if (background.transform.position.y == image_height + (image_height / 2))
                background.transform.position = new Vector3(background.transform.position.x, -image_height - (image_height / 2));

            if(background.transform.position.y == (-image_height / 2) - 2 * image_height)
                background.transform.position = new Vector3(background.transform.position.x, image_height / 2);
        }

    }

    public void ChangeMenu(int input)
    {
        if (!has_to_move)
        {

            menu_indicator += input;

            if (menu_indicator == -1 || menu_indicator == 3)
            {
                if (menu_indicator == -1)
                    menu_indicator = 2;
                if (menu_indicator == 3)
                    menu_indicator = 0;
            }

            if (input == 1)
            {
                destination = new Vector3(standard_x_pos, background.transform.position.y - image_height);
            }
            if (input == -1)
            {
                destination = new Vector3(standard_x_pos, background.transform.position.y + image_height);
            }

            has_to_move = true;
            menu_text.sprite = texts[menu_indicator];

        }
    }

}
