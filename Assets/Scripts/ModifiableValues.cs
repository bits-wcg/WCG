[System.Serializable]
public  class ModifiableValues
{
    public string title;
    public Values value;
    
    
    public enum Values
    {
        INR,
        UNITS,
        CYCLES,
        HEADER,
        YEARS,
        INR_M,
        UNITS_C,
        Percent
    }
}