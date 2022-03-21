namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class WithHoldings
    {
        private static readonly List<string> withHoldingsOutput;
        private static readonly List<string> withHoldingsInput;

        static WithHoldings()
        {
            withHoldingsOutput = new List<string>()
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
                "TRA_PNEX",
                "TRA_CUR",
                "TRA_OBR",
                "CMO_PROINTELEC",
                "CMO_PROINDUS",
                "CMO_ATE",
                "CMO_ABM",
                "CMO_RDODI",
                "CMO_SIN",
                "CMO_SINCYM",
                "CMO_REDI",
                "CMO_ORDO",
                "CMO_ORDOCYM",
                "OAE_DIMG",
                "OAE_RCON",
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

            withHoldingsInput = new List<string>()
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
                "TRA_PNEX",
                "TRA_CUR",
                "TRA_OBR",
                "CMO_PROINTELEC",
                "CMO_PROINDUS",
                "CMO_ATE",
                "CMO_ABM",
                "CMO_RDODI",
                "CMO_SIN",
                "CMO_SINCYM",
                "CMO_REDI",
                "CMO_ORDO",
                "CMO_ORDOCYM",
                "OAE_DIMG",
                "OAE_RCON",
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
        }

        public static bool ItExistForOutput(string code)
        {
            return withHoldingsOutput.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return withHoldingsInput.Contains(code);
        }
    }
}
