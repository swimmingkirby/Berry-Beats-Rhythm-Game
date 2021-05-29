# EditorUI | Docs

## Table of Contents

- [EditorUI | Docs](#editorui--docs)
  - [Table of Contents](#table-of-contents)
  - [*struct* CustomLabelData](#struct-customlabeldata)
    - [Variables:](#variables)
      - [position](#position)
      - [text](#text)
      - [color](#color)
      - [***static*** Default()](#static-default)
    - [Example](#example)
  - [*class* CustomLabel](#class-customlabel)
    - [Methods](#methods)
      - [***static*** New(CustomLabelData, Transform)](#static-newcustomlabeldata-transform)
      - [Init()](#init)
      - [SetData(CustomLabelData)](#setdatacustomlabeldata)
      - [SetText(string)](#settextstring)
      - [SetColor(Color)](#setcolorcolor)
      - [SetPosition(Vector2)](#setpositionvector2)
    - [Variables](#variables-1)
      - [GetData()](#getdata)
    - [**NOTE**](#note)
    - [Example](#example-1)
  - [*struct* CustomButtonData [IN DEVELOPMENT]](#struct-custombuttondata-in-development)
    - [Variables:](#variables-2)
      - [normal](#normal)
      - [hovered](#hovered)
      - [clicked](#clicked)
      - [disabled](#disabled)
      - [transform](#transform)
      - [text](#text-1)
    - [Example](#example-2)
  - [*class* CustomButton [IN DEVELOPMENT]](#class-custombutton-in-development)
  - [*struct* CustomPanelData](#struct-custompaneldata)
    - [Variables](#variables-3)
      - [transform](#transform-1)
      - [panelType [IN DEVELOPMENT]](#paneltype-in-development)
      - [closable [IN DEVELOPMENT]](#closable-in-development)
      - [title](#title)
  - [*class* CustomPanel](#class-custompanel)
    - [Methods](#methods-1)
      - [***static*** New(CustomPanelData, Transform)](#static-newcustompaneldata-transform)
  - [*struct* CustomProgressData](#struct-customprogressdata)
    - [Variables](#variables-4)
      - [position](#position-1)
      - [max](#max)
      - [value](#value)
  - [*class* CustomProgress](#class-customprogress)
    - [Methods](#methods-2)
      - [***static*** New(CustomProgressData, Transform)](#static-newcustomprogressdata-transform)
      - [Init()](#init-1)
      - [SetData(CustomProgressData)](#setdatacustomprogressdata)
      - [SetProgress(float)](#setprogressfloat)
      - [SetPosition(Vector2)](#setpositionvector2-1)
    - [Example](#example-3)

Every class in the BerryBeats.UI.Widgets namespace has a static New() initializer that instantiates the object, sets the parameters accordingly and returns the component. All New() functions also take a Data struct and a Parent transform which can be used as parameters and UI ordering respectively.

## *struct* CustomLabelData

Used to transmit data to the CustomLabel class

### Variables:

#### position
> **Vector2:** Used to indicate the position, starting from the center.
#### text
> **string:** The text that the label displays, the font only has uppercase characters so it will be converted automatically. The Glacier35 font (made by me so no copyright) has support for these characters:
> ABCDEFGHIJKLMNOPQRSTUVWXYZ!?()[]{}<>^%*#”’`-+=/1234567890|_.,\
#### color
> **Color:** Defines itself :)

#### ***static*** Default()
> **CustomLabelData:** Returns the default settings.

### Example

```cs
CustomLabelData labelData = new CustomLabelData()
{
    position = new Vector2(0, 0),
    text = "Hello, World!",
    color = Color.white
};
```

(*canvas* is the Transform of a canvas, used to indicate the parent of the label)

## *class* CustomLabel

Create a label (TMP_Text) with color, text and position in a single line!

### Methods

#### ***static*** New(CustomLabelData, Transform)
> Sets everything accordingly and creates the object, returns the component.
> *See the example section for more information*

#### Init()
> This is ***NOT*** to be used, this only exists as a replacement for Start() to prevent loops

#### SetData(CustomLabelData)
> If you want to set every parameter at the same time, I guess...

#### SetText(string)
> Sets the text, what a suprise!

#### SetColor(Color)
> Damn, I'm so good at naming these functions!

#### SetPosition(Vector2)
> I take pride in naming these things :)

### Variables

#### GetData()
> Returns a CustomLabelData object

### **NOTE**
> All of these functions return the *component* so you can chain them together like this:
```cs
customLabel
    .SetText("Hello, World!")
    .SetColor(Color.red)
    .SetPosition(new Vector2(50, -100));
```

### Example

Creating a label is super easy!

```cs
CustomLabel label = CustomLabel.New(new CustomLabelData()
{
    position = new Vector2(0, 0),
    text = "Hello, World!",
    color = Color.white
}, canvas);

// Or even this:

CustomLabel label = CustomLabel.New(labelData /* You must've already initialized a CustomLabelData */, canvas);

// OH WOW A ONE-LINER!

CustomLabel label = CustomLabel.New(CustomLabelData.Default(), canvas).SetText("Hey there!");
```

## *struct* CustomButtonData [IN DEVELOPMENT]

Oh wow whatever could this struct do? Perhaps there might a hint in the name...

### Variables:

#### normal
> **Sprite:** Button sprite in it's idle state

#### hovered
> **Sprite:** Button sprite while the mouse is over it

#### clicked
> **Sprite:** Button sprite when the mouse has pressed on it

#### disabled
> **Sprite:** Button sprite when disabled

#### transform
> **Vector4:** This is a very special variable, it actually holds 2 values! The Vector4's x and y values are the position, the z and w values are the width and height. Multitasking 101.

#### text
> **string:** Such name. Much useful

### Example

```cs
CustomButtonData buttonData = new CustomButtonData()
{
    // No sprite settings to leave them as the default

    transform = new Vector4(0, 0, 300, 100),
    text = "Press me Daddy!"
};
```

## *class* CustomButton [IN DEVELOPMENT]

Nothing yet ¯\\\_\(ツ\)\_/¯

## *struct* CustomPanelData

Use this to send data to CustomPanel.New()

### Variables

#### transform
> **Vector4:** This is a very special variable, it actually holds 2 values! The Vector4's x and y values are the position, the z and w values are the width and height. Multitasking 101.

#### panelType [IN DEVELOPMENT]
> **CustomPanel.Type:** Choose between CustomPanel.Type.WithoutTitle or CustomPanel.Type.WithTitle (will maybe add more types soon!)

#### closable [IN DEVELOPMENT]
> If the window should have a close button

#### title
> If the chosen type is CustomPanel.Type.WithTitle, this will appear at the top

## *class* CustomPanel

### Methods

#### ***static*** New(CustomPanelData, Transform)
> Creates the panel with the given data, becomes child of the given Transform.

## *struct* CustomProgressData

### Variables

#### position
> **Vector2:** Damn, what a convenient name

#### max
> **float:** The maximum value, percentage will be calculated with this in mind

#### value
> **float:** Preset a value, from 0 to `max`.

## *class* CustomProgress

### Methods

#### ***static*** New(CustomProgressData, Transform)
> A shortcut for:
> ```cs
> GameObject instance = Instantiate(Resources.Load<GameObject>("EditorUI/Progress"), parent);
> return instance.AddComponent<CustomProgress>()
>     .Init()
>     .SetData(data);
> ```

#### Init()
> This is ***NOT*** to be used. This was only made for the static New() function, use at your own risk.

#### SetData(CustomProgressData)
> Shortcut for:
> ```cs
> SetProgress(input.progress);
> SetPosition(input.position);
> ```

#### SetProgress(float)
> What a nice name you got there!

#### SetPosition(Vector2)
> What this function does may remain a mystery until a certified detective uncovers the clue in its name

### Example
```cs
CustomProgress progress = CustomProgress.New(new CustomProgressData()
{
    transform = new Vector4(0, 0, 500, 100),
    max = levelData.width*levelData.height + 500,
    progress = 0
}, canvas);
```