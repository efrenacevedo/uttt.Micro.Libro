﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using uttt.Micro.Libro.Percistence;

#nullable disable

namespace uttt.Micro.Libro.Migrations
{
    [DbContext(typeof(ContextoLibreria))]
    partial class ContextoLibreriaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("uttt.Micro.Libro.Models.LibreriaMaterial", b =>
                {
                    b.Property<Guid>("LibreriaMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AutorLibro")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("FechaPublicacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NewData")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LibreriaMaterialId");

                    b.ToTable("LibreriaMateriales");
                });
#pragma warning restore 612, 618
        }
    }
}
