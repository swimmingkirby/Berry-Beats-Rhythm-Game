using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{

    [SerializeField] GameObject background;
    [SerializeField] Image level_text;
    [SerializeField] Sprite[] texts;
    [SerializeField] RectTransform size_example;
    [SerializeField] float transition_speed = 3000f;

    int menu_indicator;
    float image_width;
    float standard_y_pos;

    bool has_to_move = false;
    Vector3 destination = new Vector3();

    void Start()
    {
        standard_y_pos = background.transform.position.y;
        image_width = size_example.rect.width;
        menu_indicator = 0;
    }

    
    void Update()
    {
        if (!has_to_move)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeMenu(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
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

        //print(background.transform.localPosition.x);
        if (background.transform.localPosition.x == image_width * -3 || background.transform.localPosition.x == image_width)
        {
            has_to_move = false;
            if (background.transform.localPosition.x == image_width * -3)
                background.transform.localPosition = new Vector3(0f, background.transform.localPosition.y);

            if (background.transform.localPosition.x == image_width)
                background.transform.localPosition = new Vector3(image_width * -2, background.transform.localPosition.y);
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
                destination = new Vector3(background.transform.position.x - image_width,  standard_y_pos);
            }
            if (input == -1)
            {
                destination = new Vector3(background.transform.position.x + image_width, standard_y_pos);
            }

            has_to_move = true;
            level_text.sprite = texts[menu_indicator];
        }
        
    }

}
