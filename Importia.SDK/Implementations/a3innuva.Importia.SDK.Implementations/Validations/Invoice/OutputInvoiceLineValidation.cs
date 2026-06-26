namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class OutputInvoiceLineValidation : Validation<IOutputInvoiceLine>
    {
        private readonly Regex accountCodeFormat;
        private readonly AnalyticDistributionValidation analyticDistributionValidation;

        public OutputInvoiceLineValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.analyticDistributionValidation = new AnalyticDistributionValidation();
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.ValidateNullable(x.CounterPart, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.CounterPart), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateNullable(x.CounterPartDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción contrapartida'"));
            this.CreateRule(x => this.Validate(x.Transaction), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Operación'"));
            this.CreateRule(x => this.ValidateTransaction(x.Transaction), this.ReplaceInMessage("No es una operación valida"));
            this.CreateRule(x => this.ValidateWithHolding(x.WithHolding), this.ReplaceInMessage("No es una retención valida"));
            this.CreateRule(x => this.ValidatePercentage(x.WithHoldingPercentage), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Porcentaje de retención'"));

            this.CreateRule(x => this.ValidateAnalyticDistributionsTotalPercentage(x.AnalyticDistributions), "La suma de los porcentajes de las distribuciones analíticas no puede ser superior a 100");
        }

        public override IEnumerable<IValidationResult> Validate(IOutputInvoiceLine entity)
        {
            var errors = new List<IValidationResult>(base.Validate(entity));

            if (entity.AnalyticDistributions == null)
                return errors;

            foreach (var distribution in entity.AnalyticDistributions)
            {
                var distributionErrors = this.analyticDistributionValidation.Validate(distribution, entity.Line);
                errors.AddRange(distributionErrors.Where(x => !x.IsValid));
            }

            return errors;
        }

        private bool ValidateAnalyticDistributionsTotalPercentage(IEnumerable<IAnalyticDistribution> distributions)
        {
            if (distributions == null)
                return true;

            return distributions.Sum(d => d.Percentage) <= 100;
        }
        
        private bool ValidateTransaction(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return Transactions.ItExistForOutput(input);
        }

        private bool ValidateWithHolding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return WithHoldings.ItExistForOutput(input);
        }

        private bool ValidatePercentage(decimal ? input)
        {
            return input.ValidatePercentage();
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }
    }
}
