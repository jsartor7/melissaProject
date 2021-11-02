using System;

namespace meslissaProject1
{

    public class AddressDataManager
    {
        string[] inputData;
        string[] sortedData;
        int numDistinctCodes;
        string outputFilePath;
        void loadInputData()
        {
            Console.WriteLine("Enter Input File Name:");
            string inputFilePath = Console.ReadLine();

            //no need to type this out every time for now
            inputFilePath = @"data\InternationalProductCodes.txt";
            inputData = System.IO.File.ReadAllLines(inputFilePath);
            Console.WriteLine($"Loaded file: \"{inputFilePath}\" \nWith {inputData.Length} lines of data.\n");
        }
        void grabOutputFilePath()
        {
            //fill this in later
            Console.WriteLine("Enter Output File Name:");
            outputFilePath = Console.ReadLine();
            outputFilePath = "temp";
        }

        public void processCommandLineInputs()
        {
            loadInputData();
            grabOutputFilePath();
        }

        public void sortData(bool saveData)
        {
            //do some stuff

            if (saveData)
            {

            }
            numDistinctCodes = inputData.Length;
        }

        public int getNumDistinctCodes()
        {
            return numDistinctCodes;
        }
    }

    class Program
    {

        static string[] sortInputData(string[] inputData)
        {
            string[] sortedData = inputData;
            return sortedData;
        }

        static void saveSortedData(string outputFileHandle)
        {
            //placeholder for now
        }

        static void Main(string[] args)
        {
            AddressDataManager dataManager = new AddressDataManager();
            
            //handle input and output files
            dataManager.processCommandLineInputs();

            //sort and save data
            dataManager.sortData(true);

            //write command line output
            Console.WriteLine($"There are {dataManager.getNumDistinctCodes()} distinct product codes in the file");
        }
    }
}
