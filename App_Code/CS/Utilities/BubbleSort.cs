using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for BubbleSort
/// </summary>
public class BubbleSort
{
	public BubbleSort()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int getValues()
    {
        arraySort MySort = new arraySort();
        MySort.x = 6;

        //Get array elements
        for (int i = 0; i < MySort.x; i++)
        {
            string s1 = "";
            MySort.a[i] = Int32.Parse(s1);
        }

        //Sort the array
        MySort.sortArray();
        return MySort.a[0];
    }
}


public class arraySort
{
    //array of integers to hold values
    public int[] a = new int[100];

    //number of elements in the array
    public int x;

    //Bubble sort algorithm
    public void sortArray()
    {
        int i;
        int j;
        int temp;

        for (i = (x - 1); i > 0; i--)
        {
            for (j = 1; j <= i; j++)
            {
                if (a[j - 1] > a[j])
                {
                    temp = a[j - 1];
                    a[j - 1] = a[j];
                    a[j] = temp;

                }
            }
        }
    }



}
