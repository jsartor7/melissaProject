using System;

namespace meslissaProject1
{
    //this class stores the data of a single product code
    public class ProductCode
    {
        //the original text input for the code
        public string text;
        //the text with punctuation and spacing removed
        public string sanitizedText;
        //an array of blocks which are either all text or numerical
        public string[] blocks;
        //blocks.Length
        public int numBlocks;

        const int maxBlocks = 8;

        //generates sanitizedText from "text"
        private void sanitizeText()
        {
            sanitizedText = text;
            sanitizedText = sanitizedText.Replace(".", "");
            sanitizedText = sanitizedText.Replace("/", "");
            sanitizedText = sanitizedText.Replace("-", "");
            sanitizedText = sanitizedText.Replace(" ", "");
            sanitizedText = sanitizedText.ToLower();
        }

        //builds the array of blocks
        private void processBlocks()
        {
            blocks = new string[maxBlocks];
            int blockPos = 0;
            bool lastType = false;
            bool numType;
            blocks[0] = "";

            for(int i = 0; i < sanitizedText.Length; i++)
            {
                char currChar = sanitizedText[i];
                numType = Char.IsDigit(currChar);

                //this condition is checking if we have switched between numbers and letters
                if (i > 0 && numType != lastType)
                {
                    blockPos++;
                    blocks[blockPos] = "";
                }

                //we are just appending the current character to the end of our string
                blocks[blockPos]=blocks[blockPos].Insert(blocks[blockPos].Length,currChar.ToString());

                lastType = numType;
            }
            numBlocks = blocks.Length;
        }

        
        private int blockComparison(string block1, string block2)
        {
            if (block1 is null && block2 is null)
            {
                return 0;
            }
            else if (block1 is null && block2 is not null)
            {
                return -1;
            }
            else if (block1 is not null && block2 is null)
            {
                return 1;
            }

            int sortPriority = 0;

            int num1;
            int num2;
            bool block1Numerical = int.TryParse(block1, out num1);
            bool block2Numerical = int.TryParse(block2, out num2);
            if (block1Numerical && block2Numerical)
            {
                if (num1 > num2)
                {
                    return 1;
                }
                else if (num2 > num1)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else if (block1Numerical && !block2Numerical)
            {
                return -1;
            }
            else if (!block1Numerical && block2Numerical)
            {
                return 1;
            }


            for (int i = 0; i < Math.Min(block1.Length, block2.Length);i++)
            {
                char char1 = block1[i];
                char char2 = block2[i];
                sortPriority = char1.CompareTo(char2);
                if (sortPriority != 0)
                {
                    break;
                }
            }
            if (sortPriority == 0)
            {
                //check that these aren't backwards at some point
                if (block1.Length > block2.Length)
                {
                    sortPriority = 1;
                }
                else if (block1.Length < block2.Length)
                {
                    sortPriority = -1;
                }
            }
            return sortPriority;
        }

        //this is an int because it has three values: positive if compareCode sorts before, negative if after, and 0 if equal
        public int codeVersionComparison(ProductCode compareCode)
        {
            if (compareCode is null)
            {
                return -1;
            }

            int sortPriority = 0;
            for(int i = 0; i < maxBlocks; i++)
            {
                sortPriority = blockComparison(blocks[i], compareCode.blocks[i]);
                if (sortPriority != 0)
                {
                    break;
                }
            }

            return sortPriority;
        }

        public ProductCode(string inputText)
        {
            text = inputText;
            sanitizeText();
            processBlocks();
        }
    }

    public class ProductCodeDataManager
    {
        //ultimately this is temp and not needed
        string[] inputDataText;

        ProductCode[] codeArray;
        ProductCode[] sortedCodeArray;
        string[] sortedDataText;
        int numDistinctCodes;
        string outputFilePath;
        void loadInputData()
        {
            Console.WriteLine("Enter Input File Name:");
            //string inputFilePath = Console.ReadLine();

            //no need to type this out every time for now
            string inputFilePath = @"data\InternationalProductCodes.txt";
            inputDataText = System.IO.File.ReadAllLines(inputFilePath);
            codeArray = new ProductCode[inputDataText.Length];
            for(int i = 0; i < inputDataText.Length; i++)
            {
                codeArray[i] = new ProductCode(inputDataText[i]);
            }
            Console.WriteLine($"Loaded file: \"{inputFilePath}\" \nWith {inputDataText.Length} lines of data.\n");
        }
        void grabOutputFilePath()
        {
            //fill this in later
            Console.WriteLine("Enter Output File Name:");
            //outputFilePath = Console.ReadLine();
            outputFilePath = "temp";

        }

        public void processCommandLineInputs()
        {
            loadInputData();
            grabOutputFilePath();
        }
        
        void saveSortedData()
        {
            //fileIO bullshit

            Console.WriteLine($"Data saved to: \"{outputFilePath}\"\n");
        }

        public void sortData(bool saveData)
        {
            //sort the data
            numDistinctCodes = 1;
            ProductCode[] tempCodeArray;
            sortedCodeArray = new ProductCode[codeArray.Length];
            sortedCodeArray[0] = codeArray[0];
            int numInserted = 1;
            int numUnique = 1;
            bool isUnique = true;

            //this is an insertion sort, depending on the system and data being processed a different sorting algorithm could be more optimal
            for (int i = 1; i < codeArray.Length; i++)
            {
                ProductCode codeToInsert = codeArray[i];
                bool inserted = false;
                tempCodeArray = (ProductCode[])sortedCodeArray.Clone();
                for (int j = 0; j < numInserted+1; j++)
                {
                    if (!inserted)
                    {
                        ProductCode codeToCompare = tempCodeArray[j];
                        int sortPriority = codeToInsert.codeVersionComparison(codeToCompare);

                        if (sortPriority == 0)
                        {
                            isUnique = false;
                        }
                        if (sortPriority > 0)
                        {
                            sortedCodeArray[j] = tempCodeArray[j];
                        }
                        else
                        {
                            sortedCodeArray[j] = codeToInsert;
                            inserted = true;
                            numInserted++;
                            if (isUnique)
                            {
                                numUnique++;
                            }
                        }
                    }
                    else if (j < codeArray.Length)
                    {
                        sortedCodeArray[j] = tempCodeArray[j-1];
                    }
                }
            }


            //fix this
            numDistinctCodes = numUnique;

            if (saveData)
            {
                saveSortedData();
            }
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
            ProductCodeDataManager codeManager = new ProductCodeDataManager();
            
            //handle input and output files
            codeManager.processCommandLineInputs();

            //sort and save data
            codeManager.sortData(true);

            //write command line output
            Console.WriteLine($"There are {codeManager.getNumDistinctCodes()} distinct product codes in the file");
        }
    }
}
