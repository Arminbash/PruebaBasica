namespace Prueba.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        IdCompra = c.Int(nullable: false, identity: true),
                        Factura = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdCompra);
            
            CreateTable(
                "dbo.CompraDetalles",
                c => new
                    {
                        IdCompraDetalle = c.Int(nullable: false, identity: true),
                        IdCompra = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCompraDetalle)
                .ForeignKey("dbo.Compras", t => t.IdCompra, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdCompra)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Nombre = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdProducto);
            
            CreateTable(
                "dbo.FacturaDetalles",
                c => new
                    {
                        IdFacturaDetalle = c.Int(nullable: false, identity: true),
                        IdFactura = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdFacturaDetalle)
                .ForeignKey("dbo.Facturas", t => t.IdFactura, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdFactura)
                .Index(t => t.IdProducto);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        IdFactura = c.Int(nullable: false, identity: true),
                        NoFactura = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdFactura);
            
            CreateTable(
                "dbo.Kardexes",
                c => new
                    {
                        IdKardex = c.Int(nullable: false, identity: true),
                        IdProducto = c.Int(nullable: false),
                        CantidadEntrada = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantidadSalida = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdDocumento = c.Int(nullable: false),
                        Documento = c.String(),
                    })
                .PrimaryKey(t => t.IdKardex)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdProducto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kardexes", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.FacturaDetalles", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.FacturaDetalles", "IdFactura", "dbo.Facturas");
            DropForeignKey("dbo.CompraDetalles", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.CompraDetalles", "IdCompra", "dbo.Compras");
            DropIndex("dbo.Kardexes", new[] { "IdProducto" });
            DropIndex("dbo.FacturaDetalles", new[] { "IdProducto" });
            DropIndex("dbo.FacturaDetalles", new[] { "IdFactura" });
            DropIndex("dbo.CompraDetalles", new[] { "IdProducto" });
            DropIndex("dbo.CompraDetalles", new[] { "IdCompra" });
            DropTable("dbo.Kardexes");
            DropTable("dbo.Facturas");
            DropTable("dbo.FacturaDetalles");
            DropTable("dbo.Productoes");
            DropTable("dbo.CompraDetalles");
            DropTable("dbo.Compras");
        }
    }
}
