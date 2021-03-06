﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wpf_program__module_sec;

namespace Wpf_program__module_sec.Migrations
{
    [DbContext(typeof(bd_table))]
    [Migration("20180206153732_DB_Sec")]
    partial class DB_Sec
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Wpf_program__module_sec.Class_table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("index");

                    b.Property<string>("xy");

                    b.HasKey("Id");

                    b.ToTable("table");
                });
        }
    }
}
