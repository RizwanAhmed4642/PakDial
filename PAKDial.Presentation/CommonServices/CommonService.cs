using PAKDial.Interfaces.PakDialServices;


namespace PAKDial.Presentation.CommonServices
{
    public class CommonService 
    {
        private readonly IEmployeeService _employeeService;

        public CommonService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public string GetEmployeeName(string UserId)
        {
            var Employee = _employeeService.FindByUserId(UserId);
            return Employee.FirstName +" "+ Employee.LastName;
        }
        
    }
}
