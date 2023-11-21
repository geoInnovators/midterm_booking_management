using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Implementations;

public class AnaDvaliBookingManager : IBookingManager
{
    private string[,] bookingsGrid;
    private Random random;

    public AnaDvaliBookingManager()
    {
        random = new Random();
    }

    public void Init(int rowsCount, int columnsCount)
    {
        bookingsGrid = new string[rowsCount, columnsCount];
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                bookingsGrid[i, j] = "";
            }
        }
    }

    public List<string> GetBoard()
    {
        int rows = bookingsGrid.GetLength(0);
        int cols = bookingsGrid.GetLength(1);
        char[,] board = new char[rows, cols];
        var res = new List<string>();
        for (int i = 0; i < rows; i++)
        {
            var s = "";
            for (int j = 0; j < cols; j++)
            {
                board[i, j] = bookingsGrid[i, j] == string.Empty ? 'O' : 'X';
                s += board[i, j];
            }
            res.Add(s);
        }

        return res;
    }


    private string GenerateRandomString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 6)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public Result CreateBooking(int k, BookingRequestType type)
    {
        string bookingNumber = GenerateRandomString();

        if (type == BookingRequestType.Gather)
        {
            bool found = false;
            int rows = bookingsGrid.GetLength(0);
            int cols = bookingsGrid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <= cols - k; j++)
                {
                    bool isAvailable = true;
                    for (int c = j; c < j + k; c++)
                    {
                        if (bookingsGrid[i, c] != string.Empty)
                        {
                            isAvailable = false;
                            break;
                        }
                    }
                    if (isAvailable)
                    {
                        for (int c = j; c < j + k; c++)
                        {
                            bookingsGrid[i, c] = bookingNumber;
                        }
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            if (!found)
            {
                return new Result
                {
                    IsSuccess = false,
                    ErrorMessage = "Could not find consecutive seats for the booking."
                };
            }
        }
        else if (type == BookingRequestType.Scatter)
        {
            int bookedCount = 0;
            int rows = bookingsGrid.GetLength(0);
            int cols = bookingsGrid.GetLength(1);

            while (bookedCount < k)
            {
                int randomRow = random.Next(0, rows);
                int randomCol = random.Next(0, cols);

                if (bookingsGrid[randomRow, randomCol] == string.Empty)
                {
                    bookingsGrid[randomRow, randomCol] = bookingNumber;
                    bookedCount++;
                }
            }
        }

        return new Result
        {
            IsSuccess = true,
            Value = bookingNumber
        };
    }


    public Result DeleteBooking(string bookingNumber)
    {
        int rows = bookingsGrid.GetLength(0);
        int cols = bookingsGrid.GetLength(1);
        bool bookingFound = false;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (bookingsGrid[i, j] == bookingNumber)
                {
                    bookingsGrid[i, j] = string.Empty;
                    bookingFound = true;
                }
            }
        }

        if (bookingFound)
        {
            return new Result
            {
                IsSuccess = true,
                Value = "Booking deleted successfully."
            };
        }
        else
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Booking not found."
            };
        }
    }
}