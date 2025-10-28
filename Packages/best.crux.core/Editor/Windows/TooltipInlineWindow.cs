using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Windows
{
    public class TooltipInlineWindow : PopupWindowContent
    {
        public class TooltipInlineData
        {
            public string text;
        }
        
        private TooltipInlineData data;
        private VisualElement root;
        
        public TooltipInlineWindow(TooltipInlineData data)
        {
            this.data = data;
        }
        
        public override Vector2 GetWindowSize()
        {
            if (root == null)
                return new Vector2(2000, 2000);

            var style = root.resolvedStyle;
            
            if (float.IsNaN(style.width) || float.IsNaN(style.height))
                return new Vector2(2000, 2000);
            
            return new Vector2(style.width, style.height);
        }

        public override void OnGUI(Rect rect)
        {
            
        }

        public override void OnOpen()
        {
            var rootUxml =
                AssetReference.ParseAndLoad<VisualTreeAsset>("99dbaa562252745db89c9187192a7add,9197481963319205126");

            var inlineUxml =
                AssetReference.ParseAndLoad<VisualTreeAsset>("3b20fd1421bc24131b2fbfa123f9659b,9197481963319205126");
            
            root = rootUxml.Instantiate();
            editorWindow.rootVisualElement.Add(root);

            inlineUxml.CloneTree(root.Q(name: "TooltipContent"));

            root.Q<Label>("Inline-Text").text = data.text;
            
            editorWindow.rootVisualElement.style.maxWidth = 300;
        }
    }
}