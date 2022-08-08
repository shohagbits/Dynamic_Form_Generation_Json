namespace Dynamic_Form_Generation_Json.Models
{
    public class BeneficiaryInformation
    {
        public string BankName { get; set; }
        public string BankAccountNo { get; internal set; }
        public string BankCode { get; internal set; }
        public string ReasonForSending { get; internal set; }
        public string BeneficiaryCountryCode { get; internal set; }
        public string BeneficiaryMobileNo { get; internal set; }
        public string BeneficiaryBankBranchCode { get; internal set; }
        public string BeneficiaryBankRoutingNumber { get; internal set; }
        public string BeneficiaryBankAccountType { get; internal set; }
        public string BeneficiaryAddress { get; internal set; }
        public string BeneficiaryCity { get; internal set; }
        public string BeneficiaryState { get; internal set; }
        public string BeneficiaryPostalCode { get; internal set; }
    }
}
