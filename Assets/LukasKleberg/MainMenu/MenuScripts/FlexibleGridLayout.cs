using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType 
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;

    public int rows;
    public int columns = 1;
    public Vector2 cellsize;
    public Vector2 spacing;
    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        //fit x stuff here
        if (fitType == FitType.Width || fitType == FitType.Height ||  fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns) 
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - 
            (padding.left / (float)columns) - (padding.right / (float)columns); ;

        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2) - 
            (padding.top / (float) rows) -  padding.bottom / (float)rows;

        cellsize.x =  fitX ? cellWidth : cellsize.x;
        cellsize.y = fitY ?  cellHeight : cellsize.y;

        int columnCount = 0;
        int rowCount = 0;

        for(int i = 0; i < rectChildren.Count; i++)
        {
            //Debug.Log("in FlexibleGridLayout. placing children in for loop");
            columnCount = i % columns;
            rowCount = i / columns;

            var item = rectChildren[i];

            var xPos = (cellsize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellsize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellsize.x);
            SetChildAlongAxis(item, 1, yPos, cellsize.y);


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
