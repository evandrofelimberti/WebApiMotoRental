﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiMotoRental.Data;

#nullable disable

namespace WebApiMotoRental.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240506001511_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApiMotoRental.Model.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datanascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_pessoa");

                    b.ToTable("pessoa", (string)null);
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEmissao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dataemissao");

                    b.Property<byte>("ImagemDocumento")
                        .HasColumnType("smallint")
                        .HasColumnName("imagemdocumento");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("numero");

                    b.Property<int>("PessoaId")
                        .HasColumnType("integer")
                        .HasColumnName("pessoaid");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_pessoadocumento");

                    b.HasIndex("PessoaId")
                        .HasDatabaseName("ix_pessoadocumento_pessoaid");

                    b.HasIndex("Numero", "Tipo")
                        .IsUnique()
                        .HasDatabaseName("ix_pessoadocumento_numero_tipo");

                    b.ToTable("pessoadocumento", (string)null);
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumentoCNH", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datavencimento");

                    b.Property<int>("PessoaDocumentoId")
                        .HasColumnType("integer")
                        .HasColumnName("pessoadocumentoid");

                    b.Property<DateTime>("PrimeiraHabilitacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("primeirahabilitacao");

                    b.HasKey("Id")
                        .HasName("pk_pessoadocumentocnh");

                    b.HasIndex("PessoaDocumentoId")
                        .IsUnique()
                        .HasDatabaseName("ix_pessoadocumentocnh_pessoadocumentoid");

                    b.ToTable("pessoadocumentocnh", (string)null);
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumentoTipoCNH", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PessoaDocumentoCNHId")
                        .HasColumnType("integer")
                        .HasColumnName("pessoadocumentocnhid");

                    b.Property<int>("TipoCNH")
                        .HasColumnType("integer")
                        .HasColumnName("tipocnh");

                    b.HasKey("Id")
                        .HasName("pk_pessoadocumentotipocnh");

                    b.HasIndex("PessoaDocumentoCNHId")
                        .HasDatabaseName("ix_pessoadocumentotipocnh_pessoadocumentocnhid");

                    b.ToTable("pessoadocumentotipocnh", (string)null);
                });

            modelBuilder.Entity("WebApiMotoRental.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("tipousuario");

                    b.HasKey("Id")
                        .HasName("pk_usuario");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumento", b =>
                {
                    b.HasOne("WebApiMotoRental.Model.Pessoa", "Pessoa")
                        .WithMany("PessoaDocumentos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pessoadocumento_pessoa_pessoaid");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumentoCNH", b =>
                {
                    b.HasOne("WebApiMotoRental.Model.PessoaDocumento", "PessoaDocumento")
                        .WithOne("PessoaDocumentoCNH")
                        .HasForeignKey("WebApiMotoRental.Model.PessoaDocumentoCNH", "PessoaDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pessoadocumentocnh_pessoadocumento_pessoadocumentoid");

                    b.Navigation("PessoaDocumento");
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumentoTipoCNH", b =>
                {
                    b.HasOne("WebApiMotoRental.Model.PessoaDocumentoCNH", "PessoaDocumentoCNH")
                        .WithMany("PessoaDocumentoTipoCNH")
                        .HasForeignKey("PessoaDocumentoCNHId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pessoadocumentotipocnh_pessoadocumentocnh_pessoadocumentocn~");

                    b.Navigation("PessoaDocumentoCNH");
                });

            modelBuilder.Entity("WebApiMotoRental.Model.Pessoa", b =>
                {
                    b.Navigation("PessoaDocumentos");
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumento", b =>
                {
                    b.Navigation("PessoaDocumentoCNH")
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiMotoRental.Model.PessoaDocumentoCNH", b =>
                {
                    b.Navigation("PessoaDocumentoTipoCNH");
                });
#pragma warning restore 612, 618
        }
    }
}
