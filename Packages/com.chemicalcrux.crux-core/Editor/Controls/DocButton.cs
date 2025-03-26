using ChemicalCrux.CruxCore.Editor.Documentation;
using ChemicalCrux.CruxCore.Editor.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Controls
{
    public class DocButton : Button
    {
        public new class UxmlFactory : UxmlFactory<DocButton, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var button = base.Create(bag, cc) as DocButton;

                button!.clicked += () =>
                {
                    DocManual manual = null;
                    DocPage page = null;
                    
                    if (!string.IsNullOrEmpty(button.DocManualRef))
                    {
                        if (!AssetReference.TryParse(button.DocManualRef, out var assetRef))
                        {
                            Debug.LogWarning("A documentation page button has an invalid documentation manual reference. See the above warning.");
                            return;
                        }
                    
                        if (!assetRef.TryLoad(out manual))
                        {
                            Debug.LogWarning("A documentation manual button failed to load its page. See the above warning.");
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(button.DocPageRef))
                    {
                        if (!AssetReference.TryParse(button.DocPageRef, out var assetRef))
                        {
                            Debug.LogWarning("A documentation page button has an invalid documentation page reference. See the above warning.");
                            return;
                        }
                    
                        if (!assetRef.TryLoad(out page))
                        {
                            Debug.LogWarning("A documentation page button failed to load its page. See the above warning.");
                            return;
                        }
                    }

                    if (manual != null)
                    {
                        DocumentationWindow.OpenDocumentation(manual);
                    }

                    if (page != null)
                    {
                        DocumentationWindow.OpenPage(page);   
                    }
                };

                return button;
            }
        }

        public new class UxmlTraits : Button.UxmlTraits
        {
            // Unity 2022.3 does not support asset references here, so the backup plan is to just store the
            // GUID and FileID of the asset in a string.
            
            private readonly UxmlStringAttributeDescription docManaulRef = new()
                { name = "doc-manual-ref", defaultValue = "" };
            
            private readonly UxmlStringAttributeDescription docPageRef = new()
                { name = "doc-page-ref", defaultValue = "" };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as DocButton;

                ate!.DocManualRef = docManaulRef.GetValueFromBag(bag, cc);
                ate!.DocPageRef = docPageRef.GetValueFromBag(bag, cc);
            }
        }

        public string DocManualRef { get; set; }
        public string DocPageRef { get; set; }
    }
}