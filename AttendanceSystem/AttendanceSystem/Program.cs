using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AttendanceSystem
{
    class Program
    {
        static List<RegularStudent> regularStudents = new List<RegularStudent>();
        static List<IrregularStudent> irregularStudents = new List<IrregularStudent>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n==== Attendance System ====");
                Console.WriteLine("1. Add Regular Student");
                Console.WriteLine("2. Add Irregular Student");
                Console.WriteLine("3. Mark Attendance");
                Console.WriteLine("4. View All");
                Console.WriteLine("5. Exit");

                int choice = GetMenuChoice(1, 5);

                switch (choice)
                {
                    case 1: AddRegularStudent(); break;
                    case 2: AddIrregularStudent(); break;
                    case 3: MarkAttendance(); break;
                    case 4: ViewAll(); break;
                    case 5: return;
                }
            }
        }

        // ================= ADD =================
        static void AddRegularStudent()
        {
            try
            {
                RegularStudent s = new RegularStudent();
                s.ID = GetUniqueID(GetAllIDs());
                s.Name = GetValidName();
                s.IsPresent = null;

                regularStudents.Add(s);
                Console.WriteLine("✅ Regular student added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AddIrregularStudent()
        {
            try
            {
                IrregularStudent s = new IrregularStudent();
                s.ID = GetUniqueID(GetAllIDs());
                s.Name = GetValidName();
                s.IsPresent = null;

                irregularStudents.Add(s);
                Console.WriteLine("✅ Irregular student added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ================= ATTENDANCE =================
        static void MarkAttendance()
        {
            Console.WriteLine("\n1. Regular Student");
            Console.WriteLine("2. Irregular Student");

            int type = GetMenuChoice(1, 2);

            if (type == 1 && regularStudents.Count == 0)
            {
                Console.WriteLine("⚠ No regular students.");
                return;
            }
            if (type == 2 && irregularStudents.Count == 0)
            {
                Console.WriteLine("⚠ No irregular students.");
                return;
            }

            Person? found = null;

            while (true)
            {
                int id = GetValidInt("Enter ID: ");

                found = (type == 1)
                    ? regularStudents.Find(s => s.ID == id)
                    : irregularStudents.Find(s => s.ID == id);

                if (found != null) break;

                Console.WriteLine("❌ ID not found.");
            }

            DateTime date;
            while (true)
            {
                Console.Write("Enter date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine() ?? "", out date)) break;
                Console.WriteLine("❌ Invalid date.");
            }

            Console.WriteLine("1. Present");
            Console.WriteLine("2. Absent");

            int status = GetMenuChoice(1, 2);

            if (found is RegularStudent r)
            {
                r.IsPresent = (status == 1);
                r.AttendanceDate = date;
            }
            else if (found is IrregularStudent ir)
            {
                ir.IsPresent = (status == 1);
                ir.AttendanceDate = date;
            }

            Console.WriteLine("✅ Attendance updated!");
        }

        // ================= DISPLAY =================
        static void ViewAll()
        {
            Console.WriteLine("\nID | Name                 | Type       | Status  | Date");
            Console.WriteLine("----------------------------------------------------------");

            foreach (var s in regularStudents) s.Display();
            foreach (var s in irregularStudents) s.Display();
        }

        // ================= VALIDATION =================
        static int GetMenuChoice(int min, int max)
        {
            while (true)
            {
                int choice = GetValidInt($"Enter choice ({min}-{max}): ");
                if (choice >= min && choice <= max)
                    return choice;

                Console.WriteLine("❌ Invalid choice.");
            }
        }

        static int GetUniqueID(List<int> existingIDs)
        {
            while (true)
            {
                int id = GetValidInt("Enter ID: ");
                if (!existingIDs.Contains(id)) return id;

                Console.WriteLine("❌ ID already exists!");
            }
        }

        static List<int> GetAllIDs()
        {
            List<int> ids = new List<int>();
            ids.AddRange(regularStudents.ConvertAll(s => s.ID));
            ids.AddRange(irregularStudents.ConvertAll(s => s.ID));
            return ids;
        }

        static string GetValidName()
        {
            while (true)
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(name) &&
                    Regex.IsMatch(name, "^[a-zA-Z ]+$"))
                    return name;

                Console.WriteLine("❌ Invalid name.");
            }
        }

        static int GetValidInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine() ?? "", out int value))
                    return value;

                Console.WriteLine("❌ Numbers only.");
            }
        }
    }
}