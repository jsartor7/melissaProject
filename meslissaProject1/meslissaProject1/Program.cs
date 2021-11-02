using System;

namespace meslissaProject1
{
    class Program
    {

        static string[] grabInputData()
        {
            Console.WriteLine("Enter Input File Name:");
            string inputFileName = Console.ReadLine();
            inputFileName = @"C:\Users\craig\Documents\softwaredevelopmentposition\melissaProject\data\Buildings.txt";
            string[] inputData = System.IO.File.ReadAllLines(inputFileName);
            return inputData;
        }

        static string grabOutputFileHandle()
        {
            //fill this in later
            Console.WriteLine("Enter Output File Name:");
            string outputFileName = Console.ReadLine();
            string outputFileHandle = "temp";
            return outputFileHandle;
        }

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
            //handle input file
            string[] inputData = grabInputData();

            //handle output file
            string outputFileHandle = grabOutputFileHandle();

            //sort data
            //todo: also grab numDistinctCodes
            string[] sortedData = sortInputData(inputData);

            //save data
            saveSortedData(outputFileHandle);

            //write command line output
            int numDistinctCodes = 4;
            Console.WriteLine($"There are {numDistinctCodes} distinct product codes in the file");
        }
    }
}
