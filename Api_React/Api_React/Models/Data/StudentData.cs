using Api_React.Models.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Api_React.Models.Data
{
    public class StudentData
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentData(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public StudentData()
        { }

        public List<Student> GetStudent()
        {
            List<Student> listStudent = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetStudent", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student()
                    {
                        StudentId = Convert.ToInt32(reader["StudenId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Email = Convert.ToString(reader["Email"]),
                        Password = Convert.ToString(reader["Password"]),
                    };
                    listStudent.Add(student);
                }
                connection.Close();
                return listStudent;
            }
        }

        public Student GetStudentById(int studentId) {
            Student listStudent = new Student();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetStudentById", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentId", studentId);


                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listStudent = new Student(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3));
                   
                }
                connection.Close();
                return listStudent;
            }

            }

        public int Insert(Student student)
        {
            int resultToReturn;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertStudent", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Password", student.Password);
                //  com.ExecuteNonQuery();
                resultToReturn = command.ExecuteNonQuery();
                connection.Close();

            }
            return resultToReturn;
        }

        public int Update(int id,Student student)
        {
            int resultToReturn;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateStudent", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentId", id);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Password", student.Password);
                resultToReturn = command.ExecuteNonQuery();
                connection.Close();

            }
            return resultToReturn;
        }
        public int Delete(int studentId)
        {
            int resultToReturn;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteStudent", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentId", studentId);
               
                resultToReturn = command.ExecuteNonQuery();
                connection.Close();

            }
            return resultToReturn;
        }

    }
}
