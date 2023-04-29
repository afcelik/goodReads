using System.Net;

namespace goodReads
{
    public partial class Form1 : Form
    {
        List<Book> books = new List<Book>();
        bool orderStatus = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            WebClient wc = new WebClient();
            string bbcData = wc.DownloadString("https://www.goodreads.com/list/show/18834.BBC_Top_200_Books");

            int contentStartIndex = bbcData.IndexOf("<table class=\"tableList js-dataTooltip\">");
            int contentEndIndex = bbcData.IndexOf("previous_page disabled");
            string content = bbcData.Substring(contentStartIndex, contentEndIndex - contentStartIndex);

            List<string> list = new List<string>();

            while (content.Contains("<tr itemscope"))
            {
                int trStartIndex = content.IndexOf("<tr itemscope");
                int trEndIndex = content.IndexOf("</tr>");
                string bookContent = content.Substring(trStartIndex, trEndIndex - trStartIndex);
                list.Add(bookContent);
                content = content.Substring(trEndIndex + 1);
            }

            foreach (string str in list)
            {
                Book book = new Book();

                int nameIndex = str.IndexOf("aria-level=") + 15;
                int nameEndIndex = str.IndexOf("</span>", nameIndex);
                book.Name = str.Substring(nameIndex, nameEndIndex - nameIndex);

                books.Add(book);
            }

            dataGridView1.DataSource = books;

            string search = txtSearch.Text.ToLower();

            List<Book> searchBooks = new List<Book>();
            foreach (var item in books)
            {
                if (item.Name.ToLower().IndexOf(search) != -1)
                {
                    searchBooks.Add(item);
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = searchBooks;

            lblCount.Text = searchBooks.Count.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();

            List<Book> searchBooks = new List<Book>();
            foreach (var item in books)
            {
                if (item.Name.ToLower().IndexOf(search) != -1)
                {
                    searchBooks.Add(item);
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = searchBooks;

            lblCount.Text = searchBooks.Count.ToString();

        }
    }
}
