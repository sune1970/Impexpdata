using Impexpdata.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
namespace Impexpdata
{
    public class CsvProcessor : ICsvProcessor
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public void ImportDataFromCsv(string fileName)
        {
            string csvData = File.ReadAllText(fileName);

            DataTable dt = FillDataTable(csvData);
            PopulateTable(dt);
        }

        public void ExportDataToCsv(string fileName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                var command = new SqlCommand("SelectAllCustomers", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();

                StreamWriter sw = new StreamWriter(fileName);
                object[] output = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    output[i] = reader.GetName(i);
                }

                sw.WriteLine(string.Join(";", output));

                while (reader.Read())
                {
                    reader.GetValues(output);
                    sw.WriteLine(string.Join(";", output));
                }

                sw.Close();
                reader.Close();
                con.Close();
            }
        }

        private void PopulateTable(DataTable dt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.Customer";
                    con.Open();
                    try
                    {
                        sqlBulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {

                    }
                    con.Close();
                }
            }
        }

        private static DataTable FillDataTable(string csvData)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("CustomerId", typeof(int)),
            new DataColumn("Name", typeof(string)),
            new DataColumn("Notes",typeof(string)),
            new DataColumn("CodeId",typeof(int)),
            new DataColumn("ImportDate", typeof(string))});


            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(';'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                    dt.Rows[dt.Rows.Count - 1][dt.Columns.Count - 1] = DateTime.Now.ToString();
                }
            }

            return dt;
        }
    }
}
