namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_wabu_20180321 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_area",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AreaName = c.String(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_bidding_news",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BiddingPackageId = c.Long(nullable: false),
                        WorkContentId = c.Long(),
                        WorkContentOtherId = c.Long(),
                        ContructionId = c.Long(nullable: false),
                        BiddingPackageDescription = c.String(),
                        ContractFormId = c.Long(),
                        DurationContract = c.Int(nullable: false),
                        NumberBidder = c.Int(nullable: false),
                        NumberBidded = c.Int(nullable: false),
                        BidStartDate = c.DateTime(nullable: false),
                        BidCloseDate = c.DateTime(nullable: false),
                        StatusInvestor = c.Byte(nullable: false),
                        StatusContractor = c.Byte(nullable: false),
                        NameContact = c.String(),
                        EmailContact = c.String(),
                        NumberPhoneContact = c.String(),
                        IsDisplayContact = c.Boolean(),
                        IsRegisEstablishmentTCHL = c.Boolean(),
                        IsFinancialTCHL = c.Boolean(),
                        IsDissolutionProcessTCHL = c.Boolean(),
                        IsBankruptTCHL = c.Boolean(),
                        NumberYearActivityAbilityExp = c.Int(nullable: false),
                        NumberSimilarContractAbilityExp = c.Int(nullable: false),
                        IsContractAbilityExp = c.Boolean(),
                        IsLiquidationAbilityExp = c.Boolean(),
                        IsBuildingPermitAbilityExp = c.Boolean(),
                        IsLaborContractAbilityHR = c.Boolean(),
                        IsDocumentRequestAbilityHR = c.Boolean(),
                        IsDecisionAbilityHR = c.Boolean(),
                        Turnover2YearAbilityFinance = c.Int(nullable: false),
                        IsFinanceSituationAbilityFinance = c.Boolean(),
                        IsProtocolAbilityFinance = c.Boolean(),
                        IsDeclarationAbilityFinance = c.Boolean(),
                        IsDocumentAbilityFinance = c.Boolean(),
                        IsReportAbilityFinance = c.Boolean(),
                        IsContractAbilityEquipment = c.Boolean(),
                        IsProfileAbilityEquipment = c.Boolean(),
                        IsProgressScheduleMKT = c.Boolean(),
                        IsQuotationMKT = c.Boolean(),
                        IsMaterialsUseMKT = c.Boolean(),
                        IsDrawingConstructionMKT = c.Boolean(),
                        IsWorkSafetyMKT = c.Boolean(),
                        IsEnvironmentalSanitationMKT = c.Boolean(),
                        IsFireProtectionMKT = c.Boolean(),
                        BuildingPermitTLDK = c.String(),
                        ConstructionDrawingsTLDK = c.String(),
                        VolumeEstimationTLDK = c.String(),
                        CertificateUseLandTLDK = c.String(),
                        Image = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_package", t => t.BiddingPackageId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_contract_form", t => t.ContractFormId)
                .ForeignKey("dbo.tbl_contruction", t => t.ContructionId)
                .ForeignKey("dbo.tbl_work_content", t => t.WorkContentId)
                .ForeignKey("dbo.tbl_work_content_other", t => t.WorkContentOtherId)
                .Index(t => t.BiddingPackageId)
                .Index(t => t.WorkContentId)
                .Index(t => t.WorkContentOtherId)
                .Index(t => t.ContructionId)
                .Index(t => t.ContractFormId);
            
            CreateTable(
                "dbo.tbl_bidding_news_ability_equipment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EquipmentName = c.String(),
                        QuantityEquipment = c.Int(nullable: false),
                        IsAccreditation = c.Boolean(),
                        BiddingNewsId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_news", t => t.BiddingNewsId)
                .Index(t => t.BiddingNewsId);
            
            CreateTable(
                "dbo.tbl_bidding_news_ability_hr",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobPosition = c.String(),
                        QualificationRequired = c.String(),
                        YearExp = c.Int(nullable: false),
                        NumberRequest = c.Int(nullable: false),
                        SimilarProgram = c.String(),
                        BiddingNewsId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_news", t => t.BiddingNewsId)
                .Index(t => t.BiddingNewsId);
            
            CreateTable(
                "dbo.tbl_technical_other",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TechnicalRequirementName = c.String(),
                        BiddingNewsId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_news", t => t.BiddingNewsId)
                .Index(t => t.BiddingNewsId);
            
            CreateTable(
                "dbo.tbl_bidding_package",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BiddingPackageName = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_work_content_other",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkContentOtherName = c.String(),
                        BiddingPackageId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_package", t => t.BiddingPackageId)
                .Index(t => t.BiddingPackageId);
            
            CreateTable(
                "dbo.tbl_work_content",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkContentName = c.String(),
                        BiddingPackageId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_bidding_package", t => t.BiddingPackageId)
                .Index(t => t.BiddingPackageId);
            
            CreateTable(
                "dbo.tbl_contract_form",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContractFormName = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_contruction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ContructionName = c.String(),
                        InvestorName = c.String(),
                        AddressBuild = c.String(),
                        Scale = c.Int(nullable: false),
                        AcreageBuild = c.Int(nullable: false),
                        ContructionDescription = c.String(),
                        ContactName = c.String(),
                        ContactPhoneNumber = c.String(),
                        IsDisplayContact = c.Boolean(nullable: false),
                        AreaId = c.Long(),
                        BuildingPermit = c.String(nullable: false),
                        BuildingPermitDate = c.DateTime(),
                        ContactEmail = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_area", t => t.AreaId)
                .ForeignKey("dbo.tbl_users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.tbl_users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        PasswordChangedDate = c.DateTime(),
                        DateOfBirth = c.DateTime(),
                        Authority = c.Byte(nullable: false),
                        FullName = c.String(maxLength: 50),
                        Gender = c.Byte(),
                        CMND = c.String(maxLength: 9),
                        PhoneNumber = c.String(maxLength: 11),
                        Address = c.String(maxLength: 250),
                        Email = c.String(maxLength: 50),
                        Image = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsPerson = c.Boolean(),
                        IsAgreeTerm = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "IX_users_1");
            
            CreateTable(
                "dbo.tbl_login_history",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LoginFailedTimes = c.Int(),
                        FirstLoginFailedTime = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_company",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(),
                        CompanyName = c.String(nullable: false, maxLength: 20),
                        Introduction = c.String(),
                        RepresentativeName = c.String(),
                        Position = c.String(),
                        CompanyAddress = c.String(),
                        CompanyPhoneNumber = c.String(),
                        NoBusinessLicense = c.String(),
                        FoundedYear = c.String(),
                        Capital = c.String(),
                        TaxCode = c.String(),
                        OrganizationalChart = c.String(),
                        ContactName = c.String(),
                        ContactPhoneNumber = c.String(),
                        Logo = c.String(),
                        OneStar = c.Int(nullable: false),
                        TwoStar = c.Int(nullable: false),
                        ThreeStar = c.Int(nullable: false),
                        FourStar = c.Int(nullable: false),
                        FiveStar = c.Int(nullable: false),
                        Link = c.String(),
                        IsOnOver = c.Byte(nullable: false),
                        TotalNewsBidded = c.Int(nullable: false),
                        NumberNewsBidded = c.Int(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_company_ability_equipment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EquipmentType = c.String(),
                        Quantity = c.Int(nullable: false),
                        Capacity = c.String(),
                        Function = c.String(),
                        NationalProduction = c.String(),
                        YearManufacture = c.String(),
                        QualityUse = c.Int(nullable: false),
                        Source = c.String(),
                        EvidenceSaleContract = c.String(),
                        EvidenceInspectionRecords = c.String(),
                        CompanyId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_company_ability_exp",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectName = c.String(),
                        InvestorName = c.String(),
                        InvestorAddress = c.String(),
                        InvestorPhoneNumber = c.String(),
                        ContructionType = c.String(),
                        ProjectScale = c.String(),
                        ContractName = c.String(),
                        ContractSignDate = c.String(),
                        ContractCompleteDate = c.String(),
                        ContractPrices = c.String(),
                        ProjectDescription = c.String(),
                        EvidenceContract = c.String(),
                        EvidenceContractLiquidation = c.String(),
                        EvidenceBuildingPermit = c.String(),
                        CompanyId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_company_ability_finance",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        YearDeclare = c.Int(nullable: false),
                        TotalAssets = c.Int(nullable: false),
                        TotalLiabilities = c.Int(nullable: false),
                        ShortTermAssets = c.Int(nullable: false),
                        TotalCurrentLiabilities = c.Int(nullable: false),
                        Revenue = c.Int(nullable: false),
                        ProfitBeforeTax = c.Int(nullable: false),
                        ProfitAfterTax = c.Int(nullable: false),
                        EvidenceCheckSettlement = c.String(),
                        EvidenceDeclareTax = c.String(),
                        EvidenceCertificationTax = c.String(),
                        EvidenceAuditReport = c.String(),
                        CompanyId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_company_ability_hr",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        Age = c.String(),
                        Title = c.String(),
                        Certificate = c.String(),
                        School = c.String(),
                        Branch = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        EvidenceLaborContract = c.String(),
                        EvidenceSimilarCertificates = c.String(),
                        EvidenceAppointmentStaff = c.String(),
                        CompanyId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_company_ability_hr_detail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FromYear = c.DateTime(),
                        ToYear = c.DateTime(),
                        ProjectSimilar = c.String(),
                        PositionSimilar = c.String(),
                        ExpTechnical = c.String(),
                        CompanyAbilityHRId = c.Long(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company_ability_hr", t => t.CompanyAbilityHRId)
                .Index(t => t.CompanyAbilityHRId);
            
            CreateTable(
                "dbo.tbl_company_ability_staff",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CompanyId = c.Long(nullable: false),
                        FullName = c.String(),
                        Position = c.String(),
                        PhoneNumber = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_company_profile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CompanyId = c.Long(),
                        NameProfile = c.String(),
                        AbilityEquipmentsId = c.String(),
                        AbilityHRsId = c.String(),
                        AbilityExpsId = c.String(),
                        AbilityFinancesId = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_bidding_detail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CompanyProfileId = c.Long(),
                        BiddingNewsId = c.Long(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BiddingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_company_ability_staff", "CompanyId", "dbo.tbl_company");
            DropForeignKey("dbo.tbl_company_ability_hr", "CompanyId", "dbo.tbl_company");
            DropForeignKey("dbo.tbl_company_ability_hr_detail", "CompanyAbilityHRId", "dbo.tbl_company_ability_hr");
            DropForeignKey("dbo.tbl_company_ability_finance", "CompanyId", "dbo.tbl_company");
            DropForeignKey("dbo.tbl_company_ability_exp", "CompanyId", "dbo.tbl_company");
            DropForeignKey("dbo.tbl_company_ability_equipment", "CompanyId", "dbo.tbl_company");
            DropForeignKey("dbo.tbl_bidding_news", "WorkContentOtherId", "dbo.tbl_work_content_other");
            DropForeignKey("dbo.tbl_bidding_news", "WorkContentId", "dbo.tbl_work_content");
            DropForeignKey("dbo.tbl_contruction", "UserId", "dbo.tbl_users");
            DropForeignKey("dbo.tbl_bidding_news", "ContructionId", "dbo.tbl_contruction");
            DropForeignKey("dbo.tbl_contruction", "AreaId", "dbo.tbl_area");
            DropForeignKey("dbo.tbl_bidding_news", "ContractFormId", "dbo.tbl_contract_form");
            DropForeignKey("dbo.tbl_bidding_news", "BiddingPackageId", "dbo.tbl_bidding_package");
            DropForeignKey("dbo.tbl_work_content", "BiddingPackageId", "dbo.tbl_bidding_package");
            DropForeignKey("dbo.tbl_work_content_other", "BiddingPackageId", "dbo.tbl_bidding_package");
            DropForeignKey("dbo.tbl_technical_other", "BiddingNewsId", "dbo.tbl_bidding_news");
            DropForeignKey("dbo.tbl_bidding_news_ability_hr", "BiddingNewsId", "dbo.tbl_bidding_news");
            DropForeignKey("dbo.tbl_bidding_news_ability_equipment", "BiddingNewsId", "dbo.tbl_bidding_news");
            DropIndex("dbo.tbl_company_ability_staff", new[] { "CompanyId" });
            DropIndex("dbo.tbl_company_ability_hr_detail", new[] { "CompanyAbilityHRId" });
            DropIndex("dbo.tbl_company_ability_hr", new[] { "CompanyId" });
            DropIndex("dbo.tbl_company_ability_finance", new[] { "CompanyId" });
            DropIndex("dbo.tbl_company_ability_exp", new[] { "CompanyId" });
            DropIndex("dbo.tbl_company_ability_equipment", new[] { "CompanyId" });
            DropIndex("dbo.tbl_users", "IX_users_1");
            DropIndex("dbo.tbl_contruction", new[] { "AreaId" });
            DropIndex("dbo.tbl_contruction", new[] { "UserId" });
            DropIndex("dbo.tbl_work_content", new[] { "BiddingPackageId" });
            DropIndex("dbo.tbl_work_content_other", new[] { "BiddingPackageId" });
            DropIndex("dbo.tbl_technical_other", new[] { "BiddingNewsId" });
            DropIndex("dbo.tbl_bidding_news_ability_hr", new[] { "BiddingNewsId" });
            DropIndex("dbo.tbl_bidding_news_ability_equipment", new[] { "BiddingNewsId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "ContractFormId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "ContructionId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "WorkContentOtherId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "WorkContentId" });
            DropIndex("dbo.tbl_bidding_news", new[] { "BiddingPackageId" });
            DropTable("dbo.tbl_bidding_detail");
            DropTable("dbo.tbl_company_profile");
            DropTable("dbo.tbl_company_ability_staff");
            DropTable("dbo.tbl_company_ability_hr_detail");
            DropTable("dbo.tbl_company_ability_hr");
            DropTable("dbo.tbl_company_ability_finance");
            DropTable("dbo.tbl_company_ability_exp");
            DropTable("dbo.tbl_company_ability_equipment");
            DropTable("dbo.tbl_company");
            DropTable("dbo.tbl_login_history");
            DropTable("dbo.tbl_users");
            DropTable("dbo.tbl_contruction");
            DropTable("dbo.tbl_contract_form");
            DropTable("dbo.tbl_work_content");
            DropTable("dbo.tbl_work_content_other");
            DropTable("dbo.tbl_bidding_package");
            DropTable("dbo.tbl_technical_other");
            DropTable("dbo.tbl_bidding_news_ability_hr");
            DropTable("dbo.tbl_bidding_news_ability_equipment");
            DropTable("dbo.tbl_bidding_news");
            DropTable("dbo.tbl_area");
        }
    }
}
