

public enum CardType
{
    NULL,
    // Identity
    Identity,

    // Corp
    Operation, Agenda, Ice, Upgrade, Asset,

    // Runner
    Hardware, Resource, Program, Event
}


public enum CardSubType
{
    NULL,
    // Identity

        // Corp
        MegaCorp,

        // Runner
        Cyborg,


    // Operation
    Dark_Ops, Transaction,

    // Agenda
    Initiate, Security,

    // Asset
    Ambush, Advertisement,

    // Ice
    Gate, Barrier, Sentry, Trap, Trance,

    // Upgrade
    Facility,


    // Hardware
    Console,

    // Resource
    Job, Connection, Location, Link,

    // Program
    Icebreaker,

    // Event
    




}

public static class ExtensionMethods
{
    public static string TypeToString(this CardSubType cardSubType)
	{
        if (cardSubType == CardSubType.NULL) return "";
        return ToString(cardSubType.ToString());
	}

    public static string TypeToString(this CardType cardType)
    {
        if (cardType == CardType.NULL) return "";
        return ToString(cardType.ToString());
    }

    static string ToString(string typeString)
	{
        return typeString.Replace("_", " ");
	}

}






























