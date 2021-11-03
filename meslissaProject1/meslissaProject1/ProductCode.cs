using System;

//this class stores the data of a single product code
public class ProductCode
{
    //the original text input for the product code
    public string text;

    //"text" with punctuation and spacing removed
    public string sanitizedText;

    //an array of blocks which are each either all text or numerical
    public string[] blocks;

    const int maxBlocks = 8;

    //cleans out unnecessary punctuation and makes everything uppercase to generate sanitizedText from "text"
    private void sanitizeText()
    {
        sanitizedText = text;
        sanitizedText = sanitizedText.Replace(".", "");
        sanitizedText = sanitizedText.Replace("/", "");
        sanitizedText = sanitizedText.Replace("-", "");
        sanitizedText = sanitizedText.Replace(" ", "");
        sanitizedText = sanitizedText.ToUpper();
    }

    //builds the array of blocks
    private void processBlocks()
    {
        blocks = new string[maxBlocks];
        int blockPos = 0;
        bool lastType = false;
        bool numType;
        blocks[0] = "";

        for (int i = 0; i < sanitizedText.Length; i++)
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
            blocks[blockPos] = blocks[blockPos].Insert(blocks[blockPos].Length, currChar.ToString());

            lastType = numType;
        }
    }


    private int blockComparison(string block1, string block2)
    {
        //null blocks mean you sort before
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


        int num1;
        int num2;
        bool block1IsNumerical = int.TryParse(block1, out num1);
        bool block2IsNumerical = int.TryParse(block2, out num2);

        //numerical blocks sort before alphabetical blocks
        if (block1IsNumerical && !block2IsNumerical)
        {
            return -1;
        }
        else if (!block1IsNumerical && block2IsNumerical)
        {
            return 1;
        }
        else if (block1IsNumerical && block2IsNumerical)
        {
            //smaller numbers sort before larger numbers
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

        //and then finally, if they are both alphabetical, we'll run Char.CompareTo on each pair of characters
        int sortPriority = 0;
        for (int i = 0; i < Math.Min(block1.Length, block2.Length); i++)
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
            //shorter blocks sort before longer blocks
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
        //this happens when you're comparing with the last spot in the array, which will be empty so you insert there
        if (compareCode is null)
        {
            return -1;
        }

        //your sort priority will ultimately be the sort priority of the first pair of blocks that don't give 0
        int sortPriority = 0;
        for (int i = 0; i < maxBlocks; i++)
        {
            sortPriority = blockComparison(blocks[i], compareCode.blocks[i]);
            if (sortPriority != 0)
            {
                break;
            }
        }

        return sortPriority;
    }

    //constructor initializes the text, cleans it up, and forms the blocks
    public ProductCode(string inputText)
    {
        text = inputText;
        sanitizeText();
        processBlocks();
    }
}
