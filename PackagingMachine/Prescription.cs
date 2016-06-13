using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagingMachine
{
    class Prescription
    {
        public int Autoid{set;get;}

        public string Id{set;get;}

        public string Registerid{set;get;}

        public string Name { set; get; }
                
        public string Sex{set;get;}

        public decimal Age{set;get;}

        public string Tele{set;get;}

        public string Email{set;get;}

        public string DepartmentName{set;get;}

        public string DoctorName{set;get;}

        public string PrescriptionName{set;get;}

        public DateTime PrescribeTime{set;get;}
        public string Creatorname{set;get;}

        public DateTime Creationtime{set;get;}

        public string Valuesn{set;get;}

        public string Valuername{set;get;}

        public DateTime Valuationtime{set;get;}

        public decimal Price{set;get;}

        public decimal Quantity{set;get;}

        public decimal Quantityday{set;get;}

        public decimal Pricetotal{set;get;}

        public string Paymenttype{set;get;}

        public string Paymentstatus{set;get;}

        public string Datasource{set;get;}

        public string Deviceid{set;get;}

        public string Processstatus{set;get;}

        public string Description{set;get;}


    }
}
