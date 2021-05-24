using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class MyDbContenxt : DbContext
    {
        public MyDbContenxt(DbContextOptions<MyDbContenxt> options): base(options)
        {

        }
        public DbSet<StudentModel> Student { get; set; }
    }
}
