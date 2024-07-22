function CreateUpdateEmployee() {
    this.EmployeeId = '';
    this.FirstName = '';
    this.LastName = '';
    this.DateOfBirth = '';
    this.Cnic = '';
    this.PassportNo = '';
    this.DesignationId = '';
    this.EmpAddress = '';
    this.CountryId = '';
    this.ProvinceId = '';
    this.CityId = '';
    this.CityAreaId = '';
    this.ContactNo = '';
    this.PhoneNo = '';
    this.UserId = '';
    this.Email = '';
    this.Password = '';
    this.RoleId = '';
    this.IsActive = '';
}

function AssignedEmpAreas() {
    this.Id = '';
    this.CityId = '';
    this.ZoneId = '';
    this.EmployeeId = '';
}

function AssignedEmpCategory() {
    this.Id = '';
    this.CategoryId = '';
    this.EmployeeId = '';
}

function VMAddUpdateRegionalManager() {
    this.EmployeeId = '';
    this.ManagerId = '';
    this.StateId = '';
    this.AssignedCityList = '';
}
function VMAddUpdateCategoryManager() {
    this.EmployeeId = '';
    this.ManagerId = '';
    this.AssignedCategoryList = '';
}

function VMAddUpdateZoneManager() {
    this.EmployeeId = '';
    this.ManagerId = '';
    this.CityId = '';
    this.AssignedZoneList = '';
}
