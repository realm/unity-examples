using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceEntity Entity { get; set; }

    private Events events = default;

    private readonly Color selectedColor = new Color(1, 0, 0, 1);
    private readonly Color deselectedColor = new Color(1, 1, 1, 1);

    public void Select()
    {
        gameObject.GetComponent<Renderer>().material.color = selectedColor;
    }

    public void Deselect()
    {
        gameObject.GetComponent<Renderer>().material.color = deselectedColor;
    }

    private void OnMouseDown()
    {
        events.PieceClickedEvent.Invoke(this);
    }

    private void Awake()
    {
        events = FindObjectOfType<Events>();
    }

    private void Start()
    {
        // Subscribe to changes of properties which will identify movement and a `Piece` being attacked.
        Entity.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            // The `IsValid` property indicates if a `RealmObject` still exists. It only ever changes once, from `true` to `false`
            // when a `RealmObject` (`Entity` in this case) is removed from it's Realm.
            case nameof(Entity.IsValid):
                if (!Entity.IsValid)
                {
                    // If the `Entity` was removed we also need to remove the corresponding `GameObject` for this `Piece`.
                    Destroy(gameObject);
                }
                break;
            case nameof(Entity.Position):
                // Whenever the `Position` of the `Entity` changes we need to update the `Piece` as well.
                gameObject.transform.position = Entity.Position;
                break;
        }
    }
}
