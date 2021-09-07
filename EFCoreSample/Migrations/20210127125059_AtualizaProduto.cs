using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSample.Migrations
{
    public partial class AtualizaProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Produtos_ProdudoId",
                table: "PedidoItens");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_ProdudoId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "ProdudoId",
                table: "PedidoItens");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "PedidoItens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_ProdutoId",
                table: "PedidoItens",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Produtos_ProdutoId",
                table: "PedidoItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_Produtos_ProdutoId",
                table: "PedidoItens");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_ProdutoId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "PedidoItens");

            migrationBuilder.AddColumn<int>(
                name: "ProdudoId",
                table: "PedidoItens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_ProdudoId",
                table: "PedidoItens",
                column: "ProdudoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_Produtos_ProdudoId",
                table: "PedidoItens",
                column: "ProdudoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
