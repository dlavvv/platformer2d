using UnityEngine;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void MoveArrowToButton(RectTransform button)
    {
        rect.position = new Vector3(rect.position.x, button.position.y, 0);
        SoundManager.instance.PlaySound(interactSound);
    }
}
