using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class TestServices : ITestServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SysUser Test()
        {
            var a = _unitOfWork.GetRepository<SysUser>();
            return a.GetFirstOrDefault();
        }

        public void TestAdd()
        {
            var a = _unitOfWork.GetRepository<SysUser>();

            var mode = new SysUser
            {
                Name = "1",
                NickName="2",
                Password="3"
            };

            a.Insert(mode);
            _unitOfWork.SaveChanges();
        }
    }
}
