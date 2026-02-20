using System.ComponentModel.DataAnnotations;

namespace LMSBackend.Domain.Enums
{
    public enum RemittanceChannel
    {
        [Display(Name = "E-WALLET (MAYA, GCASH, GOTYME)")]
        EWallet = 1,

        [Display(Name = "BANK")]
        Bank = 2,

        [Display(Name = "REMITTANCE CENTER (WHICH INCLUDES LBC, MLHUILLIER ETC)")]
        RemittanceCenter = 3
    }

    public enum EmploymentStatus
    {
        [Display(Name = "REHIRED")]
        rehired = 1,

        [Display(Name = "UNDER AGENCY")]
        underAgency = 2,

        [Display(Name = "DIRECT HIRED")]
        directHired = 3,
        [Display(Name = "LB Abroad")]
        LBAbroad = 4
    }
}