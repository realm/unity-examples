using Realms;

public class HitCount: RealmObject
{
    [PrimaryKey]
    public int Id { get; set; }
    public int Value { get; set; }

    private HitCount() { }

    public HitCount(int id)
    {
        Id = id;
    }
}
