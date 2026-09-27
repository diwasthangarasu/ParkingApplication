namespace ParkingApplication.Model;

public class ParkingLevel
{
    public int LevelNumber { get; init; }
    public List<ParkingSlot> Slots { get; private set; }
    public ParkingLevel(int levelNumber, int numberOfSlots)
    {
        this.LevelNumber = levelNumber;
        this.Slots = new ();
        for (int i = 1; i <= numberOfSlots; i++)
        {
            this.Slots.Add(new ParkingSlot(i));
        }
    }
}
