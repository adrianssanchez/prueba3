namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAsignacionDocentes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsignaciónDocentes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                        fecha = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.CursoId, t.GrupoId })
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AsignaciónDocentes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AsignaciónDocentes", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.AsignaciónDocentes", "CursoId", "dbo.Cursos");
            DropIndex("dbo.AsignaciónDocentes", new[] { "GrupoId" });
            DropIndex("dbo.AsignaciónDocentes", new[] { "CursoId" });
            DropIndex("dbo.AsignaciónDocentes", new[] { "UserId" });
            DropTable("dbo.AsignaciónDocentes");
        }
    }
}
