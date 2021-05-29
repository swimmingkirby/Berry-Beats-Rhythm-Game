using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace BerryBeats.UI.Widgets
{
    #region CustomLabel

    public struct CustomLabelData
    {
        public Vector2 position;
        public string text;
        public Color color;

        public static CustomLabelData Default()
        {
            return new CustomLabelData()
            {
                position = Vector2.zero,
                text = "Hello, World!",
                color = Color.white
            };
        }
    }

    public class CustomLabel : MonoBehaviour
    {
        private TMP_Text text;
        private RectTransform trans;
        private CustomLabelData ownData;

        public static CustomLabel New(CustomLabelData data, Transform parent)
        {
            // Set Defaults
            if (data.position == null)
                data.position = Vector2.zero;
            if (data.text == null)
                data.text = "UNSET";
            if (data.color == null)
                data.color = Color.white;

            // Create Object
            GameObject instance = Instantiate(Resources.Load<GameObject>("EditorUI/Label"), parent);

            // Exit static context and initialize
            return instance.AddComponent<CustomLabel>()
                .Init()
                .SetData(data);
        }

        /// <summary>
        /// DO NOT USE, this is only for the constuctor
        /// </summary>
        /// <returns></returns>
        public CustomLabel Init()
        {
            text = GetComponent<TMP_Text>();
            trans = GetComponent<RectTransform>();

            return this;
        }

        public CustomLabel SetData(CustomLabelData d)
        {
            ownData = d;
            SetText(ownData.text.ToLower());
            SetColor(ownData.color);
            SetPosition(ownData.position);

            return this;
        }

        public CustomLabel SetText(string t) {
            ownData.text = t;
            text.text = t;

            return this;
        }

        public CustomLabel SetColor(Color c)
        {
            ownData.color = c;
            text.color = c;

            return this;
        }

        public CustomLabel SetPosition(Vector2 pos)
        {
            ownData.position = pos;
            trans.anchoredPosition = pos;

            return this;
        }

        public CustomLabelData GetData()
        {
            return ownData;
        }
    }

    #endregion

    #region CustomButton

    public struct CustomButtonData
    {
        public Sprite normal;
        public Sprite hovered;
        public Sprite clicked;
        public Sprite disabled;

        public Vector4 transform;
        public string text;
    }

    public class CustomButton : MonoBehaviour
    {
        public static GameObject New(CustomButtonData data)
        {
            // TODO: Add Component
            // TODO: Create Initializer
            // TODO: Create non-static initializer in object-space

            return null;
        }
    }

    #endregion

    #region CustomPanel

    /// <summary>
    /// <code>
    /// Vector4 transform, 
    /// CustomPanel.Type panelType, 
    /// bool closable, 
    /// string title
    /// </code>
    /// </summary>
    public struct CustomPanelData
    {
        public Vector4 transform;
        public CustomPanel.Type panelType;
        public bool closable;
        public string title;
    }

    public class CustomPanel : MonoBehaviour
    {
        public enum Type
        {
            WithoutTitle,
            WithTitle
        }

        
        public static CustomPanel New(CustomPanelData data, Transform parent)
        {
            // TODO: Create non-static initializer in object-space

            // Create object and hide it
            GameObject instance = Instantiate(Resources.Load<GameObject>("EditorUI/Panel"), parent);
            instance.transform.localScale = Vector3.zero;
            instance.transform.LeanSetPosY(-Screen.height);

            // Set position and size
            instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(data.transform.x, data.transform.y);
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(data.transform.z, data.transform.w);

            // Add properties
            switch (data.panelType)
            {
                case CustomPanel.Type.WithTitle:
                    instance.transform.Find("Title").GetComponent<TMP_Text>().text = data.title.ToLower();
                    break;
                case CustomPanel.Type.WithoutTitle:
                    Destroy(instance.transform.Find("Title"));
                    break;
                default:
                    break;
            }

            // Show window and tween animations
            instance.transform.LeanScale(Vector3.one, 0.6f)
                .setEaseOutQuint();
            instance.transform.LeanMoveLocalY(0, 0.6f)
                .setEaseOutQuint();

            return instance.AddComponent<CustomPanel>();
        }
    }

    #endregion

    #region CustomProgress

    public struct CustomProgressData
    {
        public Vector2 position;
        public float max;
        public float progress;
    }

    // Progressbar
    public class CustomProgress : MonoBehaviour
    {
        private CustomProgressData ownData;
        private RectTransform self;
        private RectTransform bar;
        private Image fill;
        private TMP_Text label;

        // Replacement for Start() to be used in static context
        public CustomProgress Init()
        {
            bar = transform.Find("Bar").GetComponent<RectTransform>();
            fill = bar.Find("Fill").GetComponent<Image>();
            label = transform.Find("Label").GetComponent<TMP_Text>();
            self = GetComponent<RectTransform>();

            return this;
        }

        // For static New()
        public CustomProgress SetData(CustomProgressData d)
        {
            ownData = d;
            SetProgress(d.progress);
            SetPosition(d.position);

            return this;
        }

        public CustomProgress SetPosition(Vector2 p)
        {
            self.anchoredPosition = new Vector2(p.x, p.y);

            return this;
        }

        public CustomProgress SetProgress(float amount)
        {
            ownData.progress = amount;
            // Divide by max to get a normalized value
            fill.fillAmount = amount / ownData.max;
            // Create percentage value and assign to label
            label.text = Mathf.CeilToInt(fill.fillAmount * 100) + "%";

            return this;
        }

        public float GetProgress()
        {
            return ownData.progress;
        }

        public static CustomProgress New(CustomProgressData data, Transform parent)
        {
            // Set Defaults
            if (data.position == null)
                data.position = new Vector2(0, 0);

            // Create object
            GameObject instance = Instantiate(Resources.Load<GameObject>("EditorUI/Progress"), parent);
            return instance.AddComponent<CustomProgress>()
                .Init()
                .SetData(data);
        }
    }

    #endregion
}