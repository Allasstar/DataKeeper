using UnityEngine;
using UnityEngine.UI;

namespace DataKeeper.Components.UI
{
    [AddComponentMenu("DataKeeper/UI/Auto Grid Layout Group")]
    public class AutoGridLayoutGroup : LayoutGroup
    {
        public enum Corner
        {
            UpperLeft = 0,
            UpperRight = 1,
            LowerLeft = 2,
            LowerRight = 3
        }

        public enum Axis
        {
            Horizontal = 0,
            Vertical = 1
        }

        public enum Constraint
        {
            FixedColumnCount = 1,
            FixedRowCount = 2
        }

        [SerializeField] protected Corner m_StartCorner = Corner.UpperLeft;
        [SerializeField] protected Vector2 m_Spacing = Vector2.zero;
        [SerializeField] protected float m_ChildAspectRatio = 1f;
        [SerializeField] protected Constraint m_Constraint = Constraint.FixedColumnCount;
        [SerializeField] protected int m_ConstraintCount = 2;

        public Corner startCorner { get { return m_StartCorner; } set { SetProperty(ref m_StartCorner, value); } }

        protected Axis m_StartAxis = Axis.Horizontal;
        public Axis startAxis { get { return m_StartAxis; } set { SetProperty(ref m_StartAxis, value); } }

        public Vector2 spacing { get { return m_Spacing; } set { SetProperty(ref m_Spacing, value); } }

        public Constraint constraint { get { return m_Constraint; } set { SetProperty(ref m_Constraint, value); } }

        public int constraintCount { get { return m_ConstraintCount; } set { SetProperty(ref m_ConstraintCount, Mathf.Max(1, value)); } }

        public float childAspectRatio { get { return m_ChildAspectRatio; } set { SetProperty(ref m_ChildAspectRatio, value); } }

        protected AutoGridLayoutGroup()
        {}

        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            constraintCount = constraintCount;
        }
        #endif

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int minColumns = 1;
            int preferredColumns = 1;

            if (m_Constraint == Constraint.FixedColumnCount)
            {
                startAxis = Axis.Horizontal;
                minColumns = preferredColumns = m_ConstraintCount;
            }
            else if (m_Constraint == Constraint.FixedRowCount)
            {
                startAxis = Axis.Vertical;
                minColumns = preferredColumns = Mathf.CeilToInt(rectChildren.Count / (float)m_ConstraintCount);
            }

            SetLayoutInputForAxis(
                padding.horizontal + (rectTransform.rect.width / minColumns + spacing.x) * minColumns - spacing.x,
                padding.horizontal + (rectTransform.rect.width / preferredColumns + spacing.x) * preferredColumns - spacing.x,
                -1, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            int minRows = 1;
            float cellWidth = (rectTransform.rect.width - padding.horizontal - spacing.x * (m_ConstraintCount - 1)) / m_ConstraintCount;
            float cellHeight = cellWidth / m_ChildAspectRatio;

            if (m_Constraint == Constraint.FixedColumnCount)
            {
                minRows = Mathf.CeilToInt(rectChildren.Count / (float)m_ConstraintCount);
            }
            else if (m_Constraint == Constraint.FixedRowCount)
            {
                minRows = m_ConstraintCount;
            }

            float minSpace = padding.vertical + (cellHeight + spacing.y) * minRows - spacing.y;
            SetLayoutInputForAxis(minSpace, minSpace, -1, 1);
        }

        public override void SetLayoutHorizontal()
        {
            SetCellsAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            SetCellsAlongAxis(1);
        }

        private void SetCellsAlongAxis(int axis)
        {
            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            int cellCountX = 1;
            int cellCountY = 1;

            if (m_Constraint == Constraint.FixedColumnCount)
            {
                cellCountX = m_ConstraintCount;
                cellCountY = Mathf.CeilToInt(rectChildren.Count / (float)cellCountX);
            }
            else if (m_Constraint == Constraint.FixedRowCount)
            {
                cellCountY = m_ConstraintCount;
                cellCountX = Mathf.CeilToInt(rectChildren.Count / (float)cellCountY);
            }

            Vector2 cellSize = Vector2.zero;
            if (m_Constraint == Constraint.FixedColumnCount)
            {
                cellSize.x = (width - spacing.x * (cellCountX - 1) - padding.horizontal) / cellCountX;
                cellSize.y = cellSize.x / m_ChildAspectRatio;
            }
            else // FixedRowCount
            {
                cellSize.y = (height - spacing.y * (cellCountY - 1) - padding.vertical) / cellCountY;
                cellSize.x = cellSize.y * m_ChildAspectRatio;
            }

            int cornerX = (int)startCorner % 2;
            int cornerY = (int)startCorner / 2;

            int cellsPerMainAxis, actualCellCountX, actualCellCountY;
            if (startAxis == Axis.Horizontal)
            {
                cellsPerMainAxis = cellCountX;
                actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildren.Count);
                actualCellCountY = Mathf.Clamp(cellCountY, 1, Mathf.CeilToInt(rectChildren.Count / (float)cellsPerMainAxis));
            }
            else
            {
                cellsPerMainAxis = cellCountY;
                actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildren.Count);
                actualCellCountX = Mathf.Clamp(cellCountX, 1, Mathf.CeilToInt(rectChildren.Count / (float)cellsPerMainAxis));
            }

            Vector2 requiredSpace = new Vector2(
                actualCellCountX * cellSize.x + (actualCellCountX - 1) * spacing.x,
                actualCellCountY * cellSize.y + (actualCellCountY - 1) * spacing.y
            );
            Vector2 startOffset = new Vector2(
                GetStartOffset(0, requiredSpace.x),
                GetStartOffset(1, requiredSpace.y)
            );

            for (int i = 0; i < rectChildren.Count; i++)
            {
                int positionX;
                int positionY;
                if (startAxis == Axis.Horizontal)
                {
                    positionX = i % cellsPerMainAxis;
                    positionY = i / cellsPerMainAxis;
                }
                else
                {
                    positionX = i / cellsPerMainAxis;
                    positionY = i % cellsPerMainAxis;
                }

                if (cornerX == 1)
                    positionX = actualCellCountX - 1 - positionX;
                if (cornerY == 1)
                    positionY = actualCellCountY - 1 - positionY;

                SetChildAlongAxis(rectChildren[i], 0, startOffset.x + (cellSize[0] + spacing[0]) * positionX, cellSize[0]);
                SetChildAlongAxis(rectChildren[i], 1, startOffset.y + (cellSize[1] + spacing[1]) * positionY, cellSize[1]);
            }
        }
    }
}