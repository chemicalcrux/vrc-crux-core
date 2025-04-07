using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crux.Core.Editor.Windows
{
    public class TooltipWindow : PopupWindowContent
    {
        private VisualTreeAsset tree;
        private VisualElement root;
        
        public TooltipWindow(VisualTreeAsset tree)
        {
            this.tree = tree;
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
            var rootUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.chemicalcrux.crux-core/UI/Templates/Tooltip.uxml");

            root = rootUxml.Instantiate();
            editorWindow.rootVisualElement.Add(root);

            tree.CloneTree(root.Q(name: "TooltipContent"));
            
    
            editorWindow.rootVisualElement.style.maxWidth = 300;
        }
    }
}