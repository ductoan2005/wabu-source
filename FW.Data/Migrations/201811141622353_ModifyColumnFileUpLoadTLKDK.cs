namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyColumnFileUpLoadTLKDK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "EstimateVolumeFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "RequireMaterialFilePath", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "EstimateVolumeFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "RequireMaterialFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "IsSelfMakeRequireMaterial", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_bidding_news", "IsSelfMakeEstimateVolume", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK");
            DropColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK");
            DropColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK");
            DropColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKFileName");
            DropColumn("dbo.tbl_bidding_news", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_news", "Image", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "BuildingPermitTLDKFileName", c => c.String());
            AddColumn("dbo.tbl_bidding_news", "CertificateUseLandTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "VolumeEstimationTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "ConstructionDrawingsTLDK", c => c.Binary());
            AddColumn("dbo.tbl_bidding_news", "BuildingPermitTLDK", c => c.Binary());
            DropColumn("dbo.tbl_bidding_news", "IsSelfMakeEstimateVolume");
            DropColumn("dbo.tbl_bidding_news", "IsSelfMakeRequireMaterial");
            DropColumn("dbo.tbl_bidding_news", "RequireMaterialFileName");
            DropColumn("dbo.tbl_bidding_news", "EstimateVolumeFileName");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingFileName");
            DropColumn("dbo.tbl_bidding_news", "RequireMaterialFilePath");
            DropColumn("dbo.tbl_bidding_news", "EstimateVolumeFilePath");
            DropColumn("dbo.tbl_bidding_news", "ConstructionDrawingFilePath");
        }
    }
}
