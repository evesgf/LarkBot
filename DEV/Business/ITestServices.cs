using Data.Entity;

namespace Business
{
    public interface ITestServices : IDependencyRegister
    {
        SysUser Test();

        void TestAdd();
    }
}
