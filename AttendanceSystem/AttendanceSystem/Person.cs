using System;

namespace AttendanceSystem
{
    abstract class Person
    {
        private string name = string.Empty;
        private int id;

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new Exception("⚠ Name cannot be empty.");
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                if (value > 0)
                    id = value;
                else
                    throw new Exception("⚠ ID must be positive.");
            }
        }

        public abstract void Display();
    }
}