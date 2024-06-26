using UnityEngine;
using UnityEngine.UI;

namespace DataKeeper.Components.UI
{
    public class WrapLayoutGroup : LayoutGroup
    {
        public enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        public Axis startAxis = Axis.Horizontal;
        public float spacing = 0f;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalculateLayout();
        }

        public override void CalculateLayoutInputVertical()
        {
            CalculateLayout();
        }

        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1);
        }

        private void CalculateLayout()
        {
            float currentMainAxisSize = 0f;
            float currentCrossAxisSize = 0f;
            float maxCrossAxisSize = 0f;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform rect = rectChildren[i];

                float mainAxisSize = startAxis == Axis.Horizontal ? rect.rect.width : rect.rect.height;
                float crossAxisSize = startAxis == Axis.Horizontal ? rect.rect.height : rect.rect.width;

                if (currentMainAxisSize + mainAxisSize + spacing > rectTransform.rect.size.x)
                {
                    currentMainAxisSize = 0f;
                    currentCrossAxisSize += maxCrossAxisSize + spacing;
                    maxCrossAxisSize = 0f;
                }

                currentMainAxisSize += mainAxisSize + spacing;
                maxCrossAxisSize = Mathf.Max(maxCrossAxisSize, crossAxisSize);
            }
        }

        private void SetChildrenAlongAxis(int axis)
        {
            float currentMainAxisSize = 0f;
            float currentCrossAxisSize = 0f;
            float maxCrossAxisSize = 0f;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform rect = rectChildren[i];
                float mainAxisSize = startAxis == Axis.Horizontal ? rect.rect.width : rect.rect.height;
                float crossAxisSize = startAxis == Axis.Horizontal ? rect.rect.height : rect.rect.width;

                if (currentMainAxisSize + mainAxisSize + spacing > rectTransform.rect.size.x)
                {
                    currentMainAxisSize = 0f;
                    currentCrossAxisSize += maxCrossAxisSize + spacing;
                    maxCrossAxisSize = 0f;
                }

                if (startAxis == Axis.Horizontal)
                {
                    SetChildAlongAxis(rect, 0, currentMainAxisSize, mainAxisSize);
                    SetChildAlongAxis(rect, 1, currentCrossAxisSize, crossAxisSize);
                }
                else
                {
                    SetChildAlongAxis(rect, 1, currentMainAxisSize, mainAxisSize);
                    SetChildAlongAxis(rect, 0, currentCrossAxisSize, crossAxisSize);
                }

                currentMainAxisSize += mainAxisSize + spacing;
                maxCrossAxisSize = Mathf.Max(maxCrossAxisSize, crossAxisSize);
            }
        }
    }
}