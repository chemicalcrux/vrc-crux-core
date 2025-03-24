using System.Collections.Generic;
using ChemicalCrux.CruxCore.Editor.Documentation;
using ChemicalCrux.CruxCore.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace ChemicalCrux.CruxCore.Editor.Windows
{
    public class DocumentationWindow : EditorWindow
    {
        private DocManual currentManual;
        private DocPage currentPage;

        private Label titleElement;
        private Button backButton;
        private ScrollView pageList;
        private ScrollView pageElement;
        
        private Button currentButton;
        private Dictionary<DocPage, Button> buttonMap = new();

        [MenuItem(CoreConsts.MenuRootPath + "Documentation", priority = CoreConsts.MenuRootPriority)]
        public static void OpenRoot()
        {
            var window = GetWindow();
            window.ShowRoot();
            window.Show();
        }
        
        public static void OpenDocumentation(DocManual manual)
        {
            var window = CreateWindow<DocumentationWindow>();
            window.ShowDocumentation(manual);
            window.Show();
        }

        private static DocumentationWindow GetWindow()
        {
            var window = CreateWindow<DocumentationWindow>();
            
            // forces the window to show up at least this large
            window.minSize = new Vector2(500, 800);
            window.minSize = new Vector2(0, 0);

            var texture =
                AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Packages/com.chemicalcrux.crux-ui/Icons/Documentation Window.png");

            window.titleContent = new GUIContent("Documentation", texture);

            return window;
        }

        private void CreateGUI()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.chemicalcrux.crux-core/UI/Windows/Documentation.uxml");

            uxml.CloneTree(rootVisualElement);

            titleElement = rootVisualElement.Q<Label>("DocumentationTitle");
            backButton = rootVisualElement.Q<Button>("BackButton");
            pageList = rootVisualElement.Q<ScrollView>("Topics");
            pageElement = rootVisualElement.Q<ScrollView>("Page");

            backButton.clicked += ShowRoot;
        }
        
        private void Clear()
        {
            pageList.Clear();
            buttonMap.Clear();
            pageElement.Clear();

            currentManual = null;
            currentPage = null;

            currentButton = null;
        }

        private void ShowRoot()
        {
            Clear();

            titleElement.text = "Documentation";
            
            pageList.style.display = DisplayStyle.None;
            backButton.style.display = DisplayStyle.None;

            var docButton =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.chemicalcrux.crux-core/UI/Templates/DocSet Entry.uxml");
            
            foreach (var guid in AssetDatabase.FindAssets("t:" + nameof(DocManual)))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var docSet = AssetDatabase.LoadAssetAtPath<DocManual>(path);
                var button = docButton.Instantiate();
                pageElement.Add(button);

                button.Q<Label>("Title").text = docSet.title;
                button.Q<Label>("Description").text = docSet.description;
                button.Q("Icon").style.backgroundImage = Background.FromTexture2D(docSet.icon);

                button.Q<Button>("Button").clicked += () =>
                {
                    ShowDocumentation(docSet);
                };
            }
        }

        private void ShowDocumentation(DocManual manual)
        {
            Clear();

            titleElement.text = manual.title;

            pageList.style.display = DisplayStyle.Flex;
            backButton.style.display = DisplayStyle.Flex;

            currentManual = manual;
            
            foreach (var category in manual.categories)
            {
                var label = new Label(category.label);
                label.AddToClassList("doc-category");
                pageList.Add(label);
                
                foreach (var page in category.pages)
                {
                    var button = new Button
                    {
                        text = page.title
                    };

                    button.clicked += () => ShowPage(page);

                    pageList.Add(button);
                    buttonMap[page] = button;
                }
            }

            currentButton = buttonMap[manual.categories[0].pages[0]];
            ShowPage(manual.categories[0].pages[0]);
        }

        private void ShowPage(DocPage page)
        {
            currentPage = page;

            pageElement.contentContainer.Clear();

            page.document.CloneTree(pageElement.contentContainer);

            currentButton.RemoveFromClassList("selected-topic");
            currentButton = buttonMap[page];
            currentButton.AddToClassList("selected-topic");

            foreach (var label in pageElement.Query(classes: new[] { "doc-text" }).ToList())
            {
                label.RegisterCallback<PointerDownLinkTagEvent>(HandleLink);
            }
        }

        private void HandleLink(PointerDownLinkTagEvent evt)
        {
            var parts = evt.linkID.Split(":");

            if (parts.Length < 2)
                return;

            string kind = parts[0];

            switch(kind)
            {
                case "select":
                    HandleSelectLink(parts);
                    break;
            }
        }

        private void HandleSelectLink(string[] parts)
        {
            Debug.Log("Trying to ping: " + parts[1]);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(parts[1]);
            Debug.Log("Got: " + asset);
            EditorGUIUtility.PingObject(asset);
        }
    }
}