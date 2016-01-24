using BaoViet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.SampleData
{
    public class SampleData_Home_Page : Home_Page_ViewModel
    {
        public Home_Page_ViewModel ViewModel { get; set; }
        public SampleData_Home_Page() : base()
        {
            ViewModel = new Home_Page_ViewModel();
            ViewModel.IsPaneOpen = true;
        }
    }


    //public class DesignTimeCustomerViewModel : CustomerViewModel
    //{

    //    public DesignTimeCustomerViewModel()
    //      : base()
    //    {

    //        base.Customer = new Customer()
    //        {
    //            FirstName = "Alfons",
    //            LastName = "Parovszky",
    //            Photo = "../Images/image01.png"
    //        };
    //        base.Customer.CalculateRank();
    //        base.Customer.Addresses.Add(
    //            new Address() { LineOne = "125 North St", City = "Greatville", State = "CA", Zip = "98004" });
    //        base.Customer.Addresses.Add(
    //            new Address() { LineOne = "3000 1st St NE", City = "Coolville", State = "CA", Zip = "98004" });
    //    }
    //}

}
