namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using a3innuva.TAA.Migration.SDK.Interfaces.Entities.Journal;

    public class JournalLineValidation : Validation<IJournalLine>
    {
        private readonly Regex accountCodeFormat;
        private readonly AnalyticDistributionValidation analyticDistributionValidation;

        public JournalLineValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.analyticDistributionValidation = new AnalyticDistributionValidation();
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.Account), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta'"));
            this.CreateRule(x => this.Validate(x.Account, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta'"));
            this.CreateRule(x => this.accountCodeFormat.IsMatch(x.Account), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta'"));

            this.CreateRule(x => this.Validate(x.AccountDescription), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción cuenta'"));
            this.CreateRule(x => this.Validate(x.AccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción cuenta'"));

            this.CreateRule(x => this.ValidateNullable(x.Document, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Documento'"));
            this.CreateRule(x => this.ValidateNullable(x.Concept, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Concepto'"));

            this.CreateRule(x => x.HasValidAmount(), "Los importes no son correctos");

            this.CreateRule(x => this.ValidateAnalyticDistributionsTotalPercentage(x.AnalyticDistributions), "La suma de los porcentajes de las distribuciones analíticas no puede ser superior a 100");
        }

        public override IEnumerable<IValidationResult> Validate(IJournalLine entity)
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
    }
}
