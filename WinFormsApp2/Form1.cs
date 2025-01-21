using System.Windows.Forms;


namespace WinFormsApp2
{
    public partial class Form1 : Form
    {

        private List<string> _orders  = new List<string>();

        private readonly IBookingManager _bookingManager;
        public Form1()
        {
            InitializeComponent();
            _bookingManager = new FakeBookingManager();
        }

        private void DrawBoard(List<string> board)
        {
            var rows = board.Count;
            var columns = board[0].Length;

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            // Get the number of rows and columns

            // Set the number of rows and columns in the TableLayoutPanel
            tableLayoutPanel1.ColumnCount = columns;
            tableLayoutPanel1.RowCount = rows;

            // Adjust the size of each row and column to fit evenly
            for (int i = 0; i < columns; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));
            }

            for (int i = 0; i < rows; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            }

            // Add controls to the grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Create a panel for border effect
                    var panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = board[i][j] == 'X' ? Color.Red : Color.White
                    };


                    // Add panel to tableLayoutPanel
                    tableLayoutPanel1.Controls.Add(panel, j, i);
                }
            }
        }

        private void Init_Click(object sender, EventArgs e)
        {
            int rows = int.Parse(textBox1.Text);
            int columns = int.Parse(textBox2.Text);

            _bookingManager.Init(rows, columns);

            var board = _bookingManager.GetBoard();
            listBox1.DataSource = _orders;
            DrawBoard(board);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var k = int.Parse(textBox3.Text);
            CreateBooking(k, BookingRequestType.Scatter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var k = int.Parse(textBox3.Text);
            CreateBooking(k, BookingRequestType.Gather);
        }

        private void CreateBooking(int k, BookingRequestType type)
        {
            var res = _bookingManager.CreateBooking(k, type);
            if (!res.IsSuccess)
                MessageBox.Show(res.ErrorMessage);
            else
            {
                var board = _bookingManager.GetBoard();
                DrawBoard(board);
                MessageBox.Show("youe new number is : " + res.Value);
                AddOrder(res.Value);
            }
        }

        private void AddOrder(string order)
        {
            _orders.Add(order);
            listBox1.DataSource = null;
            listBox1.DataSource = _orders;
        }

        private void RemoveOrder(string order)
        {
            _orders.Remove(order);
            listBox1.DataSource = null;
            listBox1.DataSource = _orders;
        }

        private void Cancelorder_Click(object sender, EventArgs e)
        {
            string selectedOrder = listBox1.SelectedItem.ToString();
            var res = _bookingManager.DeleteBooking(selectedOrder);
            if (!res.IsSuccess)
                MessageBox.Show(res.ErrorMessage);
            else
            {
                var board = _bookingManager.GetBoard();
                DrawBoard(board);
                RemoveOrder(selectedOrder);
            }
        }
    }
}