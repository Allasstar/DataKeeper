using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Imagine"), DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(ImageGraphic))]
public class Imagine : MonoBehaviour
{
    private static Lazy<Material> _material = new Lazy<Material>(() => Resources.Load<Material>("Imagine Material"));
    private static Lazy<Sprite> _whiteSprite = new Lazy<Sprite>(() => Resources.Load<Sprite>("4x4_white"));
    private static Lazy<Sprite> _emptySprite = new Lazy<Sprite>(() => Resources.Load<Sprite>("4x4_empty"));
    
    
    private ImageGraphic _imageGraphic;
    private CanvasRenderer _canvasRenderer;
    private Canvas _canvas;

    [SerializeField] private Color _color = Color.white;
    [SerializeField, Min(0)] private float _scale = 1;
    [SerializeField, Min(0)] private float _stroke = 0;
    [SerializeField] private Rect _cornerRadius = new Rect(0, 0, 0, 0);

    private void OnEnable()
    {
        GetAllComponents();
    }
    
    private void OnValidate()
    {
        ForceUpdate();
    }
    
    private void OnDestroy()
    {
        if(_imageGraphic!= null)
            DestroyImmediate(_imageGraphic);
    }
    
    public void OnTransformParentChanged()
    {
        ForceUpdate();
    }

    private void GetAllComponents()
    {
        _imageGraphic = GetComponent<ImageGraphic>();
        _canvasRenderer = _imageGraphic.canvasRenderer;
        _canvas = GetComponentInParent<Canvas>();
    }

    [ContextMenu("Force Update")]
    private void ForceUpdate()
    {
        SetAdditionalShaderChannels();
        SetMaterial();
        SetSprite();
        SetColor();
        SetVisibility();
        ValidateCorners();
    }

    private void ValidateCorners()
    {
        var rect = _imageGraphic.GetPixelAdjustedRect();

        var minSide = Mathf.Min(rect.width, rect.height) / 2f;
        
        _cornerRadius.x = Mathf.Max(_cornerRadius.x, 0);
        _cornerRadius.y = Mathf.Max(_cornerRadius.y, 0);
        _cornerRadius.width = Mathf.Max(_cornerRadius.width, 0);
        _cornerRadius.height = Mathf.Max(_cornerRadius.height, 0);

        _cornerRadius.x = Mathf.Min(_cornerRadius.x, minSide);
        _cornerRadius.y = Mathf.Min(_cornerRadius.y, minSide);
        _cornerRadius.width = Mathf.Min(_cornerRadius.width, minSide);
        _cornerRadius.height = Mathf.Min(_cornerRadius.height, minSide);
    }
    
    private void SetVisibility()
    {
        _imageGraphic.hideFlags = HideFlags.NotEditable;
        _imageGraphic.material.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
    
    private void SetColor()
    {
        _imageGraphic.color = _color;
    }

    private void SetMaterial()
    {
        _imageGraphic.material = _material.Value;
    }
    
    private void SetSprite()
    {
        _imageGraphic.sprite = _whiteSprite.Value;
    }
    
    protected void SetAdditionalShaderChannels()
    {
        _canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2 | AdditionalCanvasShaderChannels.TexCoord3;
    }
    
    private Vector4 CornerRadius()
    {
        var vec = new Vector4(_cornerRadius.y, _cornerRadius.height, _cornerRadius.x, _cornerRadius.width);
        return vec;
    }

    public void SetVertexData(VertexHelper fill)
    {
        UIVertex vert = UIVertex.simpleVert;
        
        var rect = _imageGraphic.GetPixelAdjustedRect();
        var uv1 = CornerRadius();
        var uv2 = new Vector4(rect.width, rect.height, _stroke, 0);
        
        for (int i = 0; i < fill.currentVertCount; i++)
        {
            fill.PopulateUIVertex(ref vert, i);

            vert.position += ((Vector3)vert.uv0 - new Vector3(0.5f, 0.5f)) * _scale;
            vert.uv1 = uv1;
            vert.uv2 = uv2;
            fill.SetUIVertex(vert, i);
        }
    }


}


