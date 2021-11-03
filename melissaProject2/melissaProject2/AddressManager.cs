using System;
using System.IO;

public class AddressManager
{


	DataArray cityDataArray;
	DataArray streetsDataArray;
	DataArray buildingsDataArray;
	DataArray apartmentsDataArray;



	private void loadArrays()
	{
		cityDataArray = new DataArray("data/Cities.txt");
		streetsDataArray = new DataArray("data/Streets.txt");
		buildingsDataArray = new DataArray("data/Buildings.txt");
		apartmentsDataArray = new DataArray("data/Apartments.txt");
	}

	private void combineArrays()
    {
		string[] propertyList = { "StateName", "CityName", "PostalCode", "StreetName", "DeliveryPoints" };


		DataElem[] streetList = streetsDataArray.elemList;
		string[] output = new string[streetList.Length];
		for (int i = 0; i < streetList.Length; i++)
		{
			string outStr = "";
			DataElem currStreet = streetList[i];
			DataElem cityRow = cityDataArray.filter("CityId", currStreet.grabProperty("CityId"))[0];
			string stateName = cityRow.grabProperty("StateName");
			outStr = outStr + stateName + "|";

			string cityName = cityRow.grabProperty("CityName");
			outStr = outStr + cityName + "|";

			string postalCode = currStreet.grabProperty("PostalCode");
			outStr = outStr + postalCode + "|";

			string streetName = currStreet.grabProperty("StreetName");
			outStr = outStr + streetName + "|";

			DataElem[] buildingsList = buildingsDataArray.filter("StreetId", currStreet.grabProperty("StreetId"));
			for(int j=0; j<buildingsList.Length; j++)
            {
				DataElem currBuilding = buildingsList[j];
				outStr = outStr + currBuilding.grabProperty("BuildingNumber");

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


			Console.WriteLine(outStr);

		}

    }
	


	public AddressManager()
	{
		loadArrays();
		combineArrays();
	}
}
