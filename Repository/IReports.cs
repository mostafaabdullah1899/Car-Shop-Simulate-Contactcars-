using Grade_Project_.DTO;
using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public interface IReports
    {
        List<Report_DTO> GetAllReport();
        Reports GetById (int id);
        void Insert(PostreportDTO report);
        void Delete(int id);
    }
}
