namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class WithHoldings
    {
        private static readonly List<string> WithHoldingsOutput = new List<string>()
        {
            "CIN_ARR",
            "AP_CG",
            "AP_DA",
            "AP_IA",
            "AP_LR",
            "OAE_CG",
            "OAE_EPA",
            "OAE_FOR",
            "OAE_ESO",
            "AP_ART",
            "AP_PRI",
            "AP_PRIR",
            "AP_ANT",
            "TRA_PNEX",
            "TRA_CUR",
            "TRA_OBR",
            "TRA_OBRM",
            "TRA_PRI",
            "TRA_PRIR",
            "TRA_ANT",
            "CMO_PROINTELEC",
            "CMO_PROINTELECM",
            "CMO_PROINDUS",
            "CMO_ATE",
            "CMO_ABM",
            "CMO_RDODI",
            "CMO_SIN",
            "CMO_SINCYM",
            "CMO_REDI",
            "CMO_ORDO",
            "CMO_ORAHO",
            "CMO_ORDOCYM",
            "CMO_ANTICIPOS",
            "OAE_DIMG",
            "OAE_RCON",
            "OAE_PRI",
            "OAE_PRIR",
            "OAE_ANT",
            "TRA_CSA1",
            "TRA_CSAINF",
            "TRA_CSA2",
            "TRA_CSASUP",
            "CYA_GSUP",
            "CYA_GINF",
            "CYA_AINF",
            "CYA_OTR",
            "CDINR",
            "PGPF_JDE",
            "PGPF_FOR",
            "PGPF_CFP",
            "AGR_GYP",
            "AGR_AYF",
        };
        
        private static readonly List<string> WithHoldingsInput= new List<string>()
        {
            "CIN_ARR",
            "AP_CG",
            "AP_DA",
            "AP_IA",
            "AP_LR",
            "OAE_CG",
            "OAE_EPA",
            "OAE_FOR",
            "OAE_ESO",
            "AP_ART",
            "AP_PRI",
            "AP_PRIR",
            "AP_ANT",
            "TRA_PNEX",
            "TRA_CUR",
            "TRA_OBR",
            "TRA_OBRM",
            "TRA_PRI",
            "TRA_PRIR",
            "TRA_ANT",
            "CMO_PROINTELEC",
            "CMO_PROINTELECM",
            "CMO_PROINDUS",
            "CMO_ATE",
            "CMO_ABM",
            "CMO_RDODI",
            "CMO_SIN",
            "CMO_SINCYM",
            "CMO_REDI",
            "CMO_ORDO",
            "CMO_ORAHO",
            "CMO_ORDOCYM",
            "CMO_ANTICIPOS",
            "OAE_DIMG",
            "OAE_RCON",
            "OAE_PRI",
            "OAE_PRIR",
            "OAE_ANT",
            "TRA_CSA1",
            "TRA_CSAINF",
            "TRA_CSA2",
            "TRA_CSASUP",
            "CYA_GSUP",
            "CYA_GINF",
            "CYA_AINF",
            "CYA_OTR",
            "CDINR",
            "PGPF_JDE",
            "PGPF_FOR",
            "PGPF_CFP",
            "AGR_GYP",
            "AGR_AYF",
        };



        public static bool ItExistForOutput(string code)
        {
            return WithHoldingsOutput.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return WithHoldingsInput.Contains(code);
        }
    }
}
