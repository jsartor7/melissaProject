using System;

public class DataArray
{
    //this probably shouldn't be public, but i'm not sure it matters too much here
    public DataElem[] elemList;

    //this just loads in all the data and turns it into a list of these DataElem objects
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
        //this is probably not the best way to do this, a dynamic sized array could be better
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
