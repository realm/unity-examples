using Realms;

public class HitCountExtended : RealmObject
{
    [PrimaryKey]
    public int Id { get; set; }
    public int Unmodified { get; set; }
    public int Shift { get; set; }
    public int Control { get; set; }

    private HitCountExtended() { }

    public HitCountExtended(int id)
    {
        Id = id;
    }
}
