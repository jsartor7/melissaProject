using System;
using System.IO;

public class ProductCodeDataManager
{

    ProductCode[] codeArray;
    ProductCode[] sortedCodeArray;

    int numDistinctCodes;
    string outputFilePath;

    void loadInputData(bool defaultPath)
    {
        Console.WriteLine("Enter Input File Name:");
        string inputFilePath;

        //this is just so you don't have to type it out every time
        if (defaultPath)
        {
            inputFilePath = @"data\InternationalProductCodes.txt";
        }
        else
        {
            inputFilePath = Console.ReadLine();
        }
        string[] inputDataText = System.IO.File.ReadAllLines(inputFilePath);

        //make a list of ProductCode objects rather than text
        codeArray = new ProductCode[inputDataText.Length];
        for (int i = 0; i < inputDataText.Length; i++)
        {
            codeArray[i] = new ProductCode(inputDataText[i]);
        }
        Console.WriteLine($"Loaded file: \"{inputFilePath}\" \nWith {inputDataText.Length} lines of data.\n");
    }
    void grabOutputFilePath(bool defaultPath)
    {
        Console.WriteLine("Enter Output File Name:");

        //again just so you don't have to type it out
        if (defaultPath)
        {
            outputFilePath = @"data\sortedOutput.txt";
        }
        else
        {
            outputFilePath = Console.ReadLine();
        }
    }

    public void processCommandLineInputs(bool defaultPath)
    {
        loadInputData(defaultPath);
        grabOutputFilePath(defaultPath);
    }

    //i made this private because you wouldn't want to be able to call it unless right after sorting, or it might not be sorted
    void saveSortedData()
    {
        //fileIO
        string[] lines = new string[sortedCodeArray.Length];
        //string[] sanitizedLines = new string[sortedCodeArray.Length];
        for (int i = 0; i < sortedCodeArray.Length; i++)
        {
            lines[i] = sortedCodeArray[i].text;
            //sanitizedLines[i] = sortedCodeArray[i].sanitizedText;
        }

        File.WriteAllLinesAsync(outputFilePath, lines);

        //this is just for debug purposes
        //File.WriteAllLinesAsync(outputFilePath + ".sanitized", sanitizedLines);

        Console.WriteLine($"Data saved to: \"{outputFilePath}\"\n");
    }

    //sorts the data with an "insertion sort," it's definitely sub-optimal (i.e. n^2 rather than nlogn) so depending on the system and data
    //being processed, you could choose a more optimal sorting algorithm. insertion is just relatively straightforward to make stable
    //alternatively for data that may be already (partially) sorted, this can be significantly more efficient than nlogn
    public void sortData(bool saveData)
    {
        numDistinctCodes = 1;
        ProductCode[] tempCodeArray;
        sortedCodeArray = new ProductCode[codeArray.Length];
        //go ahead and insert the first product code
        sortedCodeArray[0] = codeArray[0];
        int numInserted = 1;

        //now iterate through the whole "codeArray" of elements to insert (start at i=1 cuz we already did one)
        for (int i = 1; i < codeArray.Length; i++)
        {
            ProductCode codeToInsert = codeArray[i];
            bool inserted = false;
            bool isUnique = true;
            tempCodeArray = (ProductCode[])sortedCodeArray.Clone();

            //iterate through the sorted array finding the index at which to insert your element
            //j goes to numInserted+1 because we're increasing the size of the sorted array by 1 each iteration
            for (int j = 0; j < numInserted + 1; j++)
            {
                if (!inserted)
                {
                    ProductCode codeToCompare = tempCodeArray[j];
                    //run our fancy comparison function, a negative sortPriority means to insert
                    int sortPriority = codeToInsert.codeVersionComparison(codeToCompare);

                    //sortPriority == 0 means they are the same code
                    if (sortPriority == 0)
                    {
                        isUnique = false;
                    }

                    //the greq here ensures the "stability" of the sort - we'll insert new elements *after* eqalities
                    if (sortPriority >= 0)
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
                            numDistinctCodes++;
                        }
                    }
                }
                else if (j < codeArray.Length)
                {
                    //push forward the rest of the array
                    sortedCodeArray[j] = tempCodeArray[j - 1];
                }
            }
        }


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