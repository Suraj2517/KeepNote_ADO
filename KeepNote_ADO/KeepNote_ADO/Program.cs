using System.Data.SqlClient;
using System.Data;

namespace KeepNote_ADO
{
    class Note
    {
        public static void CreateNote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            Console.WriteLine("Enter Id");
            int id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string descriptions = Console.ReadLine();

            DateTime date = DateTime.Now;

            var row = ds.Tables[0].NewRow();
            row["Id"] = id;
            row["Title"] = title;
            row["Descriptions"] = descriptions;
            row["Dates"] = date;

            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database saved successfully");
        }

        public static void NoteById(SqlConnection con)
        {
            Console.WriteLine("Enter Id you want to see details");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Note where Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "NoteTable");
            for (int i = 0; i < ds.Tables["NoteTable"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["NoteTable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["NoteTable"].Rows[i][j]} || ");
                }
                Console.WriteLine();
            }
        }
        public static void ViewAll(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "NoteTable");
            for (int i = 0; i < ds.Tables["NoteTable"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["NoteTable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["NoteTable"].Rows[i][j]} || ");
                }
                Console.WriteLine();
            }
        }
        public static void UpdateNote(SqlConnection con)
        {
            Console.WriteLine($"Enter Id you want to update: ");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Note where Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
         
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string descriptions = Console.ReadLine();

            DateTime date = DateTime.Now;

            ds.Tables[0].Rows[0]["title"] = title;
            ds.Tables[0].Rows[0]["descriptions"] = descriptions;
            ds.Tables[0].Rows[0]["dates"] = date;

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database Updated successfully");
        }
        public static void DeleteNote(SqlConnection con)
        {
            Console.WriteLine("Enter Id you want to delete");
            int Id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Note where Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Entry deleted successfully");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string ans = "";
            do
            {

                SqlConnection con = new SqlConnection("Server= US-5HSQ8S3; database=NoteDB; Integrated Security=true");
                Console.WriteLine("Welcome to Take Note App");
                Console.WriteLine("1. Create Note");
                Console.WriteLine("2. View Note By Id");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Note By Id");
                Console.WriteLine("5. Delete Note By Id");
                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Note.CreateNote(con);
                            break;
                        }
                    case 2:
                        {
                            Note.NoteById(con);
                            break;
                        }
                    case 3:
                        {
                            Note.ViewAll(con);
                            break;
                        }
                    case 4:
                        {
                            Note.UpdateNote(con);
                            break;
                        }
                    case 5:
                        {
                            Note.DeleteNote(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong choice entered");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue? [y/n] ");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y") ;
        }
    }
}