namespace FW.ViewModels.BiddingNewsRegistration
{
    public class BiddingNewsAbilityHRsVM
    {
        public string id { get; set; }
        public string JobPosition { get; set; } //Vị trí công việc
        public string QualificationRequired { get; set; } //Bằng cấp yêu cầu	
        public string YearExp { get; set; } //Số năm K/N	
        public int NumberRequest { get; set; } //Số lượng
        public string SimilarProgram { get; set; } //Công trình tương tự đã tham gia	
    }
}
