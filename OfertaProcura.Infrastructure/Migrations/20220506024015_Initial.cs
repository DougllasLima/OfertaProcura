using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfertaProcura.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portifolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Data_Atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portifolio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome_Profissao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagemPortifolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Portifolio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caminho_Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagemPortifolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagemPortifolio_Portifolio_Id_Portifolio",
                        column: x => x.Id_Portifolio,
                        principalTable: "Portifolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profissional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Profissao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Id_Portifolio = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissional_Portifolio_Id_Portifolio",
                        column: x => x.Id_Portifolio,
                        principalTable: "Portifolio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profissional_Profissao_Id_Profissao",
                        column: x => x.Id_Profissao,
                        principalTable: "Profissao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Numero_Casa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Numero_Residencia = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Numero_Celular = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Img_perfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nota_Perfil = table.Column<double>(type: "float", nullable: false),
                    Id_Profissional = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Profissional_Id_Profissional",
                        column: x => x.Id_Profissional,
                        principalTable: "Profissional",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Portifolio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nota = table.Column<double>(type: "float", nullable: false),
                    Data_Atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Portifolio_Id_Portifolio",
                        column: x => x.Id_Portifolio,
                        principalTable: "Portifolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuario_Id_Cliente",
                        column: x => x.Id_Cliente,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Contratado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Contratante = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratacao_Profissional_Id_Contratado",
                        column: x => x.Id_Contratado,
                        principalTable: "Profissional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratacao_Usuario_Id_Contratante",
                        column: x => x.Id_Contratante,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_Id_Cliente",
                table: "Comentarios",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_Id_Portifolio",
                table: "Comentarios",
                column: "Id_Portifolio");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacao_Id_Contratado",
                table: "Contratacao",
                column: "Id_Contratado");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacao_Id_Contratante",
                table: "Contratacao",
                column: "Id_Contratante");

            migrationBuilder.CreateIndex(
                name: "IX_ImagemPortifolio_Id_Portifolio",
                table: "ImagemPortifolio",
                column: "Id_Portifolio");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_Id_Portifolio",
                table: "Profissional",
                column: "Id_Portifolio",
                unique: true,
                filter: "[Id_Portifolio] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_Id_Profissao",
                table: "Profissional",
                column: "Id_Profissao");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Id_Profissional",
                table: "Usuario",
                column: "Id_Profissional",
                unique: true,
                filter: "[Id_Profissional] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Contratacao");

            migrationBuilder.DropTable(
                name: "ImagemPortifolio");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Profissional");

            migrationBuilder.DropTable(
                name: "Portifolio");

            migrationBuilder.DropTable(
                name: "Profissao");
        }
    }
}
