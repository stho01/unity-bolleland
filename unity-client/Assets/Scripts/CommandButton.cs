using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CommandType
{
    Attack = 0,
    Defend = 1
}

[ExecuteInEditMode]
public class CommandButton : MonoBehaviour
{
    public CommandType commandType;
    public Move move;

    public Sprite attackImage;
    public Sprite defendImage;

    private Image _image;
    private RectTransform _imageTransform;
    
    void Awake()
    {
        var iconObject = transform.Find("Icon"); 
        _image = iconObject.GetComponent<Image>();
        _image.sprite = GetIconSprite();
        _imageTransform = iconObject.GetComponent<RectTransform>();
        _imageTransform.sizeDelta = new Vector2(60, 60);
    }

    void Update()
    {
           
    }

    private Sprite GetIconSprite()
    {
        return commandType switch
        {
            CommandType.Attack => attackImage,
            CommandType.Defend => defendImage
        };
    }
}
