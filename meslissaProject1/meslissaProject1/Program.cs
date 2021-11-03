using System;

namespace meslissaProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductCodeDataManager codeManager = new ProductCodeDataManager();
            
            //grab input and output file paths, also load input files
            //you can change argument from "false" to "true" to skip the input and use default values
            codeManager.processCommandLineInputs(false);

            //sort (and save) data
            codeManager.sortData(true);

            //write command line output
            Console.WriteLine($"There are {codeManager.getNumDistinctCodes()} distinct product codes in the file");
        }
    }
}
