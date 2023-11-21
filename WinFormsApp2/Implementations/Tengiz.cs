using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Implementations;

public class TengizBookingManager : IBookingManager
{
    private int[,] space;
    private int rows, columns;
    private int uniqueNumber = 0;

    public void Init(int rowsCount, int columnsCount)
    {
        space = new int[rowsCount, columnsCount];
        rows = rowsCount;
        columns = columnsCount;
    }
    public Result CreateBooking(int k, BookingRequestType type)
    {
        if (k > columns && type == BookingRequestType.Gather)
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Not Enough Space"
            };

        int spaceToAssign = k;
        int[,] tempSpace = new int[space.GetLength(0), space.GetLength(1)];
        uniqueNumber++;

        CopyArray(space, tempSpace);

        for (int i = 0; i < tempSpace.GetLength(0); i++)
        {
            if (type == BookingRequestType.Gather)
            {
                spaceToAssign = k;
                CopyArray(space, tempSpace);
            }
            for (int j = 0; j < tempSpace.GetLength(1); j++)
            {
                if (tempSpace[i, j] == 0)
                {
                    spaceToAssign--;
                    tempSpace[i, j] = uniqueNumber;
                    if (spaceToAssign == 0)
                    {
                        break;
                    }
                }
                else if (type == BookingRequestType.Gather && space[i, j] != 0)
                {
                    spaceToAssign = k;
                    CopyArray(space, tempSpace);
                }
            }

            if (spaceToAssign == 0)
            {
                CopyArray(tempSpace, space);

                return new Result
                {
                    IsSuccess = true,
                    Value = uniqueNumber.ToString()
                };
            }
        }

        return new Result
        {
            IsSuccess = false,
            ErrorMessage = "Not Enough Space"
        };
    }

    public List<string> GetBoard()
    {
        var result = new List<string>();

        var row = new StringBuilder();

        for (int i = 0; i < space.GetLength(0); i++)
        {
            for (int j = 0; j < space.GetLength(1); j++)
            {
                row.Append(space[i, j] == 0 ? "O" : "X");
            }
            result.Add(row.ToString());
            row.Clear();
        }
        return result;
    }

    public Result DeleteBooking(string bookingNumber)
    {
        try
        {
            var bookingNumberInt = Convert.ToInt32(bookingNumber);

            var found = false;

            for (int i = 0; i < space.GetLength(0); i++)
            {
                for (int j = 0; j < space.GetLength(1); j++)
                {
                    if (space[i, j] == bookingNumberInt)
                    {
                        found = true;
                        space[i, j] = 0;
                    }
                }
            }

            if (!found)
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = $"Booking {bookingNumber} not found"
                };

            return new Result
            {
                IsSuccess = true,
                Value = $"Booking {bookingNumber} canceled"
            };
        }
        catch (Exception)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Not Enough Space"
            };
        }
    }

    private void CopyArray(int[,] fromArr, int[,] toArr)
    {
        for (int i = 0; i < fromArr.GetLength(0); i++)
            for (int j = 0; j < fromArr.GetLength(1); j++)
                toArr[i, j] = fromArr[i, j];
    }
}