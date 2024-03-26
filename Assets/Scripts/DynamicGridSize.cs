using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGridSize : MonoBehaviour
{
    public int spacing = 5; // The spacing between images
    public int paddingHorizontal = 10; // Total horizontal padding

    private GridLayoutGroup gridLayoutGroup;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        // Call the function to adjust the grid layout
        AdjustGridLayout();
    }

    void AdjustGridLayout()
    {
        if (gridLayoutGroup == null) return;

        // Calculate the size of each cell to make them square and fit 3 across the screen width
        float screenWidth = Screen.width;
        Debug.Log(screenWidth.ToString());

        // Adjust for padding and spacing to get total usable space
        float usableWidth = screenWidth - paddingHorizontal - (spacing * 2); // Assuming 2 spaces (3 columns)
        float cellSize = usableWidth / 3; // Divide by 3 to get the size for each cell
        Debug.Log(cellSize.ToString());

        // Set the calculated cell size
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        // Optionally adjust spacing and padding if needed
        gridLayoutGroup.spacing = new Vector2(spacing, spacing);
        gridLayoutGroup.padding.left = gridLayoutGroup.padding.right = paddingHorizontal / 2;
    }
}
