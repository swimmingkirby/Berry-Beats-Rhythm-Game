using BerryBeats.UI.Widgets;
using UnityEngine;

namespace BerryBeats.LevelCreator.UI
{
    public class EditorUI : MonoBehaviour
    {
        void Start()
        {
            Invoke("test", 1f);
        }

        void test()
        {
            Debug.Log("Start!");
            CustomPanel.New(new CustomPanelData{
                transform = new Vector4(0, 0, 300, 200),
                panelType = CustomPanel.Type.WithTitle,
                closable = true,
                title = "Test Window"
            }, transform);
        }
    }
}