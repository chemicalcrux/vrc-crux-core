using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChemicalCrux.CruxCore.Editor.Controls
{
    public class RevealArea : BaseField<bool>
    {
        public RevealArea() : base(null, null)
        {
            
        }

        public override void SetValueWithoutNotify(bool newValue)
        {
            base.SetValueWithoutNotify(newValue);

            if (newValue)
            {
                AddToClassList("revealed");
                RemoveFromClassList("unrevealed");
            }
            else
            {
                AddToClassList("unrevealed");
                RemoveFromClassList("revealed");
            }
                
        }

        public new class UxmlFactory : UxmlFactory<RevealArea, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var field = base.Create(bag, cc) as RevealArea;

                field!.styleSheets.Add(
                    AssetDatabase.LoadAssetAtPath<StyleSheet>(
                        "Packages/com.chemicalcrux.crux-core/UI/Stylesheets/RevealArea.uss"));

                return field;
            }
        }

        public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var ate = ve as RevealArea;
            }
        }
    }
}
