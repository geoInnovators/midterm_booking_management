using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Implementations;

public class AvtandilBookingManager : IBookingManager
{
    private int[,]? hall;

    private int rows;
    private int columns;
    private int uniqueNumber = 0;

    public void Init(int rowsCount, int columnsCount)
    {
        this.rows = rowsCount;
        this.columns = columnsCount;

        hall = new int[rowsCount, columnsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                hall[i, j] = 0;
            }
        }
    }
    public Result CreateBooking(int k, BookingRequestType type)
    {
        if (hall == null)
        {
            throw new NullReferenceException("Hall is null");

        }
        uniqueNumber++;
        Result result = new()
        {
            IsSuccess = true,
            ErrorMessage = string.Empty,
            Value = uniqueNumber.ToString()
        };


        //detect free chairs
        if (k > rows * columns)
        {
            result.IsSuccess = false;
            result.ErrorMessage = "ამდენი ადგილი არ არის დარბაზში";
            result.Value = string.Empty;

            return result;
        }


        var returnedResult = DetectFreeChairs(k, type);

        if (type == BookingRequestType.Gather && returnedResult.Item1 >= k && returnedResult.Item2 > -1 && returnedResult.Item3 > -1)
        {

            FillChairs(type, k, returnedResult.Item2, returnedResult.Item3);
        }

        if (type == BookingRequestType.Scatter && returnedResult.Item1 >= k)
        {
            FillChairs(type, k);
        }
        return result;

    }

    private void FillChairs(BookingRequestType type, int count, int indexI = -1, int indexJ = -1)
    {
        int filledChairCount = 0;

        if (type == BookingRequestType.Scatter)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (filledChairCount < count)
                    {
                        if (hall[i, j] == 0)
                        {
                            hall[i, j] = uniqueNumber;
                            filledChairCount++;

                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
        }

        else
        {
            if (indexI > -1 && indexJ > -1)
            {
                for (int i = indexI; i <= indexI; i++)
                {
                    for (int j = indexJ; j > 0; j--)
                    {
                        if (hall[i, j] == 0)
                        {
                            hall[i, j] = uniqueNumber;

                            filledChairCount++;
                        }

                        if (filledChairCount == count)
                        {
                            return;
                        }
                    }
                }



            }
        }
    }
    public Result DeleteBooking(string bookingNumber)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (hall[i, j] == int.Parse(bookingNumber))
                {
                    hall[i, j] = 0;
                }
            }
        }
        return new Result { IsSuccess = true };
    }

    private (int, int, int) DetectFreeChairs(int count, BookingRequestType type)
    {

        int result = 0;
        int indexJ = -1;
        if (type == BookingRequestType.Scatter)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (hall[i, j] == 0)
                    {
                        result++;
                    }
                }
            }
        }
        else
        {
            if (count > columns)
            {
                return (0, -1, -1);

            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    if (hall[i, j] == 0)
                    {
                        indexJ = j;
                        result++;
                    }
                    else
                    {
                        result = 0;
                        indexJ = -1;
                    }



                }
                if (result >= count)
                {
                    return (result, i, indexJ);
                }
                else
                {
                    result = 0;
                }

            }
        }
        return (result, -1, -1);
    }

    public List<string> GetBoard()
    {
        var result = new List<string>();

        for (int i = 0; i < rows; i++)
        {
            string tmp = "";

            for (int j = 0; j < columns; j++)
            {
                if (hall[i, j] > 0)
                {
                    tmp += "X";
                }
                else
                {
                    tmp += "0";
                }

            }
            result.Add(tmp);

        }
        return result;

    }

    public void PrintHall()
    {
        if (hall == null)
        {
            throw new NullReferenceException("Hall is null");
        }

        Console.WriteLine("Current Situation:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(hall[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}