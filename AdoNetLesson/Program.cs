using AdoNetLesson;
using Npgsql;
using System;
using System.Data;


namespace ConsoleApp88;


public class Program
{
    static void Main(string[] args)
    {
        const string CONNECTSTRING = "Server=127.0.0.1;Port=5432;Database=ulash;User Id=postgres;Password=sulton";

        // CreateTable(CONNECTSTRING);
        // InsertOne(CONNECTSTRING);
        // InsertAll(CONNECTSTRING);
        // GetAll(CONNECTSTRING);
        // GetByID(CONNECTSTRING);
        // Delete(CONNECTSTRING);
        // UpdateOne(CONNECTSTRING);
        // UpdateAll(CONNECTSTRING);
        // GetLike(CONNECTSTRING);
        //  AddColumn(CONNECTSTRING);
        // AddColumnDefault(CONNECTSTRING);
        // UpdateColumn(CONNECTSTRING);
        // UpdateTable(CONNECTSTRING);
        // TableTruncate(CONNECTSTRING)
        // JoinTable(CONNECTSTRING);
        // AddIndex(CONNECTSTRING);




    }

    static void CreateTable(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        connection.Open();

        string query = "create table darslar2(id serial,name varchar(255),age int)";


        NpgsqlCommand command = new NpgsqlCommand(query, connection);

        command.ExecuteNonQuery();





        connection.Close();
    }
    static void InsertOne(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        connection.Open();

        string insertQuery = @$"insert into darslar2(name , age) values('sulton',22)";

        NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

        command.ExecuteNonQuery();


        connection.Close();
    }
    static void InsertAll(string local)
    {

        NpgsqlConnection connection = new NpgsqlConnection(local) ;

        connection.Open();

        SkyDars1 table = new SkyDars1();
     

        IList<SkyDars1> datalar = new List<SkyDars1>()
        {
           new SkyDars1() { name="Benzema",age=22},
           new SkyDars1() { name="Sanjar",age=24},
           new SkyDars1() { name="Akmal",age=25},
        };

        
        string baseQuery = "insert into darslar2(name ,age) values";
        string query1 = string.Empty;
        for (int i = 0; i < datalar.Count; i++)
        {
            baseQuery += @$"('{datalar[i].name}',{datalar[i].age}),";

        }


        NpgsqlCommand command = new NpgsqlCommand(baseQuery.Substring(0, baseQuery.Length - 1), connection);

        command.ExecuteNonQuery();

        connection.Close();

    }


    static void GetAll(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        connection.Open();

        string query = "select * from darslar2";

        NpgsqlCommand command = new NpgsqlCommand(query, connection);

        NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(@$" {reader.GetInt32(0)},
                {reader.GetString(1)}");

        }
        connection.Close();


    }

    static void GetByID(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            Console.WriteLine("Employed id:  ");
            int id = Convert.ToInt32(Console.ReadLine());


            connection.Open();

            string query = "select * from darslar2 where id=@id";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id= " + reader["id"] + " name " + reader["name"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
      
    }

    static void UpdateOne(string local) {


        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            Console.WriteLine("Employed id:  ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Employed name:  ");
            string name =Console.ReadLine();


            connection.Open();

            string query = "update darslar2 set name=@name where id=@id";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);

            int rowAffect = command.ExecuteNonQuery();


            if(rowAffect > 0)
            {
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("Fail");
            }
          
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();

    }



    static void UpdateAll(string local)
    {


        NpgsqlConnection connection = new NpgsqlConnection(local);
       

        try
        {
            connection.Open();

            Console.WriteLine("Nechta malumotni update qilasiz: ");
            int son = Convert.ToInt32(Console.ReadLine());

            
            NpgsqlCommand command = null;

            for (int i = 0; i < son; i++)
            {
                Console.WriteLine("Employed id:  ");
                int  id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Employed name:  ");
                string  name = Console.ReadLine();


               

                string query = "update darslar2 set name=@name where id=@id";

                 command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);

                int rowAffect = command.ExecuteNonQuery();


                if (rowAffect > 0)
                {
                    Console.WriteLine("Updated");
                }
                else
                {
                    Console.WriteLine("Fail");
                }
            }

           



           

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();

    }



    static void GetLike(string local)
    {


        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();
            Console.WriteLine("Boshidagi harfni kiriting:  ");
            string name = Console.ReadLine()+"%";


            

            string query = "select * from darslar2 where name like @name";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", name);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id= " + reader["id"] + " name " + reader["name"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();     



    }

    static void AddColumn(string local)
    {


        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string insertQuery = "ALTER TABLE darslar2 ADD COLUMN student_kurs2 INT";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Column added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add column.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }



    }


    static void AddColumnDefault(string local)
    {


        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string insertQuery = "ALTER TABLE darslar2 ADD COLUMN student_kurs3 INT DEFAULT 0";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Column added successfully.");
            }
            else
            {
                Console.WriteLine("success");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }



    }

    static void UpdateColumn(string local)
    {



        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string insertQuery = "ALTER TABLE darslar2 RENAME COLUMN student_kurs3 TO students_three";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Column added successfully.");
            }
            else
            {
                Console.WriteLine("success");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }



    }



    static void UpdateTable(string local)
    {



        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string insertQuery = "ALTER TABLE darslar2 RENAME TO lesson2";

            NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Column added successfully.");
            }
            else
            {
                Console.WriteLine("success");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }



    }


    static void TableTruncate(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string tableName = "lesson2"; 

            string truncateQuery = $"TRUNCATE TABLE {tableName}";

            NpgsqlCommand command = new NpgsqlCommand(truncateQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected >= 0)
            {
                Console.WriteLine($"Truncated {rowsAffected} rows from the table.");
            }
            else
            {
                Console.WriteLine("Failed to truncate the table.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

    }

    static void JoinTable(string local)
    {
        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string query = @"
        SELECT lesson1.name, darslar.mavzu
        FROM lesson1
        JOIN darslar ON lesson1.id = darslar.id;
    ";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Qator ma'lumotlarini olish
                Console.WriteLine($"lesson1 name: {reader["name"]}, darslar mavzu: {reader["mavzu"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xatolik: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

    }


    static void AddIndex(string local)
    {

        NpgsqlConnection connection = new NpgsqlConnection(local);

        try
        {
            connection.Open();

            string indexName = "index_sulton";
            string tableName = "lesson1";
            string columnName = "name";

            string createIndexQuery = $"CREATE INDEX {indexName} ON {tableName}({columnName})";

            NpgsqlCommand command = new NpgsqlCommand(createIndexQuery, connection);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected >= 0)
            {
                Console.WriteLine($"Index {indexName} created successfully on {tableName}.{columnName}");
            }
            else
            {
                Console.WriteLine($"Failed to create index {indexName} on {tableName}.{columnName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }


    }


}