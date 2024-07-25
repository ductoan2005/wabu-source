namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBiddingNews_20180624 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_bidding_news", "DurationContract", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKTmp", c => c.Binary());
            Sql("Update dbo.tbl_bidding_news SET BuildingPermitTLDKTmp = Convert(varbinary, BuildingPermitTLDK)");
            DropColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK");
            RenameColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKTmp", "BuildingPermitTLDK");

            //AlterColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKTmp", c => c.Binary());
            Sql("Update dbo.tbl_bidding_news SET ConstructionDrawingsTLDKTmp = Convert(varbinary, ConstructionDrawingsTLDK)");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK");
            RenameColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKTmp", "ConstructionDrawingsTLDK");

            //AlterColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKTmp", c => c.Binary());
            Sql("Update dbo.tbl_bidding_news SET VolumeEstimationTLDKTmp = Convert(varbinary, VolumeEstimationTLDK)");
            DropColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK");
            RenameColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKTmp", "VolumeEstimationTLDK");

            //AlterColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKTmp", c => c.Binary());
            Sql("Update dbo.tbl_bidding_news SET CertificateUseLandTLDKTmp = Convert(varbinary, CertificateUseLandTLDK)");
            DropColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK");
            RenameColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKTmp", "CertificateUseLandTLDK");

            //AlterColumn("dbo.tbl_bidding_news", "Image", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "ImageTmp", c => c.Binary());
            Sql("Update dbo.tbl_bidding_news SET ImageTmp = Convert(varbinary, Image)");
            DropColumn("dbo.tbl_bidding_news", "Image");
            RenameColumn("dbo.tbl_bidding_news", "ImageTmp", "Image");
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.tbl_bidding_news", "Image", c => c.String());
            RenameColumn("dbo.tbl_bidding_news", "Image", "ImageTmp");
            AddColumn("dbo.tbl_bidding_news", "Image", c => c.String());
            Sql("Update dbo.tbl_bidding_news SET Image = Convert(string, ImageTmp)");
            DropColumn("dbo.tbl_bidding_news", "ImageTmp");

            //AlterColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK", c => c.String());
            RenameColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK", "CertificateUseLandTLDKTmp");
            AddColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK", c => c.String());
            Sql("Update dbo.tbl_bidding_news SET CertificateUseLandTLDK = Convert(string, CertificateUseLandTLDKTmp)");
            DropColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKTmp");

            //AlterColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK", c => c.String());
            RenameColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK", "VolumeEstimationTLDKTmp");
            AddColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK", c => c.String());
            Sql("Update dbo.tbl_bidding_news SET VolumeEstimationTLDK = Convert(string, VolumeEstimationTLDKTmp)");
            DropColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKTmp");

            //AlterColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK", c => c.String());
            RenameColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK", "ConstructionDrawingsTLDKTmp");
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK", c => c.String());
            Sql("Update dbo.tbl_bidding_news SET ConstructionDrawingsTLDK = Convert(string, ConstructionDrawingsTLDKTmp)");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKTmp");

            //AlterColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK", c => c.String());
            RenameColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK", "BuildingPermitTLDKTmp");
            AddColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK", c => c.String());
            Sql("Update dbo.tbl_bidding_news SET BuildingPermitTLDK = Convert(string, BuildingPermitTLDKTmp)");
            DropColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKTmp");

            AlterColumn("dbo.tbl_bidding_news", "DurationContract", c => c.Int(nullable: false));
        }
    }
}
