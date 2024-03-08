[System.Serializable]

public class CarsAvailabiltyStatus
{
    /// <summary>
    /// DIVIDER 1 STATS REF'S
    /// </summary>
    public string carNameBig;
    public string carNameSmall;
    public int carPower;

    //Engine
    public string engName;
    public string engType;
    //Tire
    public string tireName;
    public string tireType;
    //Suspension
    public string SpName;
    public string SpType;

    /// <summary>
    /// DIVIDER 3 STATS REF'S
    /// </summary>
    public int boxesToFill;

    /// <summary>
    /// Main variables for checking car availability status
    /// </summary>
    public int price;

    public bool isUnlocked;
}
