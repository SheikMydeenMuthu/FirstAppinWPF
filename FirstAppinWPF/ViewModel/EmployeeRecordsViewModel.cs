using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FirstAppinWPF.Model;
using Newtonsoft.Json;


namespace FirstAppinWPF.ViewModel
{
    public class EmployeeRecordsViewModel:BaseViewModel
    {
        private string employeeName;
        


        public string EmployeeName
        {
            get { return employeeName; }
            set 
            {
                employeeName = value; 
                OnPropertyChanged("EmployeeName");
            }
        }

        public EmployeeRecordsViewModel()
        {
            BindEmployeeRecords();
        }
        private ObservableCollection<EmployeeDetails> employeeCollection;

        public ObservableCollection<EmployeeDetails> EmployeeCollection
        {
            get { return employeeCollection; }
            set { employeeCollection = value; OnPropertyChanged("EmployeeCollection"); }
        }

        private async void BindEmployeeRecords()
        {
            EmployeeCollection = new ObservableCollection<EmployeeDetails>();
            using (HttpClient client = new HttpClient())
            {
                var result = client.GetAsync("http://dummy.restapiexample.com/api/v1/employees").Result;
                var content = await result.Content.ReadAsStringAsync();
                var employeeResults = JsonConvert.DeserializeObject<EmployeeDetailsModel>(content);
                List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
                employeeDetails = employeeResults.data;
                foreach(var emp in employeeDetails)
                EmployeeCollection.Add(emp);
            }
        }
    }
}
