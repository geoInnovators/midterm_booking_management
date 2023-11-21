using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2;

public interface IBookingManager
{
    void Init(int rowsCount, int columnsCount);
    Result CreateBooking(int k, BookingRequestType type);
    Result DeleteBooking(string bookingNumber);

    List<string> GetBoard();
}

