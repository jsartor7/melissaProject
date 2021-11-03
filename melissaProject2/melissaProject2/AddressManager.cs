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
		Console.WriteLine("Hello World!");
		Console.WriteLine(cityDataArray.elemList[0].dataList[0]);
	}



	public AddressManager()
	{
		loadArrays();
	}
}
