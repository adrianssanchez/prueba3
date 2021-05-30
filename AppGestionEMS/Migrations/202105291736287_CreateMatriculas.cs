namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMatriculas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculas",
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
            DropForeignKey("dbo.Matriculas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matriculas", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.Matriculas", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Matriculas", new[] { "GrupoId" });
            DropIndex("dbo.Matriculas", new[] { "CursoId" });
            DropIndex("dbo.Matriculas", new[] { "UserId" });
            DropTable("dbo.Matriculas");
        }
    }
}
