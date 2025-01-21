using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2;


public class FakeBookingManager : IBookingManager
{
    int r;
    int c;

    public void Init(int rowsCount, int columnsCount)
    {
        r = rowsCount;
        c = columnsCount;
    }
    public Result CreateBooking(int k, BookingRequestType type)
    {
        throw new NotImplementedException();
    }
    public Result DeleteBooking(string bookingNumber)
    {
        
        throw new NotImplementedException();
    }

    public List<string> GetBoard()
    {
        var res = new List<string>();
        for(int i=0;i < r; i++)
            res.Add(new string('O', c));
        return res;
    }
}
