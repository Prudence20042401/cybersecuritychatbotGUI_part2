using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CyberSecurityChatbotGUI.Models;

namespace CyberSecurityChatbotGUI
{
    public class DatabaseService
    {
        string connectionString =
        "server=localhost;database=cyberbotdb;uid=root;Pwd=Prudence@24;";


        // ---------------- USERS ----------------

        public UserProfile GetUser(string userName)
        {
            using (MySqlConnection conn =
                new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                "SELECT * FROM Users WHERE Name=@name";

                MySqlCommand cmd =
                new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                "@name",
                userName);

                var reader =
                cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new UserProfile
                    {
                        Id =
                        Convert.ToInt32(
                        reader["Id"]),

                        Name =
                        reader["Name"]
                        .ToString(),

                        LastFeeling =
                        reader["LastFeeling"]
                        .ToString(),

                        LastVisit =
                        Convert.ToDateTime(
                        reader["LastVisit"])
                    };
                }
            }

            return null;
        }


        public void SaveUser(
        string name,
        string feeling)
        {
            using (MySqlConnection conn =
            new MySqlConnection(connectionString))
            {
                conn.Open();

                UserProfile user =
                GetUser(name);

                if (user == null)
                {
                    string query =
                    @"INSERT INTO Users
                    (Name,LastFeeling,LastVisit)
                    VALUES
                    (@name,@feeling,@date)";

                    MySqlCommand cmd =
                    new MySqlCommand(
                    query,
                    conn);

                    cmd.Parameters.AddWithValue(
                    "@name",
                    name);

                    cmd.Parameters.AddWithValue(
                    "@feeling",
                    feeling);

                    cmd.Parameters.AddWithValue(
                    "@date",
                    DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                else
                {
                    string query =
                    @"UPDATE Users
                    SET LastFeeling=@feeling,
                    LastVisit=@date
                    WHERE Name=@name";

                    MySqlCommand cmd =
                    new MySqlCommand(
                    query,
                    conn);

                    cmd.Parameters.AddWithValue(
                    "@name",
                    name);

                    cmd.Parameters.AddWithValue(
                    "@feeling",
                    feeling);

                    cmd.Parameters.AddWithValue(
                    "@date",
                    DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // ---------------- TASKS ----------------

        public List<TaskItem> GetTasks()
        {
            List<TaskItem> tasks =
            new List<TaskItem>();

            using (MySqlConnection conn =
            new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                "SELECT * FROM Tasks";

                MySqlCommand cmd =
                new MySqlCommand(query, conn);

                var reader =
                cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(
                    new TaskItem
                    {
                        Id =
                        Convert.ToInt32(
                        reader["Id"]),

                        Title =
                        reader["Title"]
                        .ToString(),

                        Description =
                        reader["Description"]
                        .ToString(),

                        ReminderDate =
                        reader["ReminderDate"]
                        as DateTime?,

                        Completed =
                        Convert.ToBoolean(
                        reader["Completed"])
                    });
                }
            }

            return tasks;
        }


        public void AddTask(
        TaskItem task)
        {
            using (MySqlConnection conn =
            new MySqlConnection(connectionString))
            {
                conn.Open();

                string query =
                @"INSERT INTO Tasks
                (Title,Description,
                ReminderDate,
                Completed)

                VALUES
                (@title,@description,
                @date,@completed)";

                MySqlCommand cmd =
                new MySqlCommand(
                query,
                conn);

                cmd.Parameters.AddWithValue(
                "@title",
                task.Title);

                cmd.Parameters.AddWithValue(
                "@description",
                task.Description);

                cmd.Parameters.AddWithValue(
                "@date",
                task.ReminderDate);

                cmd.Parameters.AddWithValue(
                "@completed",
                task.Completed);

                cmd.ExecuteNonQuery();
            }
        }
    }
}