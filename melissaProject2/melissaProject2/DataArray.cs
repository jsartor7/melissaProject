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
}
