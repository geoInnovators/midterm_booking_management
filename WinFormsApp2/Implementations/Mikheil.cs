using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Implementations;

public class MikheilBookingManager : IBookingManager
{
    public string[,] cinemaMatrix;
    private int bookingCounter = 1;

    public void Init(int rowsCount, int columnsCount)
    {
        cinemaMatrix = new string[rowsCount, columnsCount];

        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                cinemaMatrix[i, j] = "O";
            }
        }
    }

    public Result CreateBooking(int numberOfSeats, BookingRequestType type)
    {
        if (numberOfSeats <= 0)
        {
            return new Result { IsSuccess = false, ErrorMessage = "Invalid number of seats." };
        }

        int emptySeatCount = 0;

        for (int i = 0; i < cinemaMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cinemaMatrix.GetLength(1); j++)
            {
                if (cinemaMatrix[i, j] != "0")
                {
                    emptySeatCount++;
                }
            }
        }

        if (emptySeatCount < numberOfSeats)
        {
            return new Result { IsSuccess = false, ErrorMessage = "No empty seats." };
        }

        if (type == BookingRequestType.Scatter)
        {
            int scatteredSeats = 0;

            for (int i = 0; i < cinemaMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cinemaMatrix.GetLength(1); j++)
                {
                    if (cinemaMatrix[i, j] == "O")
                    {
                        string bookingId = "B" + bookingCounter;

                        cinemaMatrix[i, j] = bookingId;
                        scatteredSeats++;

                        if (scatteredSeats == numberOfSeats)
                        {
                            return new Result { IsSuccess = true, Value = bookingId };
                        }
                    }
                }
            }

            return new Result { IsSuccess = false, ErrorMessage = "Not enough scattered seats available." };
        }

        int consecutiveSeats = 0;
        for (int i = 0; i < cinemaMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cinemaMatrix.GetLength(1); j++)
            {
                if (cinemaMatrix[i, j] == "O")
                {
                    consecutiveSeats++;
                    if (consecutiveSeats == numberOfSeats)
                    {
                        if (j - numberOfSeats + 1 >= 0)
                        {
                            string bookingId = "B" + bookingCounter;

                            for (int k = j - numberOfSeats + 1; k <= j; k++)
                            {
                                cinemaMatrix[i, k] = bookingId;
                            }

                            bookingCounter++;

                            return new Result { IsSuccess = true, Value = bookingId };
                        }
                    }
                }
                else
                {
                    consecutiveSeats = 0;
                }
            }
            consecutiveSeats = 0;
        }

        return new Result { IsSuccess = false, ErrorMessage = "Not enough consecutive seats available." };
    }

    public Result DeleteBooking(string bookingId)
    {
        var deletedSeats = 0;
        for (int i = 0; i < cinemaMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cinemaMatrix.GetLength(1); j++)
            {
                if (cinemaMatrix[i, j] == bookingId)
                {
                    cinemaMatrix[i, j] = "O";
                    deletedSeats += 1;

                }
            }
        }
        if (deletedSeats > 0)
        {
            return new Result { IsSuccess = true, Value = "Booking deleted successfully." };
        }

        return new Result { IsSuccess = false, ErrorMessage = "Booking not found." };
    }

    public List<string> GetBoard()
    {
        var res = new List<string>();
        for (int i = 0; i < cinemaMatrix.GetLength(0); i++)
        {
            var s = "";
            for (int j = 0; j < cinemaMatrix.GetLength(1); j++)
            {
                s += cinemaMatrix[i, j];
                Console.Write(cinemaMatrix[i, j] + " ");
            }
            res.Add(s);
            Console.WriteLine();
        }
        return res;
    }
}
