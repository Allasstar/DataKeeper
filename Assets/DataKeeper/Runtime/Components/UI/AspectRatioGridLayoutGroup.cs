using UnityEngine;
using UnityEngine.UI;

namespace DataKeeper.Components.UI
{
    [AddComponentMenu("DataKeeper/UI/Aspect Ratio Grid Layout Group")]
    public class AspectRatioGridLayoutGroup : LayoutGroup
    {
        public enum LayoutType
        {
            FixedRows,
            FixedColumns
        }

        public LayoutType layoutType = LayoutType.FixedRows;
        [Min(1)] public int fixedCount = 1;
        public float aspectRatio = 1f;
        public Vector2 spacing = Vector2.zero;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
    
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth, cellHeight;
            int rows = fixedCount;
            int columns = fixedCount;
            

            if (layoutType == LayoutType.FixedRows)
            {
                cellHeight = (parentHeight - padding.top - padding.bottom - spacing.y * (rows - 1)) / rows;
                cellWidth = cellHeight / aspectRatio;
                
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    int rowIndex = i % rows;
                    int columnIndex = i / rows;
        
                    var item = rectChildren[i];
        
                    var xPos = padding.left + (cellWidth + spacing.x) * columnIndex;
                    var yPos = padding.top + (cellHeight + spacing.y) * rowIndex;
        
                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else // FixedColumns
            {
                cellWidth = (parentWidth - padding.left - padding.right - spacing.x * (columns - 1)) / columns;
                cellHeight = cellWidth * aspectRatio;
                
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    int rowIndex = i / columns;
                    int columnIndex = i % columns;
        
                    var item = rectChildren[i];
        
                    var xPos = padding.left + (cellWidth + spacing.x) * columnIndex;
                    var yPos = padding.top + (cellHeight + spacing.y) * rowIndex;
        
                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}