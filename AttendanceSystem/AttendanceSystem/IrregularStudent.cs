using System;

namespace AttendanceSystem
{
    class IrregularStudent : Person
    {
        public bool? IsPresent { get; set; }
        public DateTime AttendanceDate { get; set; }

        public override void Display()
        {
            string status = IsPresent == null ? "Pending" : (IsPresent.Value ? "Present" : "Absent");
            Console.WriteLine($"{ID,-5} | {Name,-20} | Irregular      | {status,-7} | {AttendanceDate:yyyy-MM-dd}");
        }
    }
}