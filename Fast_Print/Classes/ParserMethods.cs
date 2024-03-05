// Purpose: This class is used to parse the part number and clean it up for searching in the file system.

namespace Fast_Print.Classes;

public class ParserMethods
{
    public string StringChecker(string partNumber)
    {
        // Check if the part number is a string
        if (partNumber.EndsWith("-1-1") || partNumber.EndsWith("-1-2"))
            return partNumber.Substring(0, partNumber.Length - 4);

        if (partNumber.EndsWith("-1") || partNumber.EndsWith("-2") || partNumber.EndsWith("-3")
            || partNumber.EndsWith("-4") || partNumber.EndsWith("-5")
            || partNumber.EndsWith("-6") || partNumber.EndsWith("-7")
            || partNumber.EndsWith("-8") || partNumber.EndsWith("-9")
            || partNumber.EndsWith("-0"))
        {
            partNumber = partNumber.Substring(0, partNumber.Length - 2);
            return partNumber;
        }

        if (partNumber == "NON-INV")
            // If the part number is NON-INV, delete it
            return "";
            //TODO: Delete Corresponding Row
            // Mission Impossible

        return partNumber;
    }
}