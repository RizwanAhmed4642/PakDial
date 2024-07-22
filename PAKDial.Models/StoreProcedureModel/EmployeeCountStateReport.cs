namespace PAKDial.Domains.StoreProcedureModel
{
    public class EmployeeCountStateReport
    {
        public string TableName { get; set; }
        public int TotalCount { get; set; }
        public int YearlyCount { get; set; }
        public int MonthyCount { get; set; }
        public int WeeklyCount { get; set; }
        public int DailyCount { get; set; }

    }
}
