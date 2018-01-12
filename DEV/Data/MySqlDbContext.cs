using Data.Entity;
using Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// http://blog.csdn.net/linux12a/article/details/77480111
    /// 生成数据库架构：Add-Migration MyFirstMigration
    /// 指定迁移：Update-Database –TargetMigration: 20180107124745_MyFirstMigration
    /// 更新数据库：Update-Database
    /// 自动迁移：https://www.cnblogs.com/stulzq/p/7729380.html
    /// Specified key was too long; max key length is 767 bytes：
    /// AspNetTimedJobs.table.Column((maxLength: 512),唯一键索引长度(512*3)超出utf8
    /// 解决：mapping中设置id长度
    /// https://www.cnblogs.com/littleatp/p/4612896.html
    /// </summary>
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //关联Mapping
            new UserMapping(modelBuilder.Entity<SysUser>());
            new CrawNewsMapping(modelBuilder.Entity<CrawlNews>());
        }
    }
}
