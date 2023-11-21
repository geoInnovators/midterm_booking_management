using System;
using WinFormsApp2;

namespace BokkingService;



public class AniBookingManager : IBookingManager
{
    private bool[,] seats;
    private Dictionary<string, List<(int, int)>> bookings;

    public void Init(int rowsCount, int columnsCount)
    {
        seats = new bool[rowsCount, columnsCount];
        for (int i = 0; i < rowsCount; i++)
            for (int j = 0; j < columnsCount; j++)
                seats[i, j] = true;

        bookings = new Dictionary<string, List<(int, int)>>();
    }

    public Result CreateBooking(int k, BookingRequestType type)
    {
        string bookingNumber = Guid.NewGuid().ToString();

        if (type == BookingRequestType.Gather)
        {
            for (int row = 0; row < seats.GetLength(0); row++)
            {
                int consecutiveSeats = 0;
                for (int col = 0; col < seats.GetLength(1); col++)
                {
                    if (seats[row, col])
                    {
                        consecutiveSeats++;
                        if (consecutiveSeats == k)
                        {
                            List<(int, int)> bookedSeats = new List<(int, int)>();
                            for (int bookCol = col - k + 1; bookCol <= col; bookCol++)
                            {
                                seats[row, bookCol] = false;
                                bookedSeats.Add((row, bookCol));
                            }
                            bookings.Add(bookingNumber, bookedSeats);
                            return new Result { IsSuccess = true, Value = bookingNumber };
                        }
                    }
                    else
                    {
                        consecutiveSeats = 0;
                    }
                }
            }
        }
        else if (type == BookingRequestType.Scatter)
        {
            List<(int, int)> bookedSeats = new List<(int, int)>();
            for (int row = 0; row < seats.GetLength(0); row++)
            {
                for (int col = 0; col < seats.GetLength(1); col++)
                {
                    if (seats[row, col])
                    {
                        bookedSeats.Add((row, col));
                        if (bookedSeats.Count == k)
                        {
                            foreach (var seat in bookedSeats)
                            {
                                seats[seat.Item1, seat.Item2] = false;
                            }
                            bookings.Add(bookingNumber, bookedSeats);
                            return new Result { IsSuccess = true, Value = bookingNumber };
                        }
                    }
                }
            }
        }

        return new Result { IsSuccess = false, ErrorMessage = "No seats available" };
    }

    public Result DeleteBooking(string bookingNumber)
    {
        if (string.IsNullOrEmpty(bookingNumber))
        {
            return new Result { IsSuccess = false, ErrorMessage = "Invalid booking number" };
        }

        if (bookings.ContainsKey(bookingNumber))
        {
            foreach (var seat in bookings[bookingNumber])
            {
                seats[seat.Item1, seat.Item2] = true;
            }
            bookings.Remove(bookingNumber);
            return new Result { IsSuccess = true };
        }
        else
        {
            return new Result { IsSuccess = false, ErrorMessage = "Booking number not found" };
        }
    }

    public List<string> GetBoard()
    {
        List<string> board = new List<string>();
        for (int i = 0; i < seats.GetLength(0); i++)
        {
            string row = "";
            for (int j = 0; j < seats.GetLength(1); j++)
            {
                row += seats[i, j] ? "O" : "X";
            }
            board.Add(row);
        }
        return board;
    }
}

