using System;
using System.IO;

public class AddressManager
{
	DataArray cityDataArray;
	DataArray streetsDataArray;
	DataArray buildingsDataArray;
	DataArray apartmentsDataArray;

	//these will be our output
	string[] formattedData;
	string[] formattedPropertyList;

	//grab these four files and load them in as DataArray objects
	private void loadArrays()
	{
		cityDataArray = new DataArray("data/Cities.txt");
		streetsDataArray = new DataArray("data/Streets.txt");
		buildingsDataArray = new DataArray("data/Buildings.txt");
		apartmentsDataArray = new DataArray("data/Apartments.txt");

		formattedPropertyList = new string[5] { "StateName", "CityName", "PostalCode", "StreetName", "DeliveryPoints" };
	}

	//this function is a bit of a mess - it could definitely improved by putting all the data into a structure and then
	//separately building up the strings
	private void combineArrays()
    {
		DataElem[] streetList = streetsDataArray.elemList;
		formattedData = new string[streetList.Length+1];
		formattedData[0] = string.Join("|", formattedPropertyList);

		//each line in the final output is associated with one street
		for (int i = 0; i < streetList.Length; i++)
		{
			DataElem currStreet = streetList[i];
			string outStr = "";

			//first, grab the city that is associated with that street, and add it's state and name
			DataElem cityRow = cityDataArray.filter("CityId", currStreet.grabProperty("CityId"))[0];
			string stateName = cityRow.grabProperty("StateName");
			outStr = outStr + stateName + "|";
			string cityName = cityRow.grabProperty("CityName");
			outStr = outStr + cityName + "|";

			//then grab the postal code and name of the street and add that (neglecting that plenty of streets span multiple postal codes, I guess)
			string postalCode = currStreet.grabProperty("PostalCode");
			outStr = outStr + postalCode + "|";
			string streetName = currStreet.grabProperty("StreetName");
			outStr = outStr + streetName + "|";

			//now we use our filter function to find all the buildings that are on that street
			DataElem[] buildingsList = buildingsDataArray.filter("StreetId", currStreet.grabProperty("StreetId"));
			for(int j=0; j<buildingsList.Length; j++)
            {
				DataElem currBuilding = buildingsList[j];
				outStr = outStr + currBuilding.grabProperty("BuildingNumber");

				//and similarly, all the apartments (if any) which are in that building)
				DataElem[] apartmentList = apartmentsDataArray.filter("BuildingId", currBuilding.grabProperty("BuildingId"));
				if (apartmentList.Length>0)
				{
					outStr = outStr + "(";
					for (int k = 0; k < apartmentList.Length; k++)
					{
						outStr = outStr + apartmentList[k].grabProperty("ApartmentNumber");
						if (k < apartmentList.Length - 1)
                        {
							outStr = outStr + ",";
                        }
					}
						outStr = outStr + ")";
				}

				if (j < buildingsList.Length - 1)
				{
					outStr = outStr + ";";
				}
			}

			formattedData[i+1] = outStr;

		}

    }
	
	//save our output
	private void saveFormattedData(string outputPath)
    {
		File.WriteAllLinesAsync(outputPath, formattedData);
	}

	public AddressManager(string outputPath)
	{
		loadArrays();
		combineArrays();
		saveFormattedData(outputPath);
	}
}
