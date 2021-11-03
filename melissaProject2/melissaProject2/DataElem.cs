using System;

public class DataElem
{
	string[] nameList;
	string[] dataList;

	//grab a property by column name
	public string grabProperty(string name)
    {
		int index = Array.IndexOf(nameList,name);
		if (index > -1)
        {
			return dataList[index];
        }
		else
        {
			//this could probably lead to enormous problems if the data were not properly sanitized... but it is
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
