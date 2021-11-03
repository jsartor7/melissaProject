using System;

public class DataElem
{
	string[] nameList;
	string[] dataList;

	public string grabProperty(string name)
    {
		int index = Array.IndexOf(nameList,name);
		if (index > -1)
        {
			return dataList[index];
        }
		else
        {
			return null;
        }
		
    }

	public DataElem(string names, string data)
	{
		//grab the names from names, and the data from data
		nameList = names.Split("|");
		dataList = data.Split("|");

	}
}
