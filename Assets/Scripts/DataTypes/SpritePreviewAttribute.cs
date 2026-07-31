using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Draws a Sprite field as a colored square swatch in the inspector.
/// Usage: [SpritePreview(nameof(bgColor))] public Sprite icon;
/// The named color field is read live from the same SerializedObject each repaint,
/// so if another script changes it (e.g. Drug.DefineColor), the swatch updates automatically.
/// </summary>
public class SpritePreviewAttribute : PropertyAttribute
{
    public readonly string colorFieldName;
    public readonly float size;

    public SpritePreviewAttribute(string colorFieldName = null, float size = 64f)
    {
        this.colorFieldName = colorFieldName;
        this.size = size;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
public class SpritePreviewDrawer : PropertyDrawer
{
    private const float Padding = 4f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var attr = (SpritePreviewAttribute)attribute;
        return EditorGUIUtility.singleLineHeight + Padding + attr.size;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (SpritePreviewAttribute)attribute;

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(fieldRect, property, label);

        Sprite sprite = property.objectReferenceValue as Sprite;
        Color tint = ResolveTint(property, attr.colorFieldName);

        Rect swatchRect = new Rect(position.x, fieldRect.yMax + Padding, attr.size, attr.size);
        DrawSwatch(swatchRect, sprite, tint);

        EditorGUI.EndProperty();
    }

    private static Color ResolveTint(SerializedProperty spriteProperty, string colorFieldName)
    {
        if (string.IsNullOrEmpty(colorFieldName))
            return Color.white;

        string path = spriteProperty.propertyPath;
        int lastDot = path.LastIndexOf('.');
        string siblingPath = lastDot >= 0 ? path.Substring(0, lastDot + 1) + colorFieldName : colorFieldName;

        SerializedProperty colorProperty = spriteProperty.serializedObject.FindProperty(siblingPath);
        return colorProperty != null && colorProperty.propertyType == SerializedPropertyType.Color
            ? colorProperty.colorValue
            : Color.white;
    }

    private static void DrawSwatch(Rect rect, Sprite sprite, Color tint)
    {
        // border
        EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.4f));

        Rect inner = new Rect(rect.x + 1f, rect.y + 1f, rect.width - 2f, rect.height - 2f);

        Texture texture = sprite != null
            ? (Texture)AssetPreview.GetAssetPreview(sprite) ?? sprite.texture
            : Texture2D.whiteTexture;

        Color prevColor = GUI.color;
        GUI.color = tint;
        GUI.DrawTexture(inner, texture, ScaleMode.ScaleToFit);
        GUI.color = prevColor;
    }
}
#endif
