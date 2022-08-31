using Grade_Project_.DTO;
using Grade_Project_.Models;
using Microsoft.EntityFrameworkCore;

namespace Grade_Project_.Repository
{
    public class Report_Repository:IReports
    {
        private readonly Cars_Entity context;

        public Report_Repository(Cars_Entity context)
        {
            this.context= context;
        }



        public List<Report_DTO> GetAllReport()
        {
            var x = context.Reports.Include(x => x.Car).Select
                (e => new Report_DTO()
                {

                    Id = e.Id,
                    Report = e.Report,
                    ReportsCar_Id = e.ReportsCar_Id,
                    CarBrand = e.Car.Car_Brand.Brand_Name,
                    CarModel = e.Car.Car_Model.Model_Name,
                    CarAddress = e.Car.Car_Address,
                    User_FullName = context.Users.Where(u => u.Id == e.Car.User_Id).Select(un => un.Full_Name).FirstOrDefault(),
                    User_Email = context.Users.Where(u => u.Id == e.Car.User_Id).Select(un => un.Email).FirstOrDefault(),
                }).ToList();
            return x;
        }
        public Users getUser(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }
        public Reports GetById(int id)
        {
            return context.Reports.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(PostreportDTO report)
        {
            Reports reports = new Reports();
            reports.Report = report.Report;
            reports.ReportsCar_Id = report.ReportsCar_Id;

            //reports.Id = report.Id;
            context.Add(reports);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Reports reports=GetById(id);
            context.Remove(reports);
            context.SaveChanges();
        }


    }
}
