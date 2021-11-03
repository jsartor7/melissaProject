using System;

namespace melissaProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            //this could probably be better as an input (and also potentially one for the location of the data files)
            string dataOutputPath = "data/formattedOutputData.txt";

            AddressManager myAddressManager = new AddressManager(dataOutputPath);
        }
    }
}
