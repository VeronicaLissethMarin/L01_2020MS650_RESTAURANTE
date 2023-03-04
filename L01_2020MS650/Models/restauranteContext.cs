﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020MS650.Models
{
    public class restauranteContext:DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {
            
        }

        public DbSet<clientes> clientes { get; set; }

        public DbSet<motorista> motorista { get; set; }

        public DbSet<pedidos> pedidos { get; set; }

        public DbSet<platos> platos { get; set; }
    }
}
