using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Data;
        public class ApplicationContext : DbContext
        {
            public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {

            }
        }

