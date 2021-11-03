using System;

public class DataArray
{
    //this maybe should not be public, debug only
    public DataElem[] elemList;
	public DataArray(string path)
	{
		string[] inputDataText = System.IO.File.ReadAllLines(path);

        elemList = new DataElem[inputDataText.Length-1];

        string names = inputDataText[0];
        for (int i = 1; i < inputDataText.Length; i++)
        {
            string data = inputDataText[i];
            elemList[i-1] = new DataElem(names,data);
        }
    }

    public DataElem[] filter(string name, string value)
    {
        //this is quite obviously a wildly inefficient use of resources
        //some kind of dynamic array sizing would be a lot better
        DataElem[] filteredList = new DataElem[elemList.Length];
        int currIndex = 0;

        for (int i = 0; i < elemList.Length; i++)
        {
            if (elemList[i].grabProperty(name) == value)
            {
                filteredList[currIndex] = elemList[i];
                currIndex++;
            }
        }
        Array.Resize(ref filteredList, currIndex);
        return filteredList;
    }
}
